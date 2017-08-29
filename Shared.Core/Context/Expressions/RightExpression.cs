using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Context.Expressions
{
    public class RightExpression
    {
        Expression _expression;

        private RightExpression(object value, Type type)
        {
            _expression = Expression.Convert(Expression.Constant(value), type);
        }

        public Expression GetExpression()
        {
            return _expression;
        }

        public static RightExpression Create(object value, LeftExpression left)
        {
            return new RightExpression(value, left.GetExpression().Type);
        }
    }
}
