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
    public class SellService
    {
        private readonly DataAccess DA;

        public SellService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sell, SellDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(SellDTO obj)
        {
            var record = GetMapper().Map<Sell>(obj);
            return DA.SellData().Create(record);
        }

        public IEnumerable<SellDTO> GetAll()
        {
            var data = DA.SellData().GetAll();
            return GetMapper().Map<List<SellDTO>>(data);

        }

        public SellDTO? GetById(int Id)
        {
            var data = DA.SellData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<SellDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.SellData().Get(Id);
            if (ext == null) return false;
            DA.SellData().Delete(ext.Id);
            return true;
        }

        public bool Update(SellDTO obj)
        {
            var data = GetMapper().Map<Sell>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.SellData().Update(data);
        }
    }
}
