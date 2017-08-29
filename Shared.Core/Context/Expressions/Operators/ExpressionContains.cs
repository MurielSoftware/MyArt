using Shared.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Context.Expressions.Operators
{
    public class ExpressionContains : IExpressionOperator
    {
        public const string CONTAINS_METHOD_NAME = "Contains";

        public Expression Generate(Parameter parameter, LeftExpression left, RightExpression right, FilterAttribute filterAttribute, object value)
        {
            return Expression.Call(left.GetExpression(), typeof(string).GetMethod(CONTAINS_METHOD_NAME, new[] { typeof(string) }), right.GetExpression());
        }
    }
}
