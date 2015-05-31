using System;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;

using Orbital.Source;

namespace Orbital.UI
{
    public sealed class VisualSelector
    {
        #region Fields

        private static readonly Dictionary<FilterType, Func<object, object, bool>> FiltersFunc = 
            new Dictionary<FilterType, Func<object, object, bool>>
        {
            { FilterType.EqualsTo, EqualsTo },
            { FilterType.StartsWith, StartsWith },
            { FilterType.EndsWith, EndsWith },
            { FilterType.In, In }
        };

        private static readonly Dictionary<Expression<Func<IDataSource, object>>, Func<IDataSource, object>> CachedExpression = 
            new Dictionary<Expression<Func<IDataSource, object>>, Func<IDataSource, object>>();

        private readonly Type _visualType;
        private readonly List<SelectorFilter> _filters = new List<SelectorFilter>();

        #endregion

        #region Properties

        public Type VisualType
        {
            get { return _visualType; }
        }

        #endregion

        #region Constructors

        public VisualSelector(Type visualType)
        {
            if (!visualType.GetInterfaces().Any(c => c == typeof(IVisual)))
                throw new ArgumentException("Provided type does not implement IVisual");

            _visualType = visualType;
        }

        #endregion

        #region Filter Methods

        public void EqualsTo(Expression<Func<IDataSource, object>> member, object value)
        {
            AddFilter(GetFilterFunc(FilterType.EqualsTo), GetCompiledExpression(member), value);
        }

        public void StartsWith(Expression<Func<IDataSource, object>> member, string value)
        {
            AddFilter(GetFilterFunc(FilterType.StartsWith), GetCompiledExpression(member), value);
        }

        public void EndsWith(Expression<Func<IDataSource, object>> member, string value)
        {
            AddFilter(GetFilterFunc(FilterType.EndsWith), GetCompiledExpression(member), value);
        }

        public void In(Expression<Func<IDataSource, object>> member, IEnumerable value)
        {
            AddFilter(GetFilterFunc(FilterType.In), GetCompiledExpression(member), value);
        }

        private void AddFilter(Func<object, object, bool> filterFunc, Func<IDataSource, object> memberFunc, object value)
        {
            InternalSelectorFilter filter = new InternalSelectorFilter(filterFunc, memberFunc, value);
            _filters.Add(filter);
        }

        #endregion

        #region Internal Filter Methods

        private static Func<object, object, bool> GetFilterFunc(FilterType filterType)
        {
            Func<object, object, bool> result;
            if (!FiltersFunc.TryGetValue(filterType, out result))
                return null;

            return result;
        }

        private static bool EqualsTo(object obj1, object obj2)
        {
            return obj1 == obj2;
        }

        private static bool StartsWith(object obj1, object obj2)
        {
            return ((string)obj1).StartsWith((string)obj2);
        }

        private static bool EndsWith(object obj1, object obj2)
        {
            return ((string)obj1).EndsWith((string)obj2);
        }

        private static bool In(object obj1, object obj2)
        {
            return ((IEnumerable)obj2).Cast<object>().Contains(obj1);
        }

        #endregion

        #region Expression Cache Methods

        private static Func<IDataSource, object> GetCompiledExpression(Expression<Func<IDataSource, object>> expression)
        {
            Func<IDataSource, object> result;
            if(!CachedExpression.TryGetValue(expression, out result))
            {
                result = expression.Compile();
                CachedExpression[expression] = result;
            }

            return result;
        }

        #endregion
    }
}