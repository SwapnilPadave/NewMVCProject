using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFDBFirstExample.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public System.DateTime DateOfPurchase { get; set; }
        public string AvailabilityStatus { get; set; }
        public virtual long CategoryID { get; set; }
        public virtual long BrandID { get; set; }
        public bool Active { get; set; }
        public string Photo { get; set; }

        public decimal Quantity { get; set; }
        [ForeignKey("BrandID")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}