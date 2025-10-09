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
    public class TransferService
    {
        private readonly DataAccess DA;

        public TransferService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transfer, TransferDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(TransferDTO obj)
        {
            var record = GetMapper().Map<Transfer>(obj);
            return DA.TransferData().Create(record);
        }

        public IEnumerable<TransferDTO> GetAll()
        {
            var data = DA.TransferData().GetAll();
            return GetMapper().Map<List<TransferDTO>>(data);

        }

        public TransferDTO? GetById(int Id)
        {
            var data = DA.TransferData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<TransferDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.TransferData().Get(Id);
            if (ext == null) return false;
            DA.TransferData().Delete(ext.Id);
            return true;
        }

        public bool Update(TransferDTO obj)
        {
            var data = GetMapper().Map<Transfer>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.TransferData().Update(data);
        }
    }
}
