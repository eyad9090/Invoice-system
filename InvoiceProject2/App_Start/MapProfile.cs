using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceProject2.Dtos;
using InvoiceProject2.Models;

namespace InvoiceProject2.App_Start
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>();
            //.ForMember(m => m.Id, opt => opt.Ignore())
        }
    }
}