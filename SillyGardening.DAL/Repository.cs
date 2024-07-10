using Microsoft.EntityFrameworkCore;
using SillyGardening.Core.Models;
using SillyGardening.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SillyGardening.DAL
{
    public class Repository : IRepository
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            TEntity result = _context.Set<TEntity>().Add(entity).Entity;
            _context.SaveChanges();
            return result;
        }
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public TEntity? GetById<TEntity>(Guid id) where TEntity : class
        {
            return _context.Set<TEntity>().Find(id);
        }


        public TEntity Remove<TEntity>(TEntity entity) where TEntity : class
        {
            TEntity result = _context.Set<TEntity>().Remove(entity).Entity;
            _context.SaveChanges();
            return result;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            if(typeof(TEntity).Name != typeof(User).Name)
            {
                TEntity result = _context.Set<TEntity>().Update(entity).Entity;
                _context.SaveChanges();
                return result;
            }
            var oldUser = entity as User;
            var user = _context.Users.FirstOrDefault(u => u.UserName == oldUser.UserName);
            if(user != null)
            {
                user.SaveData = oldUser.SaveData;
                _context.SaveChanges();
                return entity;
            }
            return null;
        }
    }
}
