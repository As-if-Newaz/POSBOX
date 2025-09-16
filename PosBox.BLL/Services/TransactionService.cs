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
    public class TransactionService
    {
        private readonly DataAccess DA;

        public TransactionService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, TransactionDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(TransactionDTO obj)
        {
            var record = GetMapper().Map<Transaction>(obj);
            return DA.TransactionData().Create(record);
        }

        public IEnumerable<TransactionDTO> GetAll()
        {
            var data = DA.TransactionData().GetAll();
            return GetMapper().Map<List<TransactionDTO>>(data);

        }

        public TransactionDTO? GetById(int Id)
        {
            var data = DA.TransactionData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<TransactionDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.TransactionData().Get(Id);
            if (ext == null) return false;
            DA.TransactionData().Delete(ext.Id);
            return true;
        }

        public bool Update(TransactionDTO obj)
        {
            var data = GetMapper().Map<Transaction>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.TransactionData().Update(data);
        }
    }
}
