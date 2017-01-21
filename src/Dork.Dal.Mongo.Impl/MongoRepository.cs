using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dork.Core.Dal;
using Dork.Core.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dork.Dal.Mongo.Impl
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : IEntity<TKey>
    {

        protected readonly IMongoCollection<T> _collection;
        public string CollectionName => _collection.CollectionNamespace.CollectionName;

        #region Contructors

        public Repository(IConfiguration config) : this(MongoFactory<TKey>.GetDefaultConnectionString(config)) { }

        public Repository(string connectionString)
        {
            _collection = MongoFactory<TKey>.GetCollectionFromConnectionString<T>(connectionString);
        }
        public Repository(string connectionString, string collectionName)
        {
            _collection = MongoFactory<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }

        public Repository(MongoUrl url)
        {
            _collection = MongoFactory<TKey>.GetCollectionFromUrl<T>(url);
        }

        public Repository(MongoUrl url, string collectionName)
        {
            _collection = MongoFactory<TKey>.GetCollectionFromUrl<T>(url, collectionName);
        }

        #endregion



        public IEnumerator<T> GetEnumerator()
        {
            return _collection.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.AsQueryable().GetEnumerator();
        }

        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }
        public IMongoCollection<T> Collection { get; }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllAsync(_ => true);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> condition)
        {
            return await _collection.Find(condition).ToListAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _collection.Aggregate().Match(x => x.Id.Equals(BsonValue.Create(id))).FirstOrDefaultAsync();
        }

        public async Task<long> UpdateAsync(T entity)
        {

            var r = await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new UpdateOptions { IsUpsert = true });
            return r.IsAcknowledged ? r.ModifiedCount : 0;
        }

        public virtual async Task<long> UpdateAsync(IEnumerable<T> entities)
        {
            long results = 0;
            foreach (var entity in entities)
            {
                results += await UpdateAsync(entity);
            }
            return results;
        }

        public async Task<long> DeleteAsync(TKey id)
        {
            var r = await _collection.DeleteOneAsync(x => x.Id.Equals(new ObjectId(id as string)));
            return r.IsAcknowledged ? r.DeletedCount : 0;
        }

        public async Task<long> DeleteAsync(T entity)
        {
            return await DeleteAsync(entity.Id);
        }

        public async Task<long> DeleteAllAsync()
        {
            var r = await _collection.DeleteManyAsync(new BsonDocument());
            return r.IsAcknowledged ? r.DeletedCount : 0;
        }

        public async Task<long> CountAsync()
        {
            return await _collection.CountAsync(new BsonDocument());
        }


    }

    public class Repository<T> : Repository<T, string>, IRepository<T> where T : IEntity<string>
    {
        // public MongoRepository() : base() { }



        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// Uses the Default App/Web.Config connectionstrings to fetch the connectionString and Database name.
        /// </summary>
        /// <remarks>Default constructor defaults to "MongoServerSettings" key for connectionstring.</remarks>
        public Repository(IConfiguration config) : base(config) { }

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        public Repository(MongoUrl url) : base(url) { }

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        public Repository(MongoUrl url, string collectionName) : base(url, collectionName) { }

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="connectionString">Connectionstring to use for connecting to MongoDB.</param>
        public Repository(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="connectionString">Connectionstring to use for connecting to MongoDB.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        public Repository(string connectionString, string collectionName) : base(connectionString, collectionName) { }

        public new async Task<long> UpdateAsync(T entity)
        {
            if (entity.Id == null)
            {
                entity.Id = SetGeneratedId();
            }
            var r = await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new UpdateOptions { IsUpsert = true });
            return r.IsAcknowledged ? r.ModifiedCount : 0;
        }

        private string SetGeneratedId()
        {
            return ObjectId.GenerateNewId().ToString();
        }

    }
}
