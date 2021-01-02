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
            _slotUsers.Find(slotMachine => true).ToList();

        public SlotMachine Get(string UserId) =>
            _slotUsers.Find<SlotMachine>(slotMachine => slotMachine.UserId == UserId).FirstOrDefault();

        public SlotMachine Create(SlotMachine slotUser)
        {
            _slotUsers.InsertOne(slotUser);
            return slotUser;
        }

        public void Update(string UserId, SlotMachine slotUserIn) =>
            _slotUsers.ReplaceOne(slotMachine => slotMachine.UserId == UserId, slotUserIn);

    }
}
