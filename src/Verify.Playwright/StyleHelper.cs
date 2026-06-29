static class StyleHelper
{
    public static Task RemovePlaywrightStyle(this IPage page) =>
        page.EvaluateAsync(
            """
            () => {
                for (const style of document.querySelectorAll('style')) {
                    if (style.innerHTML.includes('*:not(#playwright')) {
                        style.remove();
                    }
                }
            }
            """);
}