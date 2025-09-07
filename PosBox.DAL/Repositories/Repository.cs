using Microsoft.EntityFrameworkCore;
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
    internal class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDBContext db;
        protected readonly DbSet<T> dbSet;
        public Repository(ApplicationDBContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }

        public bool Create(T entity)
        {
            dbSet.Add(entity);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int Id)
        {
            var exobj = Get(Id);
            if (exobj != null)
            {
                exobj.IsDeleted = true;
                return Update(exobj);
            }
            return false;
        }

        public T? Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public bool Update(T entity)
        {
            dbSet.Update(entity);
            return db.SaveChanges() > 0;
        }
    }
}
