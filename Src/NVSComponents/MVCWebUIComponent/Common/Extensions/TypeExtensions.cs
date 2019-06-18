using System;

namespace Volvo.LAT.MVCWebUIComponent.Common.Extensions
{
    /// <summary>
    /// Provides extension methods into the <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// A name in which each of the controller classes should end.
        /// </summary>
        private const string ControllerTypeName = "Controller";

        /// <summary>
        /// A length of the controller name in which every controller class should ends.
        /// </summary>
        private static readonly int controllerTypeNameLength = ControllerTypeName.Length;

        /// <summary>
        /// Converts the current type name into the Mvc controller name.
        /// </summary>
        /// <param name="type">A type which is the Mvc controller.</param>
        /// <returns>A name of the mvc controller (with no Controller text in it).</returns>
        public static string ToControllerName(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            // If we do not have a controller included in the name we are assuming we have the name already.
            string name = type.Name;
            if (!name.EndsWith(ControllerTypeName))
            {
                return name;
            }

            return name.Substring(0, name.Length - controllerTypeNameLength);
        }
    }
}