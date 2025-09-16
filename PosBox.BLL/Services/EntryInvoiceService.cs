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
    public class EntryInvoiceService
    {
        private readonly DataAccess DA;

        public EntryInvoiceService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EntryInvoice, EntryInvoiceDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(EntryInvoiceDTO obj)
        {
            var record = GetMapper().Map<EntryInvoice>(obj);
            return DA.EntryInvoiceData().Create(record);
        }

        public IEnumerable<EntryInvoiceDTO> GetAll()
        {
            var data = DA.EntryInvoiceData().GetAll();
            return GetMapper().Map<List<EntryInvoiceDTO>>(data);

        }

        public EntryInvoiceDTO? GetById(int Id)
        {
            var data = DA.EntryInvoiceData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<EntryInvoiceDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.EntryInvoiceData().Get(Id);
            if (ext == null) return false;
            DA.EntryInvoiceData().Delete(ext.Id);
            return true;
        }

        public bool Update(EntryInvoiceDTO obj)
        {
            var data = GetMapper().Map<EntryInvoice>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.EntryInvoiceData().Update(data);
        }
    }
}
