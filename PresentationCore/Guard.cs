using System;

namespace WPFPresentationCore
{
    /// <summary>
    /// Internal null guard class
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Ensures the specified string is not null or empty
        /// </summary>
        /// <param name="s">The string to test.</param>
        /// <param name="propertyName">Name of the property.</param>
        public static void NotNullOrEmpty(string s, string propertyName)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new NullReferenceException("'" + propertyName + "' is null or emtpy.");
            }
        }

        /// <summary>
        /// Ensures the specified object is not null
        /// </summary>
        /// <param name="o">The object to test.</param>
        /// <param name="propertyName">Name of the property.</param>
        public static void NotNull(object o, string propertyName)
        {
            if (o == null)
            {
                throw new NullReferenceException("'" + propertyName + "' is null.");
            }
        }

        public static bool IsNull(object o)
        {
            return o == null;
        }
    }
}