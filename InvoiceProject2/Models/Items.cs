using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace InvoiceProject2.Models
{
    public class Items
    {
        [Key]
        public int ItemCode { get; set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
    }
}