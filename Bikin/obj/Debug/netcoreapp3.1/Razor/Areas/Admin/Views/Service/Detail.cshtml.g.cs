#pragma checksum "C:\Users\user\source\repos\Bikin\Bikin\Areas\Admin\Views\Service\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e63019fa690dbbdde0d6da842ed55ad4873b29ea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Service_Detail), @"mvc.1.0.view", @"/Areas/Admin/Views/Service/Detail.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\user\source\repos\Bikin\Bikin\Areas\Admin\Views\_ViewImports.cshtml"
using Bikin.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\user\source\repos\Bikin\Bikin\Areas\Admin\Views\_ViewImports.cshtml"
using Bikin.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\user\source\repos\Bikin\Bikin\Areas\Admin\Views\_ViewImports.cshtml"
using Bikin.Areas.Admin.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e63019fa690dbbdde0d6da842ed55ad4873b29ea", @"/Areas/Admin/Views/Service/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8c3ce33a52f31ca2b79e2439814d9f59821f2998", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Service_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Service>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div>\r\n    <div>\r\n        <p>Id:</p>\r\n        <p>");
#nullable restore
#line 5 "C:\Users\user\source\repos\Bikin\Bikin\Areas\Admin\Views\Service\Detail.cshtml"
      Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n    <div>\r\n        <p>Icon:</p>\r\n        <i");
            BeginWriteAttribute("class", " class=\"", 135, "\"", 154, 1);
#nullable restore
#line 9 "C:\Users\user\source\repos\Bikin\Bikin\Areas\Admin\Views\Service\Detail.cshtml"
WriteAttributeValue("", 143, Model.Icon, 143, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("></i>\r\n    </div>\r\n    <div>\r\n        <p>Title:</p>\r\n        <p>");
#nullable restore
#line 13 "C:\Users\user\source\repos\Bikin\Bikin\Areas\Admin\Views\Service\Detail.cshtml"
      Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n    <div>\r\n        <p>Description:</p>\r\n        <p>");
#nullable restore
#line 17 "C:\Users\user\source\repos\Bikin\Bikin\Areas\Admin\Views\Service\Detail.cshtml"
      Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Service> Html { get; private set; }
    }
}
#pragma warning restore 1591
