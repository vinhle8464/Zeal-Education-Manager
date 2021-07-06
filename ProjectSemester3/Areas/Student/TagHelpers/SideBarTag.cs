using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("sidebar", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SideBarTag : TagHelper
    {

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        private IHtmlHelper htmlHelper;

        public SideBarTag(IHtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (htmlHelper as IViewContextAware).Contextualize(ViewContext);


            output.TagName = "";
            output.Content.SetHtmlContent(await htmlHelper.PartialAsync("TagHelpers/SideBar/Index"));
        }
    }
}
