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
    public class StockService
    {
        private readonly DataAccess DA;

        public StockService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Stock, StockDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(StockDTO obj)
        {
            var record = GetMapper().Map<Stock>(obj);
            return DA.StockData().Create(record);
        }

        public IEnumerable<StockDTO> GetAll()
        {
            var data = DA.StockData().GetAll();
            return GetMapper().Map<List<StockDTO>>(data);

        }

        public StockDTO? GetById(int Id)
        {
            var data = DA.StockData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<StockDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.StockData().Get(Id);
            if (ext == null) return false;
            DA.StockData().Delete(ext.Id);
            return true;
        }

        public bool Update(StockDTO obj)
        {
            var data = GetMapper().Map<Stock>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.StockData().Update(data);
        }
    }
}
