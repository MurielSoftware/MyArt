using Shared.Core.Attributes;
using Shared.Core.Context.Expressions.Operators;
using Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Context.Expressions
{
    public class ExpressionBuilder
    {
        private static Dictionary<CompareOperator, IExpressionOperator> COMPARE_OPERATOR_TO_EXPRESSION_OPERATOR_MAP = CreateCompareOperatorToExpressionOperatorMap();

        private static Dictionary<CompareOperator, IExpressionOperator> CreateCompareOperatorToExpressionOperatorMap()
        {
            return new Dictionary<CompareOperator, IExpressionOperator>()
            {
                { CompareOperator.EQUAL, new ExpressionEqual() },
                { CompareOperator.NON_EQUAL, new ExpressionNonEqual() },
                { CompareOperator.GREATER_OR_EQUAL_THAN, new ExpressionGreaterOrEqual() },
                { CompareOperator.GREATER_THAN, new ExpressionGreater() },
                { CompareOperator.LESS_OR_EQUAL_THAN, new ExpressionLessOrEqual() },
                { CompareOperator.LESS_THAN, new ExpressionLess() },
                { CompareOperator.CONTAINS, new ExpressionContains() },
                { CompareOperator.IS_IN_COLLECTION, new ExpressionAny() },
            };
        }

        public static Expression<Func<T, bool>> BuildWhere<T>(BaseFilterDto baseFilterDto) where T : IBaseEntity
        {
            Parameter parameter = new Parameter(typeof(T));
            List<Expression> whereParts = new List<Expression>();

            foreach (IGrouping<string, PropertyInfo> groupedfilterProperties in GetFilterProperties(baseFilterDto))
            {
                List<Expression> expressionGroupedAndWhereParts = new List<Expression>();
                List<Expression> expressionGroupedOrWhereParts = new List<Expression>();
                foreach (PropertyInfo propertyInfo in groupedfilterProperties)
                {
                    FilterAttribute filterAttribute = propertyInfo.GetCustomAttribute<FilterAttribute>();
                    object value = propertyInfo.GetValue(baseFilterDto);
                    Expression wherePartExpression = BuildWherePart<T>(parameter, filterAttribute, value);
                    if(wherePartExpression != null)
                    {
                        expressionGroupedAndWhereParts.Add(wherePartExpression);
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
            return Expression.Lambda<Func<T, bool>>(JoinAndWhereParts(whereParts), parameter.GetExpression());
        }

        private static IEnumerable<IGrouping<string, PropertyInfo>> GetFilterProperties(BaseFilterDto baseFilterDto)
        {
            return baseFilterDto.GetType().GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(FilterAttribute)))
                .GroupBy(x => x.GetCustomAttribute<FilterAttribute>().TargetProperty);
        }

        private static Expression BuildWherePart<T>(Parameter parameter, FilterAttribute filterAttribute, object value)
        {
            if(value == null)
            {
                return null;
            }

            if(CompareOperator.IS_IN_COLLECTION.Equals(filterAttribute.Operator))
            {
                return COMPARE_OPERATOR_TO_EXPRESSION_OPERATOR_MAP[filterAttribute.Operator].Generate(parameter, null, null, filterAttribute, value);
            }

            LeftExpression left = LeftExpression.Create(parameter, filterAttribute);
            RightExpression right = RightExpression.Create(value, left);
            return COMPARE_OPERATOR_TO_EXPRESSION_OPERATOR_MAP[filterAttribute.Operator].Generate(parameter, left, right, filterAttribute, value);
        }

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
