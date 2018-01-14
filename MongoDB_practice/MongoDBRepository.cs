using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB_practice
{
  internal class MongoDBRepository<T> where T : Equipment
  {
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<T> collection;

    public MongoDBRepository(string connectionString)
    {
      client = new MongoClient(connectionString);
      database = client.GetDatabase("local");
      collection = database.GetCollection<T>(nameof(T));

    }
    public void Add(T item)
    {

      collection.InsertOne(item);
    }

    public void Update(T item)
    {
      var builder = Builders<T>.Filter;
      var filter = builder.Eq(eq => eq.id, item.id);
      var update = Builders<T>.Update.Set(s => s.name, item.name);

      //collection.UpdateOne(filter,update);
      collection.FindOneAndUpdate<T>(filter, update);

    }
  }

    internal class MongoDBRepository
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<Equipment> collection;

        public MongoDBRepository(string connectionString) {
            client = new MongoClient(connectionString);
            database = client.GetDatabase("local");
            collection = database.GetCollection<Equipment>("Equipment");

        }
        public void Add(Equipment item) {
            collection.InsertOne(item);
        }

        public void AddMany(List<Equipment> equipments) {
            collection.InsertMany(equipments);
        }

        public long CountDocuments() {
            return collection.Count<Equipment>(eq=>eq.id > 1);
        }

        public Equipment GetById(int id) {
            return collection.Find(eq => eq.id == id).FirstOrDefault();
        }

        public IEnumerable<Equipment> GetAll() {
            // give empty filter, then return all documents
            return collection.Find(Builders<Equipment>.Filter.Empty).ToEnumerable();
        }
  }
}
