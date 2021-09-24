using DinkToPdf;
using DinkToPdf.Contracts;
using new_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace new_project.Services.CustomerContractService
{
    public class ContractServiceForPdf : ContractService
    {
        private readonly IConverter _converter;
        public ContractServiceForPdf(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] GeneratePdfContract(Project project)
        {
            var htmlFile = $"<p>Project Name: {project.name.ToString()}</p>" 
                +  "<p> Email:</p>"  
                +  $"<p>Budget: {project.budget.ToString()}</p>" 
                +  $"<p>StartDate: {project.dateOfStart.ToString()}</p>" 
                +  $"<p>EndDate:{project.dateOfFinish.ToString()}</p>" 
                +  "<p><br></p>" 
                +  "<p>This is demonstration contact only.</p>";

            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 25, Bottom = 25 };
            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = htmlFile;
            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";
            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 15;
            headerSettings.FontName = "Ariel";
            headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Line = true;
            FooterSettings footerSettings = new FooterSettings();
            footerSettings.FontSize = 12;
            footerSettings.FontName = "Ariel";
            footerSettings.Center = "This is for demonstration purposes only.";
            footerSettings.Line = true;
            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            return _converter.Convert(htmlToPdfDocument);
        }

    }
}
