using System;
using System.Diagnostics.CodeAnalysis;
using MyMoney.models;

namespace MyMoney.extensions
{
    [ExcludeFromCodeCoverage]
    public static class StringExtension
    {
        public static Month ToMonth(this string value)
        {
            return Enum.TryParse(value, true, out Month result) ? result : default;
        }
    }
}