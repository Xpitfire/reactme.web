using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dork.Core.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Dork.Dal.Mongo.Impl
{
    public static class MongoFactory<TKey>
    {
        /// <summary>
        ///     Retrieves the default connectionstring from the configuration.json file.
        /// </summary>
        /// <returns>Returns the default connectionstring from the configuration.json file.</returns>
        public static string GetDefaultConnectionString(IConfiguration config)
        {
            return config["MongoConfiguration:Server"] + "/" + config["MongoConfiguration:Database"];
        }

        /// <summary>
        ///     Creates and returns a MongoDatabase from the specified url.
        /// </summary>
        /// <param name="url">The url to use to get the database from.</param>
        /// <returns>Returns a MongoDatabase from the specified url.</returns>
        private static IMongoDatabase GetDatabaseFromUrl(MongoUrl url)
        {
            var client = new MongoClient(url);
            // return client.GetDatabase("mesdb");
            return client.GetDatabase(url.DatabaseName);
        }

        /// <summary>
        ///     Creates and returns a MongoCollection from the specified type and connectionstring.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="connectionString">The connectionstring to use to get the collection from.</param>
        /// <returns>Returns a MongoCollection from the specified type and connectionstring.</returns>
        public static IMongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString) where T : IEntity<TKey>
        {
            return GetCollectionFromConnectionString<T>(connectionString, GetCollectionName<T>());
        }

        /// <summary>
        ///     Creates and returns a MongoCollection from the specified type and connectionstring.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="connectionString">The connectionstring to use to get the collection from.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        /// <returns>Returns a MongoCollection from the specified type and connectionstring.</returns>
        public static IMongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString, string collectionName) where T : IEntity<TKey>
        {
            return GetDatabaseFromUrl(new MongoUrl(connectionString)).GetCollection<T>(collectionName);
        }

        /// <summary>
        ///     Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        public static IMongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url) where T : IEntity<TKey>
        {
            return GetCollectionFromUrl<T>(url, GetCollectionName<T>());
        }

        /// <summary>
        ///     Creates and returns a MongoCollection from the specified type and url.
        /// </summary>
        /// <typeparam name="T">The type to get the collection of.</typeparam>
        /// <param name="url">The url to use to get the collection from.</param>
        /// <param name="collectionName">The name of the collection to use.</param>
        /// <returns>Returns a MongoCollection from the specified type and url.</returns>
        public static IMongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url, string collectionName) where T : IEntity<TKey>
        {
            return GetDatabaseFromUrl(url).GetCollection<T>(collectionName);
        }

        /// <summary>
        ///     Determines the collectionname for T and assures it is not empty
        /// </summary>
        /// <typeparam name="T">The type to determine the collectionname for.</typeparam>
        /// <returns>Returns the collectionname for T.</returns>
        private static string GetCollectionName<T>() where T : IEntity<TKey>
        {
            var collectionName = GetCollectionNameFromType(typeof(T));

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }


        /// <summary>
        ///     Determines the collectionname from the specified type.
        /// </summary>
        /// <param name="entityType">The type of the entity to get the collectionname from.</param>
        /// <returns>Returns the collectionname from the specified type.</returns>
        private static string GetCollectionNameFromType(Type entityType)
        {
            string collectionName = entityType.Name;

            return collectionName;
        }
    }
}
