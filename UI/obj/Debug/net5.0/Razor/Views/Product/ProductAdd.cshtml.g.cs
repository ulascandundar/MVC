#pragma checksum "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2013c4c40388e6eb2d952bff23c147a20e12de82"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_ProductAdd), @"mvc.1.0.view", @"/Views/Product/ProductAdd.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2013c4c40388e6eb2d952bff23c147a20e12de82", @"/Views/Product/ProductAdd.cshtml")]
    public class Views_Product_ProductAdd : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Entities.Concrete.Product>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
  
    ViewData["Title"] = "ProductAdd";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Ürün Ekleme</h1>\r\n<br />\r\n");
#nullable restore
#line 9 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
 using (Html.BeginForm("ProductAdd", "Product", FormMethod.Post))
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
Write(Html.Label("Ürün İsmi"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
Write(Html.TextBoxFor(x => x.Name, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\r\n");
#nullable restore
#line 14 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
Write(Html.Label("Fiyatı"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
Write(Html.TextBoxFor(x => x.Price, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\r\n");
#nullable restore
#line 17 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
Write(Html.Label("Stok"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
Write(Html.TextBoxFor(x => x.Stock, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\r\n");
#nullable restore
#line 20 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
Write(Html.Label("Kategori"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
Write(Html.DropDownListFor(x => x.CategoryId, (List<SelectListItem>)ViewBag.v1, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("    <button class=\"btn btn-info\">Ekle</button>\r\n");
#nullable restore
#line 23 "C:\Users\pc\source\repos\ERYAZ\UI\Views\Product\ProductAdd.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Entities.Concrete.Product> Html { get; private set; }
    }
}
#pragma warning restore 1591