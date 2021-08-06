using ITCompany.Services.Helpers;
using ITCompany.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ITCompany.Services.Implementation
{
    public class MongoService<TEntity> : IMongoService<TEntity>
        where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoService(IConfiguration configuration)
        {
            var mongoUrl = new MongoUrl(configuration.GetConnectionString("MongoDb"));
            var database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);

            _collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
        }

        private protected string GetCollectionName(Type entityType)
        {
            return ((BsonCollectionAttribute)entityType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault())?.CollectionName;
        }

        //Index
        public async Task CreateIndex(string field)
        {
            var indexKeysDefinition = Builders<TEntity>.IndexKeys.Ascending(field);
            await _collection.Indexes.CreateOneAsync(new CreateIndexModel<TEntity>(indexKeysDefinition));
        }

        //Aggregation
        public IQueryable<TEntity> EntitiesAggregate(string field, string fieldValue)
        {
            var entities = _collection.Aggregate().Match(new BsonDocument { { field, fieldValue } }).ToList().AsQueryable();
            return entities;
        }

        //CRUD operations
        public virtual IQueryable<TEntity> AsQueryable()
        {
            return _collection.AsQueryable();
        }

        public void DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TEntity>.Filter.Eq(ent => ent.Id, objectId);
            _collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TEntity>.Filter.Eq(ent => ent.Id, objectId);
                _collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }

        public void DeleteOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.DeleteOne(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public virtual IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TEntity FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TEntity>.Filter.Eq(ent => ent.Id, objectId);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TEntity> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TEntity>.Filter.Eq(ent => ent.Id, objectId);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public void InsertMany(ICollection<TEntity> entities)
        {
            _collection.InsertMany(entities);
        }

        public virtual async Task InsertManyAsync(ICollection<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public virtual void InsertOne(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public virtual Task InsertOneAsync(TEntity entity)
        {
            return Task.Run(() => _collection.InsertOneAsync(entity));
        }

        public void ReplaceOne(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(ent => ent.Id, entity.Id);
            _collection.FindOneAndReplace(filter, entity);
        }

        public virtual async Task ReplaceOneAsync(TEntity entity)
        {

            var filter = Builders<TEntity>.Filter.Eq(ent => ent.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity);
        }
    }
}
