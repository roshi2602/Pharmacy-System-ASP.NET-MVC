
using System.Linq;
using System.Web.Mvc;
using Practice4.Models;
using System.Drawing;
using System.IO;
using System.Web.UI.DataVisualization.Charting;

namespace Practice4.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        PracticeaspDBEntities1 db = new PracticeaspDBEntities1();
        public ActionResult MyChart()
        {
            
            var x = db.SalesReports.ToList();
            //instantiating chart
            var charts = new Chart();
            //initalizing chartarea
            var ca = new ChartArea();
            //add chartarea in chart
            //ChartArea is used to store Chart
            charts.ChartAreas.Add(ca);
            //instantiate Series
            var ser = new Series();
            //access it
            foreach(var i in x)
            {
                ser.Points.AddXY( i.SalesYear,i.Store.price);
            }
            //adding label as percentage    
            ser.Label = "#PERCENT{P0}";
            ser.Font = new Font("Times New Roman", 8f); 
            //chart type is column
            ser.ChartType = SeriesChartType.Column;

           
            //add var ser to Series
            charts.Series.Add(ser);
            //  The name of the System.Web.UI.DataVisualization.Charting.Series to be added
            //create var for MemoryStream()
            var stream = new MemoryStream();
            //memory stream deals with data coming from file
            //it is used as backing source of data u want to keep in memory
            charts.ImageType = ChartImageType.Png;
            //ChartImageType.=specifies image type for chart
            //imagetype =is for types of image into which chart is rendered

            //setting charts height and width
            charts.Height = 500;
            charts.Width = 1000;

            charts.BackColor = Color.White;
         
            //save it in file
            charts.SaveImage(stream);
            //setting position of stream
            stream.Position = 0;
            //instantiating FileStreamResult and returning it in view because initializing its var gives error

         return new FileStreamResult(stream,"image/png");
           
            //FileContentResult - use it when you have a byte array you would like to return as a file
         //   FileStreamResult - you have a stream open, you want to return it's content as a file
            



            
        }
    }
}