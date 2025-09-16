using AutoMapper;
using PosBox.BLL.DTOs;
using PosBox.DAL;
using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.BLL.Services
{
    public class SellInvoiceService
    {
        private readonly DataAccess DA;

        public SellInvoiceService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SellInvoice, SellInvoiceDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(SellInvoiceDTO obj)
        {
            var record = GetMapper().Map<SellInvoice>(obj);
            return DA.SellInvoiceData().Create(record);
        }

        public IEnumerable<SellInvoiceDTO> GetAll()
        {
            var data = DA.SellInvoiceData().GetAll();
            return GetMapper().Map<List<SellInvoiceDTO>>(data);

        }

        public SellInvoiceDTO? GetById(int Id)
        {
            var data = DA.SellInvoiceData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<SellInvoiceDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.SellInvoiceData().Get(Id);
            if (ext == null) return false;
            DA.SellInvoiceData().Delete(ext.Id);
            return true;
        }

        public bool Update(SellInvoiceDTO obj)
        {
            var data = GetMapper().Map<SellInvoice>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.SellInvoiceData().Update(data);
        }
    }
}
