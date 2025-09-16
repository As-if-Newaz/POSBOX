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
    public class QuickSellService
    {
        private readonly DataAccess DA;

        public QuickSellService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuickSell, QuickSellDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(QuickSellDTO obj)
        {
            var record = GetMapper().Map<QuickSell>(obj);
            return DA.QuickSellData().Create(record);
        }

        public IEnumerable<QuickSellDTO> GetAll()
        {
            var data = DA.QuickSellData().GetAll();
            return GetMapper().Map<List<QuickSellDTO>>(data);

        }

        public QuickSellDTO? GetById(int Id)
        {
            var data = DA.QuickSellData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<QuickSellDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.QuickSellData().Get(Id);
            if (ext == null) return false;
            DA.QuickSellData().Delete(ext.Id);
            return true;
        }

        public bool Update(QuickSellDTO obj)
        {
            var data = GetMapper().Map<QuickSell>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.QuickSellData().Update(data);
        }
    }
}
