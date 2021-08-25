using Practice4.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace Practice4.ViewModel
{
    public class StoreView
    {
        public List<Store> Stores { get; set; }

        [HiddenInput(DisplayValue = false)]  //id should be hidden and not displayed on web page
        public int Id { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "medicine name is required")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        public string MedicineName { get; set; }

        [DisplayName("medicine expiry date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "date is required")]
        public DateTime ExpiryData { get; set; }


        [DisplayName("medicine manufacturing date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "date is required")]

        public DateTime ManufacturedDate { get; set; }


        [DisplayName("medicine price")]
        [Required(ErrorMessage = "price is required")]
        [DataType(DataType.Currency)]
        [ScaffoldColumn(true)]              //it hides template such as EditorFor,DisplayFor //true for displaying on UI //false for not displaying on web
        public double price { get; set; }


        [DisplayName("medicine picture upload")]
        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        public string MedicinePhtot { get; set; }


        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Email appears to be invalid.")]
        [UIHint("_Email")]
        [DisplayName("pharmacy email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "email is required")]
        public string PharmEmail { get; set; }


        [DataType(DataType.PhoneNumber)]
        [DisplayName("pharmacy number")]
        [Required(ErrorMessage = "phone number is required")]
        public string PharmNumber { get; set; }


        [DisplayName("pharmacy website")]
        [DataType(DataType.Url)]
        [UIHint("open in new window")]
        [Required(ErrorMessage = "pharmacy website is required")]
        public string PharmWebsite { get; set; }



    }
}