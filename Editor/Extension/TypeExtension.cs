using System;
using System.Linq;

namespace Orbital.Extension
{
    public static class TypeExtension
    {
        #region Extension Methods

        public static bool ImplementsInterfaces(this Type type, params Type[] types)
        {
            Type[] interfaces = type.GetInterfaces();

            return interfaces.Intersect(types).Any();
        }

        public static string GetGenericsName(this Type type)
        {
            string result = type.Name;
            if (!type.IsGenericType)
                return result;

            int genericSym = result.IndexOf('`');
            if (genericSym > 0)
                result = result.Remove(genericSym);

            result += "<";

            Type[] typeParameters = type.GetGenericArguments();
            result += string.Join(",", typeParameters.Select(c => c.GetGenericsName()).ToArray());

            result += ">";

            return result;
        }

        #endregion
    }
}