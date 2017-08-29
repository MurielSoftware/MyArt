using Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Context.Expressions.Operators
{
    public class ExpressionLessOrEqual : IExpressionOperator
    {
        public Expression Generate(Parameter parameter, LeftExpression left, RightExpression right, FilterAttribute filterAttribute, object value)
        {
            return Expression.LessThanOrEqual(left.GetExpression(), right.GetExpression());
        }
    }
}
