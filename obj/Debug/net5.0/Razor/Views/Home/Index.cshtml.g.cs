#pragma checksum "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ccae0675476a359c11ae9b57fa2c8f325df299c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ccae0675476a359c11ae9b57fa2c8f325df299c7", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"88c69e937a5b103983807108153584045158061e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/itsxdipux/Projects/ASP.NET/GoDam/Views/Home/Index.cshtml"
  

    ViewData["Title"] = "Home Page";
    Layout = "~/Views/Shared/landing.cshtml";


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<main>
    <section class=""hero"">
        <div class=""container"">
            <div class=""hero-inner"">
                <div class=""hero-copy"">
                    <h1 class=""hero-title mt-0"">GoDam-Manage your stock.</h1>
                    <p class=""hero-paragraph"">A convinient place to manage and keep track of your purchase reports, sales report, and products in your store.</p>
                    <div class=""hero-cta""><a class=""button button-primary"" href=""/Identity/Account/Login"">Log in</a><");
            WriteLiteral(@"</div>
                </div>
                <div class=""hero-figure anime-element"">
                    <svg class=""placeholder"" width=""528"" height=""396"" viewBox=""0 0 528 396"">
                        <rect width=""528"" height=""396"" style=""fill:transparent;"" />
                    </svg>
                    <div class=""hero-figure-box hero-figure-box-01"" data-rotation=""45deg""></div>
                    <div class=""hero-figure-box hero-figure-box-02"" data-rotation=""-45deg""></div>
                    <div class=""hero-figure-box hero-figure-box-03"" data-rotation=""0deg""></div>
                    <div class=""hero-figure-box hero-figure-box-04"" data-rotation=""-135deg""></div>
                    <div class=""hero-figure-box hero-figure-box-05""></div>
                    <div class=""hero-figure-box hero-figure-box-06""></div>
                    <div class=""hero-figure-box hero-figure-box-07""></div>
                    <div class=""hero-figure-box hero-figure-box-08"" data-rotation=""-22deg""></div>
       ");
            WriteLiteral(@"             <div class=""hero-figure-box hero-figure-box-09"" data-rotation=""-52deg""></div>
                    <div class=""hero-figure-box hero-figure-box-10"" data-rotation=""-50deg""></div>
                </div>
            </div>
        </div>
    </section>

    <section class=""features section"">
        <div class=""container"">
            <div class=""features-inner section-inner has-bottom-divider"">
                <div class=""features-wrap"">
                    <div class=""feature text-center is-revealing"">
                        <div class=""feature-inner"">
                            <div class=""feature-icon"">
                                <img src=""dist/images/feature-icon-01.svg"" alt=""Feature 01"">
                            </div>
                            <h4 class=""feature-title mt-24"">Keep track of all the product in your stock.</h4>
                            <p class=""text-sm mb-0"">The system will help to keep track if any item is running out of stock or is not selling at ");
            WriteLiteral(@"all.</p>
                        </div>
                    </div>
                    <div class=""feature text-center is-revealing"">
                        <div class=""feature-inner"">
                            <div class=""feature-icon"">
                                <img src=""dist/images/feature-icon-02.svg"" alt=""Feature 02"">
                            </div>
                            <h4 class=""feature-title mt-24"">Create your own category.</h4>
                            <p class=""text-sm mb-0"">The system will allow users to cteate, edit and delete a category for the products and assign those category to the products.</p>
                        </div>
                    </div>
                    <div class=""feature text-center is-revealing"">
                        <div class=""feature-inner"">
                            <div class=""feature-icon"">
                                <img src=""dist/images/feature-icon-03.svg"" alt=""Feature 03"">
                            </div>
     ");
            WriteLiteral(@"                       <h4 class=""feature-title mt-24"">Sort products into various categories.</h4>
                            <p class=""text-sm mb-0"">User can sort the products assigned to various categories and view them according to those categories.</p>
                        </div>
                    </div>
                    <div class=""feature text-center is-revealing"">
                        <div class=""feature-inner"">
                            <div class=""feature-icon"">
                                <img src=""dist/images/feature-icon-04.svg"" alt=""Feature 04"">
                            </div>
                            <h4 class=""feature-title mt-24"">Keep track of your customers.</h4>
                            <p class=""text-sm mb-0"">The system allows user to save all the personal information any Customer including any purchases made within last 31 days.</p>
                        </div>
                    </div>
                    <div class=""feature text-center is-reveal");
            WriteLiteral(@"ing"">
                        <div class=""feature-inner"">
                            <div class=""feature-icon"">
                                <img src=""dist/images/feature-icon-05.svg"" alt=""Feature 05"">
                            </div>
                            <h4 class=""feature-title mt-24"">Add or remove products into the system.</h4>
                            <p class=""text-sm mb-0"">The system will allow users to cteate, edit and delete any product into the system and keep track of those product and inform user if the product is running out of stock.</p>
                        </div>
                    </div>
                    <div class=""feature text-center is-revealing"">
                        <div class=""feature-inner"">
                            <div class=""feature-icon"">
                                <img src=""dist/images/feature-icon-06.svg"" alt=""Feature 06"">
                            </div>
                            <h4 class=""feature-title mt-24"">Keep everything i");
            WriteLiteral(@"n your own hands.</h4>
                            <p class=""text-sm mb-0"">The system is userfriendly and accomodates all the needs of the user and loows user to handle all the various part of the system.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</main>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
