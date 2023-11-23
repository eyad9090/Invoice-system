using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceProject2.Models
{
    public class CustomerInvoice2
    {
        public int InvoiceId { get; set; }

        public string InvoiceDate { get; set; }

        public string PaymentMethod { get; set; }

        public string Customer { get; set; }

        public string Description { get; set; }

        public int ItemCode { get; set; }

        public Invoice Invoice { get; set; }

        public string ItemName { get; set; }

        public string Qty { get; set; }

        public string Price { get; set; }
    }
}