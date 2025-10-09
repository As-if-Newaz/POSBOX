using AutoMapper;
using PosBox.BLL.DTOs;
using PosBox.DAL;
using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PosBox.DAL.Entity_Framework.Table_Models.Enums;

namespace PosBox.BLL.Services
{
    public class AuditLogService
    {
        private readonly DataAccess DA;

        public AuditLogService(DataAccess dataAccess)
        {
            DA = dataAccess;
        }

        static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuditLog, AuditLogDTO>().ReverseMap();
                
            });
            return new Mapper(config);
        }
        public bool RecordLog(int userId, UserRole role, AuditActions action, string description)
        {
            return DA.AuditData().RecordLog(userId,role, action, description);
        }

        public bool Delete(int logId)
        {
            var log = DA.AuditData().Get(logId);
            if (log == null) return false;
            DA.AuditData().Delete(log.Id);
            return true;
        }

        public IEnumerable<AuditLogDTO> GetAll()
        {
            var data = DA.UserData().GetAll();
            return GetMapper().Map<List<AuditLogDTO>>(data);

        }

        public AuditLogDTO? GetById(int logId)
        {
            var data = DA.AuditData().Get(logId);
            if (data == null) return null;
            return GetMapper().Map<AuditLogDTO>(data);
        }

        public bool Update(AuditLogDTO obj)
        {
            var data = GetMapper().Map<AuditLog>(obj);
            if (data == null)
            {
                return false;
            }
            return DA.AuditData().Update(data);
        }
    }
}
