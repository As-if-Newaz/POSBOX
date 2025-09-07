using PosBox.DAL.Entity_Framework;
using PosBox.DAL.Entity_Framework.Table_Models;
using PosBox.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Repositories
{
    internal class AuditLogRepository : Repository<AuditLog>, IAuditLog
    {
        public AuditLogRepository(ApplicationDBContext db) : base(db)
        {
        }
        public IEnumerable<AuditLog>? GetAuditById(int Id)
        {
            return db.AuditLogs.Where(u => u.PerformedById == Id);
        }

        public bool RecordLog(int Id, string Action, string? Details)
        {
            var auditLog = new AuditLog
            {
                Action = Action,
                Details = Details,
                PerformedById = Id,
                PerformedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
            };
            db.AuditLogs.Add(auditLog);
            return db.SaveChanges() > 0;
        }
    }
}
