using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ProjectSemester3.Services;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("navbar", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class NavBarTag : TagHelper
    {
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        private IHtmlHelper htmlHelper;
        private readonly IAccountService accountService;
    
        public NavBarTag(IHtmlHelper htmlHelper, IAccountService _accountService)
        {
            this.htmlHelper = htmlHelper;
            this.accountService = _accountService;
            
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (htmlHelper as IViewContextAware).Contextualize(ViewContext);
            if (ViewContext.HttpContext.Session.GetString("username") != null && ViewContext.HttpContext.Session.GetString("role") != null)
            {
                htmlHelper.ViewBag.account = accountService.Find(ViewContext.HttpContext.Session.GetString("username"));
            }

            output.TagName = "";
            output.Content.SetHtmlContent(await htmlHelper.PartialAsync("TagHelpers/NavBar/Index"));
        }
    }
}
