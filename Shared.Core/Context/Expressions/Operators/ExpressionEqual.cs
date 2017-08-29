using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shared.Core.Attributes;

namespace Shared.Core.Context.Expressions.Operators
{
    public class ExpressionEqual : IExpressionOperator
    {
        public Expression Generate(Parameter parameter, LeftExpression left, RightExpression right, FilterAttribute filterAttribute, object value)
        {
            return Expression.Equal(left.GetExpression(), right.GetExpression());
        }
    }
}
