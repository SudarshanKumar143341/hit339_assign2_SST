#pragma checksum "C:\Users\sandi\source\repos\hit339_assign2_SST3\TennisForEveryone\Areas\Identity\Pages\Account\Logout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "acf321d273a4b4dd8650c17231b06987dc03df69"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Identity_Pages_Account_Logout), @"mvc.1.0.razor-page", @"/Areas/Identity/Pages/Account/Logout.cshtml")]
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
#line 1 "C:\Users\sandi\source\repos\hit339_assign2_SST3\TennisForEveryone\Areas\Identity\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sandi\source\repos\hit339_assign2_SST3\TennisForEveryone\Areas\Identity\Pages\_ViewImports.cshtml"
using TennisForEveryone.Areas.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sandi\source\repos\hit339_assign2_SST3\TennisForEveryone\Areas\Identity\Pages\_ViewImports.cshtml"
using TennisForEveryone.Areas.Identity.Pages;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\sandi\source\repos\hit339_assign2_SST3\TennisForEveryone\Areas\Identity\Pages\_ViewImports.cshtml"
using TennisForEveryone.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\sandi\source\repos\hit339_assign2_SST3\TennisForEveryone\Areas\Identity\Pages\Account\_ViewImports.cshtml"
using TennisForEveryone.Areas.Identity.Pages.Account;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"acf321d273a4b4dd8650c17231b06987dc03df69", @"/Areas/Identity/Pages/Account/Logout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3cba7fc89b68cbf11fa70058bdfc10eab07c9f99", @"/Areas/Identity/Pages/_ViewImports.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"01ff0693c13c52fa3e9e0584e354980dd01d5cdb", @"/Areas/Identity/Pages/Account/_ViewImports.cshtml")]
    public class Areas_Identity_Pages_Account_Logout : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\sandi\source\repos\hit339_assign2_SST3\TennisForEveryone\Areas\Identity\Pages\Account\Logout.cshtml"
  
    ViewData["Title"] = "Log out";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<header>\r\n    <h1>");
#nullable restore
#line 8 "C:\Users\sandi\source\repos\hit339_assign2_SST3\TennisForEveryone\Areas\Identity\Pages\Account\Logout.cshtml"
   Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    <p>You have successfully logged out of the application.</p>\r\n</header>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LogoutModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LogoutModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<LogoutModel>)PageContext?.ViewData;
        public LogoutModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
