using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Practice4.Models;
namespace Practice4.ViewModel
{
    public class PurchaseView
    {
        public IEnumerable<Purchase> Purchases { get; set; }
        public int PurchaseId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "supplier is required")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        public string Supplier { get; set; }

       
     
        [Required(ErrorMessage = "Batch number is required")]
        public int BatchNo { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [DataType(DataType.Currency)]
        public int Quantity { get; set; }

        [DisplayName("Supplier date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "date is required")]
        public System.DateTime Date { get; set; }

        public Nullable<int> Agency_Id { get; set; }

    }
}