using AutoMapper;
using InvoiceProject2.App_Start;
using InvoiceProject2.Dtos;
using InvoiceProject2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoiceProject2.Controllers.Api
{
    public class InvoiceController : ApiController
    {
        private ApplicationDbContext _context;
        public InvoiceController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<InvoiceDto>GetInvoices()
        {
            IMapper _mapper = Configration();
            return _context.Invoice.ToList().Select(_mapper.Map<Invoice, InvoiceDto>);
        }
        [HttpPost]
        public IHttpActionResult AddInvoice(InvoiceDetails objInvoiceDto)
        {
            if (objInvoiceDto.Invoice == null)
                return BadRequest("it is null");
            _context.Invoice.Add(objInvoiceDto.Invoice);
            foreach(var item in objInvoiceDto.Items)
            {
                _context.Items.Add(item);
            }
            _context.SaveChanges();
            return Ok();
        }
        private IMapper Configration()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapProfile>();
            });
            return config.CreateMapper();
        }
    }
}
