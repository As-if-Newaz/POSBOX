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
    public class BusinessService
    {
        private readonly DataAccess DA;

        public BusinessService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Business, BusinessDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(BusinessDTO obj)
        {
            var record = GetMapper().Map<Business>(obj);
            return DA.BusinessData().Create(record);
        }

        public IEnumerable<BusinessDTO> GetAll()
        {
            var data = DA.BusinessData().GetAll();
            return GetMapper().Map<List<BusinessDTO>>(data);

        }

        public BusinessDTO? GetById(int Id)
        {
            var data = DA.BusinessData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<BusinessDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.BusinessData().Get(Id);
            if (ext == null) return false;
            DA.BusinessData().Delete(ext.Id);
            return true;
        }

        public bool Update(BusinessDTO obj)
        {
            var data = GetMapper().Map<Business>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.BusinessData().Update(data);
        }
    }


}
