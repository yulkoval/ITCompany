using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITCompany.Services.Interfaces
{
    public interface IMongoService<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> AsQueryable();

        IEnumerable<TEntity> FilterBy(
        Expression<Func<TEntity, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TEntity, bool>> filterExpression,
            Expression<Func<TEntity, TProjected>> projectionExpression);

        TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression);

        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        TEntity FindById(string id);

        Task<TEntity> FindByIdAsync(string id);

        void InsertOne(TEntity entity);

        Task InsertOneAsync(TEntity entity);

        void InsertMany(ICollection<TEntity> entity);

        Task InsertManyAsync(ICollection<TEntity> entity);

        void ReplaceOne(TEntity entity);

        Task ReplaceOneAsync(TEntity entity);

        void DeleteOne(Expression<Func<TEntity, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);

        void DeleteMany(Expression<Func<TEntity, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);

        Task CreateIndex(string field);

        IQueryable<TEntity> EntitiesAggregate(string field, string fieldValue);
    }
}
