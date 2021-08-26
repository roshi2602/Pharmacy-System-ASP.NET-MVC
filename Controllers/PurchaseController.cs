using System.IO;
using System.Linq;
using System.Web.Mvc;
using Practice4.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Practice4.ViewModel;
using System.Collections.Generic;

namespace Practice4.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        PracticeaspDBEntities1 db = new PracticeaspDBEntities1();

        //used View model
        //search functionality applied
        [Authorize(Roles = "Chemist,Pharmacist,Manager")]
        public ActionResult Home3(string search)
        {
          
             var vmodel = new List<PurchaseView>();
            vmodel = db.Purchases.Where(i=>i.Supplier.Contains(search)).ToList().Select(i => new PurchaseView
            {
                PurchaseId = i.PurchaseId,
                Supplier = i.Supplier,
                BatchNo = i.BatchNo,
                Quantity = i.Quantity,
                Date = i.Date,
                Agency_Id = i.Agency_Id
            }).ToList();
           return View(vmodel);

        }

        [HttpGet]
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Create()
        {
            Purchase x = new Purchase();
            return View(x);
        }



        [HttpPost]
        [Authorize(Roles = "Pharmacist")]
        public ActionResult Create(PurchaseView pp)
        {
            if (ModelState.IsValid)
            {

                Purchase ps = new Purchase
                {
                    Supplier = pp.Supplier,
                    BatchNo = pp.BatchNo,
                    Quantity = pp.Quantity,
                    Date = pp.Date
                };
                db.Purchases.Add(ps);
                db.SaveChanges();
                return RedirectToAction("Home3");
            }
            return View();

        }


       
        //entire code for PDF Download with Database data along with tables
        //install from nuget=itextsharp , itextsharp.xmlworker
        [HttpPost]
        [ValidateInput(false)]
        //validate input =because sometimes we want to send html values to controller
        //is used to enable or disable request validation to prevent csrf  //false=for disable request validation
        [Authorize(Roles = "Pharmacist")]
        public FileResult Export()
        {
            MemoryStream stream = new MemoryStream();
           
            //file name to be created   
            string strPDFFileName = string.Format("RoshiPdf"+"-"+"PurchaseList" + ".pdf");
            //creating document to add some info
            Document doc = new Document();
            doc.SetMargins(0,0,0,0);
            //Create PDF Table with 6 columns  
            PdfPTable tableLayout = new PdfPTable(6);
            //setting margins for left right top bottom
            doc.SetMargins(0 ,0,0,0);

            //Create PDF Table 
            PdfWriter.GetInstance(doc, stream).CloseStream = false;
            //opening pdf
            doc.Open();

            //Add Content to PDF   
            doc.Add(pdf(tableLayout));  //private method made below

            // Closing the document  
            doc.Close();

            byte[] byteInfo = stream.ToArray(); //byte is 8 bit unsigned integer with range 0 to 255 and helps in performing operations
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;  //setting the current position within the stream


            return File(stream, "application/pdf", strPDFFileName);

        }
        public PdfPTable pdf(PdfPTable tableLayout)  //passing parameter
        {
            var x = db.Purchases.ToList();
            //designing it
            tableLayout.AddCell(new PdfPCell(new Phrase("Creating Pdf using ItextSharp", new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            //Add header  
            //using private methods made below
            AddCellToHeader(tableLayout, "PurchaseId");
            AddCellToHeader(tableLayout, "Supplier");
            AddCellToHeader(tableLayout, "BatchNo");
            AddCellToHeader(tableLayout, "Quantity");
            AddCellToHeader(tableLayout, "Date");
            AddCellToHeader(tableLayout, "Agency_Id");
            foreach (var i in x)
            {
                //using private methods made below
                AddCellToBody(tableLayout, i.PurchaseId.ToString());
                AddCellToBody(tableLayout, i.Supplier);
                AddCellToBody(tableLayout, i.BatchNo.ToString());
                AddCellToBody(tableLayout, i.Quantity.ToString());
                AddCellToBody(tableLayout, i.Date.ToString());
                AddCellToBody(tableLayout, i.Agency_Id.ToString());


            }

            return tableLayout;

        }
              

            // Method to add single cell to the Header  
            private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
            {
            //designing it
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 12, 1, iTextSharp.text.BaseColor.PINK)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(0, 128, 0)
                });
            }

            // Method to add single cell to the body  
            private static void AddCellToBody(PdfPTable tableLayout, string cellText)
            {
            //designing it
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.TIMES_ROMAN, 12, 1, iTextSharp.text.BaseColor.BLACK)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT, Padding = 5, BackgroundColor = new iTextSharp.text.BaseColor(240, 128, 128)
                });
            }

        

    }


}

