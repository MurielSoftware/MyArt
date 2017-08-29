using Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Context.Expressions
{
    public class LeftExpression
    {
        private Expression _expression;

        private LeftExpression(Parameter parameter, FilterAttribute filterAttribute)
        {
            string[] leftExpressionProperties = GetLeftExpressionProperties(filterAttribute.TargetProperty);
            Expression previousExpression = null;
            Expression nextExpression = null;

            foreach (string leftExpressionProperty in leftExpressionProperties)
            {
                if (previousExpression == null)
                {
                    nextExpression = Expression.Property(parameter.GetExpression(), leftExpressionProperty);
                }
                else
                {
                    nextExpression = Expression.Property(previousExpression, leftExpressionProperty);
                }
                previousExpression = nextExpression;
            }
            _expression = nextExpression;
        }

        public Expression GetExpression()
        {
            return _expression;
        }

        public static LeftExpression Create(Parameter parameter, FilterAttribute filterAttribute)
        {
            return new LeftExpression(parameter, filterAttribute);
        }

        private static string[] GetLeftExpressionProperties(string targetProperty)
        {
            return targetProperty.Split('.');
        }
    }
}
