using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace WebGame.Domain
{
    public static class UnitDb
    {
        public static MongoClient Client { get; set; }
        public static MongoDatabase Database { get; set; }
        public static MongoServer Server { get; set; }
        public static MongoCollection<Unit> Collection { get; set; }

        static UnitDb()
        {
            Client = new MongoClient(ConfigurationManager.AppSettings["mongoDbConnectionString"]);
            Server = Client.GetServer();
            Database = Server.GetDatabase("servergamedatabase");
            Collection = Database.GetCollection<Unit>("Units");
        }

        public static Unit GetUnit(string name)
        {
            return Collection.FindOneById(name);
        }

        public static void AddUnit(Unit unit)
        {
            Collection.Insert(unit);
        }

        public static List<Unit> GetAllUnit()
        {
            return Collection.FindAll().ToList();
        }

        public static IQueryable<Unit> QueryableForLinqCommand()
        {
            return Collection.AsQueryable();
        }
    }
}
