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
    public class CategoryService
    {
        private readonly DataAccess DA;

        public CategoryService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(CategoryDTO obj)
        {
            var record = GetMapper().Map<Category>(obj);
            return DA.CategoryData().Create(record);
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            var data = DA.CategoryData().GetAll();
            return GetMapper().Map<List<CategoryDTO>>(data);

        }

        public CategoryDTO? GetById(int Id)
        {
            var data = DA.CategoryData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<CategoryDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.CategoryData().Get(Id);
            if (ext == null) return false;
            DA.CategoryData().Delete(ext.Id);
            return true;
        }

        public bool Update(CategoryDTO obj)
        {
            var data = GetMapper().Map<Category>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.CategoryData().Update(data);
        }
    }
}
