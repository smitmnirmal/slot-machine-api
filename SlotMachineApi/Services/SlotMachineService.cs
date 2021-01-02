using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlotMachineApi.Models;
using MongoDB.Driver;

namespace SlotMachineApi.Services
{
    public class SlotMachineService
    {
        private readonly IMongoCollection<SlotMachine> _slotUsers;

        public SlotMachineService(ISlotMachineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _slotUsers = database.GetCollection<SlotMachine>(settings.SlotMachineCollectionName);
        }

        public List<SlotMachine> Get() =>
            _slotUsers.Find(book => true).ToList();

        public SlotMachine Get(string UserName) =>
            _slotUsers.Find<SlotMachine>(user => user.UserName == UserName).FirstOrDefault();

        public SlotMachine Create(SlotMachine slotUser)
        {
            _slotUsers.InsertOne(slotUser);
            return slotUser;
        }

        public void Update(string UserName, SlotMachine slotUserIn) =>
            _slotUsers.ReplaceOne(user => user.UserName == UserName, slotUserIn);

/*        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);*/
    }
}
