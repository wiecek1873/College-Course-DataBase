#pragma checksum "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "317b368b792c94579a4d89a57c91a44d119472cd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ViewAllTopics), @"mvc.1.0.view", @"/Views/Home/ViewAllTopics.cshtml")]
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
#line 1 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\_ViewImports.cshtml"
using Bazadanych;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\_ViewImports.cshtml"
using Bazadanych.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"317b368b792c94579a4d89a57c91a44d119472cd", @"/Views/Home/ViewAllTopics.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"60978b1f624cfd294b6acfcdb70b5064b37846c6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ViewAllTopics : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Bazadanych.Models.TopicModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
  
    ViewData["Title"] = "ViewAllTopics";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>All topics</h1>\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 13 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayNameFor(model => model.Maininformation));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 16 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayNameFor(model => model.VoteStart));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 19 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayNameFor(model => model.VoteEnd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 22 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayNameFor(model => model.OptionA));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 25 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayNameFor(model => model.OptionB));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 31 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
 foreach (var item in Model) {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 34 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayFor(modelItem => item.Maininformation));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayFor(modelItem => item.VoteStart));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 40 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayFor(modelItem => item.VoteEnd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 43 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayFor(modelItem => item.OptionA.Information));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 46 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
           Write(Html.DisplayFor(modelItem => item.OptionB.Information));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 49 "C:\Users\wiece\Desktop\Sem6\BD\Program\Views\Home\ViewAllTopics.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Bazadanych.Models.TopicModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
