#pragma checksum "C:\Users\simon\Desktop\Teamber\AspNetCore.Docs\aspnetcore\data\ef-rp\intro\samples\cu30\Pages\About.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4779e2eea869f44e89aeb73ad8d841f65f728924"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ContosoUniversity.Pages.Pages_About), @"mvc.1.0.razor-page", @"/Pages/About.cshtml")]
namespace ContosoUniversity.Pages
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
#line 1 "C:\Users\simon\Desktop\Teamber\AspNetCore.Docs\aspnetcore\data\ef-rp\intro\samples\cu30\Pages\_ViewImports.cshtml"
using ContosoUniversity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4779e2eea869f44e89aeb73ad8d841f65f728924", @"/Pages/About.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4e3d676151f6d39034ea3a39737612183bc36e14", @"/Pages/_ViewImports.cshtml")]
    public class Pages_About : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 4 "C:\Users\simon\Desktop\Teamber\AspNetCore.Docs\aspnetcore\data\ef-rp\intro\samples\cu30\Pages\About.cshtml"
  
    ViewData["Title"] = "Student Body Statistics";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h2>Student Body Statistics</h2>\n\n<table>\n    <tr>\n        <th>\n            Enrollment Date\n        </th>\n        <th>\n            Employees\n        </th>\n    </tr>\n\n");
#nullable restore
#line 20 "C:\Users\simon\Desktop\Teamber\AspNetCore.Docs\aspnetcore\data\ef-rp\intro\samples\cu30\Pages\About.cshtml"
     foreach (var item in Model.Employees)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\n            <td>\n                ");
#nullable restore
#line 24 "C:\Users\simon\Desktop\Teamber\AspNetCore.Docs\aspnetcore\data\ef-rp\intro\samples\cu30\Pages\About.cshtml"
           Write(Html.DisplayFor(modelItem => item.EnrollmentDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n            <td>\n                ");
#nullable restore
#line 27 "C:\Users\simon\Desktop\Teamber\AspNetCore.Docs\aspnetcore\data\ef-rp\intro\samples\cu30\Pages\About.cshtml"
           Write(item.EmployeeCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            </td>\n        </tr>\n");
#nullable restore
#line 30 "C:\Users\simon\Desktop\Teamber\AspNetCore.Docs\aspnetcore\data\ef-rp\intro\samples\cu30\Pages\About.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ContosoUniversity.Pages.AboutModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ContosoUniversity.Pages.AboutModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ContosoUniversity.Pages.AboutModel>)PageContext?.ViewData;
        public ContosoUniversity.Pages.AboutModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591