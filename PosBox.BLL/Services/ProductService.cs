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
    public class ProductService
    {
        private readonly DataAccess DA;

        public ProductService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(ProductDTO obj)
        {
            var record = GetMapper().Map<Product>(obj);
            return DA.ProductData().Create(record);
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var data = DA.ProductData().GetAll();
            return GetMapper().Map<List<ProductDTO>>(data);

        }

        public ProductDTO? GetById(int Id)
        {
            var data = DA.ProductData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<ProductDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.ProductData().Get(Id);
            if (ext == null) return false;
            DA.ProductData().Delete(ext.Id);
            return true;
        }

        public bool Update(ProductDTO obj)
        {
            var data = GetMapper().Map<Product>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.ProductData().Update(data);
        }
    }
}
