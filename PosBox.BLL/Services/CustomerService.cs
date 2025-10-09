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
    public class CustomerService
    {
        private readonly DataAccess DA;

        public CustomerService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(CustomerDTO obj)
        {
            var record = GetMapper().Map<Customer>(obj);
            return DA.CustomerData().Create(record);
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            var data = DA.CustomerData().GetAll();
            return GetMapper().Map<List<CustomerDTO>>(data);

        }

        public CustomerDTO? GetById(int Id)
        {
            var data = DA.CustomerData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<CustomerDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.CustomerData().Get(Id);
            if (ext == null) return false;
            DA.CustomerData().Delete(ext.Id);
            return true;
        }

        public bool Update(CustomerDTO obj)
        {
            var data = GetMapper().Map<Customer>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.CustomerData().Update(data);
        }
    }
}
