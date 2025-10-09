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
    public class StockDiscardApplicationService
    {
        private readonly DataAccess DA;

        public StockDiscardApplicationService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StockDiscardApplication, StockDiscardApplicationDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(StockDiscardApplicationDTO obj)
        {
            var record = GetMapper().Map<StockDiscardApplication>(obj);
            return DA.StockDiscardApplicationData().Create(record);
        }

        public IEnumerable<StockDiscardApplicationDTO> GetAll()
        {
            var data = DA.StockDiscardApplicationData().GetAll();
            return GetMapper().Map<List<StockDiscardApplicationDTO>>(data);

        }

        public StockDiscardApplicationDTO? GetById(int Id)
        {
            var data = DA.StockDiscardApplicationData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<StockDiscardApplicationDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.StockDiscardApplicationData().Get(Id);
            if (ext == null) return false;
            DA.StockDiscardApplicationData().Delete(ext.Id);
            return true;
        }

        public bool Update(StockDiscardApplicationDTO obj)
        {
            var data = GetMapper().Map<StockDiscardApplication>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.StockDiscardApplicationData().Update(data);
        }
    }
}
