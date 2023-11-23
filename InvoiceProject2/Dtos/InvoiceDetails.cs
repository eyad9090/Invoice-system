using InvoiceProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceProject2.Dtos
{
    public class InvoiceDetails
    {
        public Invoice Invoice { get; set; }
        public List<Items> Items { get; set; }
    }
}