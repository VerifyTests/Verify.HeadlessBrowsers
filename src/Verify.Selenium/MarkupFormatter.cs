using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Html.Dom;

class MarkupFormatter : PrettyMarkupFormatter
{
    public MarkupFormatter()
    {
        Indentation = "  ";
    }

    public override string OpenTag(IElement element, bool selfClosing)
    {
        if (ShouldExcludeElement(element))
        {
            return "";
        }
        return base.OpenTag(element, selfClosing);
    }

    private static bool ShouldExcludeElement(IElement element)
    {
        return element is IHtmlHeadElement;
    }

    public override string CloseTag(IElement element, bool selfClosing)
    {
        if (ShouldExcludeElement(element))
        {
            return "";
        }
        return base.CloseTag(element, selfClosing);
    }

    protected override string Attribute(IAttr attr)
    {
        var name = attr.Name;

        var value = attr.Value;
        switch (name)
        {
            case "class":
            {
                var values = value.Split(' ')
                    .Where(x => !x.StartsWith("uno-"))
                    .ToList();
                if (!values.Any())
                {
                    return "";
                }

                attr.Value = string.Join(" ", values);
                break;
            }
            case "xamltype":
            {
                return base.Attribute(new Attr("xaml", value.Substring(value.LastIndexOf(".") + 1)));
            }
            case "xuid":
            {
                return base.Attribute(new Attr("id", value));
            }
        }

        return base.Attribute(attr);
    }
}