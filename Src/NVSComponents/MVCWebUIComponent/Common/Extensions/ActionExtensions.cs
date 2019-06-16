using System;
using JetBrains.Annotations;

namespace Volvo.LAT.MVCWebUIComponent.Common.Extensions
{
    /// <summary>
    /// Provides extensions method to Mvc actions.
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// Converts an action with its arguments into a pure action name.
        /// </summary>
        /// <param name="action">An action name with optional arguments to be converted.</param>
        /// <returns>A pure action name with no action argument values.</returns>
        public static string ToActionName([NotNull] this string action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            // When we have a pure action already (with no arguments) then no need to do anything.
            int index = action.IndexOf("/", StringComparison.InvariantCulture);
            if (index == -1)
            {
                return action;
            }

            return action.Substring(0, index);
        }

        /// <summary>
        /// Converts an action with its arguments into an action arguments value string.
        /// </summary>
        /// <param name="action">An action name with optional arguments to be converted.</param>
        /// <returns>An action arguments string (with no action name).</returns>
        public static string ToActionValue(this string action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            // When we have a pure action already (with no arguments) then no need to do anything.
            int index = action.IndexOf("/", StringComparison.InvariantCulture);
            if (index == -1)
            {
                return null;
            }

            return action.Substring(index + 1, action.Length - index - 1);
        }
    }
}