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
    public class DailyReportService
    {
        private readonly DataAccess DA;

        public DailyReportService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DailyReport, DailyReportDTO>().ReverseMap();

            });
            return new Mapper(config);
        }

        public bool create(DailyReportDTO obj)
        {
            var record = GetMapper().Map<DailyReport>(obj);
            return DA.DailyReportData().Create(record);
        }

        public IEnumerable<DailyReportDTO> GetAll()
        {
            var data = DA.DailyReportData().GetAll();
            return GetMapper().Map<List<DailyReportDTO>>(data);

        }

        public DailyReportDTO? GetById(int Id)
        {
            var data = DA.DailyReportData().Get(Id);
            if (data == null) return null;
            return GetMapper().Map<DailyReportDTO>(data);
        }

        public bool Delete(int Id)
        {
            var ext = DA.DailyReportData().Get(Id);
            if (ext == null) return false;
            DA.DailyReportData().Delete(ext.Id);
            return true;
        }

        public bool Update(DailyReportDTO obj)
        {
            var data = GetMapper().Map<DailyReport>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.DailyReportData().Update(data);
        }
    }
}
