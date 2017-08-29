using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Context.Expressions
{
    public class Parameter
    {
        private const string _name = "x";
        private int _index = 0;
        private Type _parameterType;
        private ParameterExpression _parameterExpression;

        public Parameter(Type parameterType)
        {
            _parameterType = parameterType;
            _parameterExpression = Expression.Parameter(_parameterType, ToString());
        }

        public Parameter(Type parameterType, int index)
        {
            _parameterType = parameterType;
            _index = index;
            _parameterExpression = Expression.Parameter(_parameterType, ToString());
        }

        public int GetIndex()
        {
            return _index;
        }

        public Type GetParameterType()
        {
            return _parameterType;
        }

        public ParameterExpression GetExpression()
        {
            return _parameterExpression;
        }

        public static Parameter CreateNestedParameter(Parameter parameter)
        {
            return new Parameter(parameter.GetParameterType(), parameter.GetIndex());
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}", _name, _index);
        }
    }
}
