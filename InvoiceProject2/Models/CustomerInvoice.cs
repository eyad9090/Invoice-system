using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceProject2.Models
{
    public class CustomerInvoice
    {
        public int InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public bool PaymentMethod { get; set; }

        public string Customer { get; set; }

        public string Description { get; set; }
        public int ItemCode { get; set; }

        public Invoice Invoice { get; set; }

        public string ItemName { get; set; }

        public int Qty { get; set; }

        public double Price { get; set; }
    }
}