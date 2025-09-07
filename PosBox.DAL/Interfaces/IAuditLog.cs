using PosBox.DAL.Entity_Framework.Table_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosBox.DAL.Interfaces
{
    public interface IAuditLog : IRepository<AuditLog>
    {
        bool RecordLog(int Id, string Action, string? Details);

        IEnumerable<AuditLog>? GetAuditById(int Id);
    }
}
