using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopIt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PopIt.Controllers
{
    public class FileUpController : Controller
    {

        IHostingEnvironment _hostingEnvironment = null;
        public FileUpController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index(string filename = "")
        {
            FileClass fileObj = new FileClass();
            fileObj.Name = filename;

            string path = $"{_hostingEnvironment.WebRootPath}\\files\\";
            int nId = 1;

            foreach (string pdfPath in Directory.EnumerateFiles(path, "*.pdf"))
            {
                fileObj.Files.Add(new FileClass()
                {
                    FileId = nId++,
                    Name = Path.GetFileName(pdfPath),
                    path = pdfPath
                });
            }
            return View(fileObj);
        }

        [HttpPost]

        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return Index();
        }

        public IActionResult PDFViewerNewTab(string fileName)
        {
            string path = _hostingEnvironment.WebRootPath + "\\files\\" + fileName;
            return File(System.IO.File.ReadAllBytes(path), "application/pdf");
        }

        public IActionResult student(string fileName)
        {

            //string path = _hostingEnvironment.WebRootPath + "\\files\\" + fileName;
            //return File(System.IO.File.ReadAllBytes(path), "application/pdf");
            return Index();
        }

        //public IActionResult PDFViewerNewTab1(string fileName)
        //{
        //    string path = _hostingEnvironment.WebRootPath + "\\files\\" + fileName;
        //    return File(System.IO.File.ReadAllBytes(path), "application/pdf");
        //}



    }
}
