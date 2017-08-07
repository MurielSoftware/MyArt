using Shared.Core.Attributes;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Context
{
    /// <summary>
    /// Class reponsible for build the where sql statement.
    /// </summary>
    public class ExpressionQueryBuilder
    {
        private const string EXPRESSION_PARAMETER = "x";

        /// <summary>
        /// Builds the where statement.
        /// </summary>
        /// <typeparam name="T">The Entity type</typeparam>
        /// <param name="baseFilterDto">The filtering DTO</param>
        /// <returns>The where statement</returns>
        public static Expression<Func<T, bool>> BuildWhere<T>(BaseFilterDto baseFilterDto) where T : IBaseEntity
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), EXPRESSION_PARAMETER);
            List<Expression> whereParts = new List<Expression>();

            foreach (IGrouping<string, PropertyInfo> searchPropertyGroup in GetGroupedSearchedPropertiesInfo(baseFilterDto))
            {
                List<Expression> expressionGroupedOrWhereParts = new List<Expression>();
                List<Expression> expressionGroupedAndWhereParts = new List<Expression>();
                foreach (PropertyInfo searchPropertyInfo in searchPropertyGroup)
                {
                    FilterAttribute searchAttribute = (FilterAttribute)searchPropertyInfo.GetCustomAttribute(typeof(FilterAttribute));
                    object value = searchPropertyInfo.GetValue(baseFilterDto);
                    Expression expressionWherePart = null;
                    //if (value is bool && searchAttribute.EnumAttribute != null)
                    //{
                    //    if ((bool)value == true)
                    //    {
                    //        expressionWherePart = BuildWherePart<T>(parameterExpression, searchAttribute, searchAttribute.EnumAttribute);
                    //        if (expressionWherePart != null)
                    //        {
                    //            expressionGroupedOrWhereParts.Add(expressionWherePart);
                    //        }
                    //    }
                    //}
                    //else
                    {
                        expressionWherePart = BuildWherePart<T>(parameterExpression, searchAttribute, value);
                        if (expressionWherePart != null)
                        {
                            expressionGroupedAndWhereParts.Add(expressionWherePart);
                        }
                    }
                }
                Expression finalGroupedOrWhereParts = JoinOrWhereParts(expressionGroupedOrWhereParts);
                Expression finalGroupedAndWhereParts = JoinAndWhereParts(expressionGroupedAndWhereParts);
                if (finalGroupedOrWhereParts != null)
                {
                    whereParts.Add(finalGroupedOrWhereParts);
                }
                if (finalGroupedAndWhereParts != null)
                {
                    whereParts.Add(finalGroupedAndWhereParts);
                }
            }

            if (whereParts.Count == 0)
            {
                return x => true;
            }

            return Expression.Lambda<Func<T, bool>>(JoinAndWhereParts(whereParts), parameterExpression);
        }

        /// <summary>
        /// Gets the grouped searched properties by target property.
        /// </summary>
        /// <param name="baseFilterDto">The base filtering DTO</param>
        /// <returns>The grouped searched properties</returns>
        private static IEnumerable<IGrouping<string, PropertyInfo>> GetGroupedSearchedPropertiesInfo(BaseFilterDto baseFilterDto)
        {
            return baseFilterDto.GetType().GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(FilterAttribute), true).Length > 0)
                .GroupBy(x => ((FilterAttribute)x.GetCustomAttribute(typeof(FilterAttribute))).TargetProperty);
        }

        /// <summary>
        /// Builds the one where part.
        /// </summary>
        /// <typeparam name="T">The Entity type</typeparam>
        /// <param name="parameterExpression">The current parameter expression for the where expression</param>
        /// <param name="searchAttribute">The current search attribute</param>
        /// <param name="value">The value of the searched property in the search DTO</param>
        /// <returns>The where part segment</returns>
        private static Expression BuildWherePart<T>(ParameterExpression parameterExpression, FilterAttribute searchAttribute, object value)
        {
            if (value == null)
            {
                return null;
            }

            Expression left = Expression.Property(parameterExpression, searchAttribute.TargetProperty);
            Expression right = GetValueExpression(value, left.Type);
            switch (searchAttribute.Operator)
            {
                case CompareOperator.EQUAL:
                    return Expression.Equal(left, right);
                case CompareOperator.NON_EQUAL:
                    return Expression.NotEqual(left, right);
                case CompareOperator.GREATER_OR_EQUAL_THAN:
                    return Expression.GreaterThanOrEqual(left, right);
                case CompareOperator.GREATER_THAN:
                    return Expression.GreaterThan(left, right);
                case CompareOperator.LESS_OR_EQUAL_THAN:
                    return Expression.LessThanOrEqual(left, right);
                case CompareOperator.LESS_THAN:
                    return Expression.LessThan(left, right);
                case CompareOperator.CONTAINS:
                    return Expression.Call(left, typeof(string).GetMethod("Contains", new[] { typeof(string) }), right);
                case CompareOperator.IS_IN_COLLECTION:
                    return AnyCall(parameterExpression, searchAttribute, value);

            }
            return null;
        }

        /// <summary>
        /// Gets the converted value expression.
        /// </summary>
        /// <param name="value">The value of the attribute</param>
        /// <param name="type">The type of the attribute</param>
        /// <returns></returns>
        private static Expression GetValueExpression(object value, Type type)
        {
            return Expression.Convert(Expression.Constant(value), type);
        }

        private static Expression AnyCall(ParameterExpression parameterExpression, FilterAttribute filterAttribute, object value)
        {
            Expression collectionExpression = Expression.PropertyOrField(parameterExpression, filterAttribute.TargetProperty);
            Type collectionType = collectionExpression.Type;
            Type elemType = collectionType.GetGenericArguments()[0];
            Type predType = typeof(Func<,>).MakeGenericType(elemType, typeof(bool));

            MethodInfo anyMethod = typeof(Enumerable).GetMethods().Single(x => x.Name == "Any" && x.GetParameters().Count() == 2).MakeGenericMethod(new Type[] { elemType });

            ParameterExpression p2 = Expression.Parameter(elemType, "y");
            Expression left = Expression.Property(p2, elemType.GetProperty(filterAttribute.SourceJoinProperty));
            Expression InnerLambda = Expression.Equal(left, GetValueExpression(value, left.Type));

            //Expression<Func<IBaseEntity, bool>> innerFunction = Expression.Lambda<Func<IBaseEntity, bool>>(InnerLambda, p2);
            var innerFunction = typeof(Func<,>).MakeGenericType(elemType, typeof(bool));
            Expression innerExpression = Expression.Lambda(innerFunction, InnerLambda, p2);


            return Expression.Call(anyMethod, collectionExpression, innerExpression);
        }

        /// <summary>
        /// Joins all the where statements to one.
        /// </summary>
        /// <param name="whereParts">The where statements</param>
        /// <returns>The complete where expression</returns>
        private static Expression JoinAndWhereParts(List<Expression> whereParts)
        {
            Expression finalWhere = null;
            foreach (Expression wherePart in whereParts)
            {
                if (finalWhere == null)
                {
                    finalWhere = wherePart;
                }
                else
                {
                    finalWhere = Expression.AndAlso(finalWhere, wherePart);
                }
            }
            return finalWhere;
        }

        /// <summary>
        /// Joins the or parts.
        /// </summary>
        /// <param name="whereParts">The where statements</param>
        /// <returns>The or part.</returns>
        private static Expression JoinOrWhereParts(List<Expression> whereParts)
        {
            Expression finalWhere = null;
            foreach (Expression wherePart in whereParts)
            {
                if (finalWhere == null)
                {
                    finalWhere = wherePart;
                }
                else
                {
                    finalWhere = Expression.Or(finalWhere, wherePart);
                }
            }
            return finalWhere;
        }
    }
}
