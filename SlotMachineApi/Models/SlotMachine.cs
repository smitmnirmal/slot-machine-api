using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SlotMachineApi.Models
{
    public class SlotMachine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /*[BsonElement("Name")]*/
        public string UserName { get; set; }

        public decimal Balance { get; set; }
    }
}
