using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa.TagHelpers
{
    public class Inlinesvg : TagHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        // PascalCase gets translated into kebab-case.
        public String Src { get; set; }
        
        public Inlinesvg(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;
            //output.TagMode = TagMode.SelfClosing;
            //output.Content.Clear();
            output.Content.SetHtmlContent(System.IO.File.ReadAllText(System.IO.Path.Combine(_webHostEnvironment.WebRootPath, Src)));
        }
    }
}
