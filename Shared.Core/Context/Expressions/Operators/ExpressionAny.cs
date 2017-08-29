using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Attributes;
using System.Reflection;

namespace Shared.Core.Context.Expressions.Operators
{
    public class ExpressionAny : IExpressionOperator
    {
        private const string ANY_METHOD_NAME = "Any";

        public Expression Generate(Parameter parameter, LeftExpression left, RightExpression right, FilterAttribute filterAttribute, object value)
        {
            List<string> leftExpressionProperties = GetLeftExpressionProperties(filterAttribute.TargetProperty);
            return GenerateAnyExpression(parameter, leftExpressionProperties, value);
        }

        private static Expression GenerateAnyExpression(Parameter parameter, List<string> leftExpressionProperties, object value)
        {
            Expression collectionExpression = Expression.PropertyOrField(parameter.GetExpression(), leftExpressionProperties.First());
            Type collectionType = collectionExpression.Type;
            Type elementType = collectionType.GetGenericArguments()[0];
            MethodInfo anyMethod = typeof(Enumerable).GetMethods().Single(x => x.Name == ANY_METHOD_NAME && x.GetParameters().Count() == 2).MakeGenericMethod(new Type[] { elementType });
            return Expression.Call(anyMethod, collectionExpression, GenerateInnerExpression(parameter, elementType, leftExpressionProperties.Skip(1).ToList(), value));
        }

        private static Expression GenerateInnerExpression(Parameter parameter, Type elementType, List<string> leftExpressionProperties, object value)
        {
            Parameter innerParameter = new Parameter(elementType, parameter.GetIndex() + 1);
            if (leftExpressionProperties.Count == 1)
            {
                return GenerateEqualExpression(innerParameter, elementType, leftExpressionProperties.First(), value);
            }
            return GenerateAnyExpression(innerParameter, leftExpressionProperties, value);
        }

        private static Expression GenerateEqualExpression(Parameter parameter, Type elementType, string leftExpressionProperty, object value)
        {
            Expression innerLeft = Expression.Property(parameter.GetExpression(), elementType.GetProperty(leftExpressionProperty));
            Expression innerLambda = Expression.Equal(innerLeft, GetValueExpression(value, innerLeft.Type));
            var innerFunction = typeof(Func<,>).MakeGenericType(elementType, typeof(bool));
            return Expression.Lambda(innerFunction, innerLambda, parameter.GetExpression());
        }

        private static Expression GetValueExpression(object value, Type type)
        {
            return Expression.Convert(Expression.Constant(value), type);
        }

        private static List<string> GetLeftExpressionProperties(string targetProperty)
        {
            return targetProperty.Split('.').ToList();
        }
    }
}
