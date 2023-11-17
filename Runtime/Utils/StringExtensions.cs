using System;

namespace Slimebones.ECSCore.Utils
{
    public static class StringExtensions
    {
        public static string Capitalize(this string input)
        {
            // ref: https://stackoverflow.com/a/4405876

            switch (input)
            {
                case null:
                    throw new ArgumentNullException(nameof(input));
                case "":
                    throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}