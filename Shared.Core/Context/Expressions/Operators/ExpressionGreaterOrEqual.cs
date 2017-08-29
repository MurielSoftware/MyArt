using Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Context.Expressions.Operators
{
    public class ExpressionGreaterOrEqual : IExpressionOperator
    {
        public Expression Generate(Parameter parameter, LeftExpression left, RightExpression right, FilterAttribute filterAttribute, object value)
        {
            return Expression.GreaterThanOrEqual(left.GetExpression(), right.GetExpression());
        }
    }
}
