using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InvoiceProject2.Dtos
{
    public class InvoiceDto
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public bool PaymentMethod { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Customer Name")]
        public string Customer { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}