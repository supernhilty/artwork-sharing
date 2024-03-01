﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ArtworkSharing.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Entities { get; }
        DbContext DbContext { get; }
        T Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
      
        Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        
        /// <summary>
        /// Fin one item of an entity synchronous method
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T Find(params object[] keyValues);
        /// <summary>
        /// Find one item of an entity by asynchronous method 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Task<T> FindAsync(params object[] keyValues);
       
        /// <summary>
        /// Remove one item from an entity by asynchronous method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task DeleteAsync(int id, bool saveChanges = true);
        /// <summary>
        /// Remove one item from an entity by asynchronous method
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity, bool saveChanges = true);
        /// <summary>
        /// Remove multiple items from an entity by asynchronous method
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
    }
}

