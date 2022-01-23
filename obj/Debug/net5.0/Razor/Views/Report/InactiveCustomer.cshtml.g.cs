#pragma checksum "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Report/InactiveCustomer.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "78e1989c83b534a02a244584588fd040f74d94e1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Report_InactiveCustomer), @"mvc.1.0.view", @"/Views/Report/InactiveCustomer.cshtml")]
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
#line 1 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/_ViewImports.cshtml"
using GoDam;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/_ViewImports.cshtml"
using GoDam.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"78e1989c83b534a02a244584588fd040f74d94e1", @"/Views/Report/InactiveCustomer.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"88c69e937a5b103983807108153584045158061e", @"/Views/_ViewImports.cshtml")]
    public class Views_Report_InactiveCustomer : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<GoDam.ViewModels.InactiveCustomerViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Report/InactiveCustomer.cshtml"
  
    ViewData["Title"] = "Inactive Customer";
    Layout = "~/Views/Shared/layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container-fluid"">
    <h4>List of Customers who have not Purchased any item in last 31 days</h4>
    <table class=""table table-hover table-bordered table-striped"">
        <thead class=""thead-dark"">
            <tr>

                <th>
                    CustomerID
                </th>
                <th>
                    Name
                </th>
                <th>
                    Last purchased on
                </th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 25 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Report/InactiveCustomer.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 29 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Report/InactiveCustomer.cshtml"
                   Write(Html.DisplayFor(modelItem => item.CustomerID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 32 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Report/InactiveCustomer.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 35 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Report/InactiveCustomer.cshtml"
                   Write(Html.DisplayFor(modelItem => item.LastPurchasedON));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 38 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Report/InactiveCustomer.cshtml"

            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<GoDam.ViewModels.InactiveCustomerViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
