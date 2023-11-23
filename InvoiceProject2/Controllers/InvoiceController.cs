using InvoiceProject2.Dtos;
using InvoiceProject2.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceProject2.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext _context;
        public InvoiceController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult NewInvoice()
        {
            return View();
        }
        public ActionResult DiplayInvoices()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            var Invoice = new InvoiceDetails
            {
                Invoice = _context.Invoice.SingleOrDefault(i => i.InvoiceId == id),
                Items = _context.Items.Where(i => i.InvoiceId == id).ToList()
            };
            return View(Invoice);
        }
        public ActionResult Reports(string ReprotType,int id)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/InvoiceReport.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "InvoiceDataSet";
            var clientIdParameter = new SqlParameter("@id", id);
            var customerInvoice= _context.Database.SqlQuery<CustomerInvoice>("GetCustomerInvoice @id", clientIdParameter).ToList();
            List<CustomerInvoice2> customerInvoice2=new List<CustomerInvoice2>();
            for (int i=0;i< customerInvoice.Count; i++)
            {
                CustomerInvoice2 x = new CustomerInvoice2();
                x.InvoiceId = customerInvoice[i].InvoiceId;
                x.Customer = customerInvoice[i].Customer;
                x.InvoiceDate = customerInvoice[i].InvoiceDate.ToString("MM/dd/yyyy");
                x.PaymentMethod = customerInvoice[i].PaymentMethod.ToString();
                if (x.PaymentMethod == "True")
                    x.PaymentMethod = "Credit";
                else
                    x.PaymentMethod = "Cash";
                x.Description = customerInvoice[i].Description;
                x.ItemCode = customerInvoice[i].ItemCode;
                x.ItemName = customerInvoice[i].ItemName;
                x.Qty = customerInvoice[i].Qty.ToString();
                x.Price = customerInvoice[i].Price.ToString();
                customerInvoice2.Add(x);
            }
            reportDataSource.Value = customerInvoice2;
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReprotType;
            string mimeType;
            string encoding;
            string fileNameExtension="pdf";
            if(reportType=="PDF")
            {
                fileNameExtension = "pdf";
            }
            else if(reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension
                , out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=Invoice_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }
        public ActionResult Reports2(string ReprotType, int id)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/InvoiceReport2.rdlc");
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "InvoiceDataSet";
            var clientIdParameter = new SqlParameter("@id", id);
            var customerInvoice = _context.Database.SqlQuery<CustomerInvoice>("GetInvoiceInformation").ToList();
            List<CustomerInvoice2> customerInvoice2 = new List<CustomerInvoice2>();
            for (int i = 0; i < customerInvoice.Count; i++)
            {
                CustomerInvoice2 x = new CustomerInvoice2();
                x.InvoiceId = customerInvoice[i].InvoiceId;
                x.Customer = customerInvoice[i].Customer;
                x.InvoiceDate = customerInvoice[i].InvoiceDate.ToString("MM/dd/yyyy");
                x.PaymentMethod = customerInvoice[i].PaymentMethod.ToString();
                if (x.PaymentMethod == "True")
                    x.PaymentMethod = "Credit";
                else
                    x.PaymentMethod = "Cash";
                x.Description = customerInvoice[i].Description;
                x.ItemCode = customerInvoice[i].ItemCode;
                x.ItemName = customerInvoice[i].ItemName;
                x.Qty = customerInvoice[i].Qty.ToString();
                x.Price = customerInvoice[i].Price.ToString();
                customerInvoice2.Add(x);
            }
            reportDataSource.Value = customerInvoice2;
            localReport.DataSources.Add(reportDataSource);
            string reportType = ReprotType;
            string mimeType;
            string encoding;
            string fileNameExtension = "pdf";
            if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            string[] streams;
            Warning[] warnings;
            byte[] renderedByte;
            renderedByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension
                , out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=Invoice_Report." + fileNameExtension);
            return File(renderedByte, fileNameExtension);
        }
    }
}