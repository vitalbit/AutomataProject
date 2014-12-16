﻿using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IDalEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> f);
        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity e);
    }
}
