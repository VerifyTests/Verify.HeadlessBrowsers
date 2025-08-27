static class StyleHelper
{
    public static async Task RemovePlaywrightStyle(this IPage page)
    {
        var elements = await page.QuerySelectorAllAsync("style");
        foreach (var element in elements)
        {
            var value = await element.InnerHTMLAsync();
            if (value.Contains("*:not(#playwright"))
            {
                await element.EvaluateAsync("element => element.remove()", element);
            }
        }
    }
}