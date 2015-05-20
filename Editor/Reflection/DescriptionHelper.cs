namespace Orbital.Reflection
{
    internal static class DescriptionHelper
    {
        #region Assembly Methods

        public static void ExtractNames(string fullname, out string namespaceName, out string typeName)
        {
            namespaceName = string.Empty;
            typeName = string.Empty;

            string[] splitname = fullname.Split('.');
            if (splitname.Length == 0)
                return;

            if (splitname.Length > 1)
                namespaceName = string.Join(".", splitname, 0, splitname.Length - 1);

            typeName = splitname[splitname.Length - 1];
        }

        #endregion
    }
}
