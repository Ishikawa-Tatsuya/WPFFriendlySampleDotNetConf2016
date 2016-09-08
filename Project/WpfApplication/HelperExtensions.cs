using Reactive.Bindings;

namespace WpfApplication
{
    public static class HelperExtensions
    {
        public static bool IsNullOrEmpty(this ReactiveProperty<string> text)
            => string.IsNullOrEmpty(text.Value);

        public static bool IsNullOrEmpty(this string text)
            => string.IsNullOrEmpty(text);
    }
}
