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
    public class TransferInvoiceService
    {
        private readonly DataAccess DA;

        public TransferInvoiceService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferInvoice, TransferInvoiceDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(TransferInvoiceDTO obj)
        {
            var record = GetMapper().Map<TransferInvoice>(obj);
            return DA.TransferInvoiceData().Create(record);
        }

        public IEnumerable<TransferInvoiceDTO> GetAll()
        {
            var data = DA.TransferInvoiceData().GetAll();
            return GetMapper().Map<List<TransferInvoiceDTO>>(data);

        }

        public TransferInvoiceDTO? GetById(int Id)
        {
            var data = DA.TransferInvoiceData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<TransferInvoiceDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.TransferInvoiceData().Get(Id);
            if (ext == null) return false;
            DA.TransferInvoiceData().Delete(ext.Id);
            return true;
        }

        public bool Update(TransferInvoiceDTO obj)
        {
            var data = GetMapper().Map<TransferInvoice>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.TransferInvoiceData().Update(data);
        }
    }
}
