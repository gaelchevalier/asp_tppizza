using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace TPPizza.TagHelpers
{
    public class SubmitTagHelper : TagHelper
    {
        public string? Label {get; set;}
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.SetAttribute("type", "submit");
            output.Attributes.SetAttribute("class", "btn btn-primary mt-3");
            output.Attributes.SetAttribute("value", Label);
            output.Content.SetContent(Label);
        }
    }
}
