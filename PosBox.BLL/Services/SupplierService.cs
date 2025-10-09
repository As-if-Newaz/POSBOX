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
    public class SupplierService
    {
        private readonly DataAccess DA;

        public SupplierService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Supplier, SupplierDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(SupplierDTO obj)
        {
            var record = GetMapper().Map<Supplier>(obj);
            return DA.SupplierData().Create(record);
        }

        public IEnumerable<SupplierDTO> GetAll()
        {
            var data = DA.SupplierData().GetAll();
            return GetMapper().Map<List<SupplierDTO>>(data);

        }

        public SupplierDTO? GetById(int Id)
        {
            var data = DA.SupplierData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<SupplierDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.SupplierData().Get(Id);
            if (ext == null) return false;
            DA.SupplierData().Delete(ext.Id);
            return true;
        }

        public bool Update(SupplierDTO obj)
        {
            var data = GetMapper().Map<Supplier>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.SupplierData().Update(data);
        }
    }
}
