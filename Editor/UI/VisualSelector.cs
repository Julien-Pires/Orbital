using System;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;
using System.Collections.Generic;

using Orbital.Data;

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

        private static readonly Dictionary<Expression<Func<IValueSource, object>>, Func<IValueSource, object>> CachedExpression = 
            new Dictionary<Expression<Func<IValueSource, object>>, Func<IValueSource, object>>();

        private readonly Type _visualType;
        private readonly List<SelectorFilter> _filters = new List<SelectorFilter>();

        #endregion

        #region Properties

        public Type VisualType
        {
            get { return _visualType; }
        }

        internal bool IsBuiltIn { get; set; }

        #endregion

        #region Constructors

        public VisualSelector(Type visualType)
        {
            if (!visualType.GetInterfaces().Any(c => c == typeof(BaseViual)))
                throw new ArgumentException("Provided type does not implement IVisual");

            _visualType = visualType;
        }

        #endregion

        #region Filter Methods

        internal int Filter(IValueSource source)
        {
            int result = 0;
            for (int i = 0; i < _filters.Count; i++)
                result += _filters[i].Filter(source) ? 1 : 0;

            return result;
        }

        public void EqualsTo(Expression<Func<IValueSource, object>> member, object value)
        {
            AddFilter(GetFilterFunc(FilterType.EqualsTo), GetCompiledExpression(member), value);
        }

        public void StartsWith(Expression<Func<IValueSource, object>> member, string value)
        {
            AddFilter(GetFilterFunc(FilterType.StartsWith), GetCompiledExpression(member), value);
        }

        public void EndsWith(Expression<Func<IValueSource, object>> member, string value)
        {
            AddFilter(GetFilterFunc(FilterType.EndsWith), GetCompiledExpression(member), value);
        }

        public void In(Expression<Func<IValueSource, object>> member, IEnumerable value)
        {
            AddFilter(GetFilterFunc(FilterType.In), GetCompiledExpression(member), value);
        }

        private void AddFilter(Func<object, object, bool> filterFunc, Func<IValueSource, object> memberFunc, object value)
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

        private static Func<IValueSource, object> GetCompiledExpression(Expression<Func<IValueSource, object>> expression)
        {
            // TODO : Implement expression caching
            return expression.Compile();
        }

        #endregion
    }
}