using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlotMachineApi.Models;
using MongoDB.Driver;

namespace SlotMachineApi.Services
{
    public class ConfigurationService
    {
        private readonly IMongoCollection<Configuration> _config;

        public ConfigurationService(ISlotMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _config = database.GetCollection<Configuration>(settings.ConfigurationCollectionName);
        }

        public List<Configuration> Get() =>
            _config.Find(config => true).ToList();

        public Configuration Get(string id) =>
            _config.Find<Configuration>(config => config.Id == id).FirstOrDefault();

        public void Update(string id, Configuration configIn) =>
            _config.ReplaceOne(config => config.Id == id, configIn);
    }
}
