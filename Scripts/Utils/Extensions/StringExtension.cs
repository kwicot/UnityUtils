namespace Kwicot.Core.Scripts.Utils.Extensions
{
    public static class StringExtension
    {
        public static bool IsNull(this string value) => string.IsNullOrEmpty(value);
        public static bool IsNotNull(this string value) => !string.IsNullOrEmpty(value);
        public static bool IsEmpty(this string value) => string.IsNullOrEmpty(value);
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
        public static bool IsNotEmpty(this string value) => !IsEmpty(value);
        public static bool IsNotNullOrWhiteSpace(this string value) => !IsNullOrWhiteSpace(value);
        
    }
}