using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlotMachineApi.Models
{
    public class SlotMachineDatabaseSettings : ISlotMachineDatabaseSettings
    {
        public string SlotMachineCollectionName { get; set; }
        public string UserCollectionName { get; set; }
        public string ConfigurationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISlotMachineDatabaseSettings
    {
        string SlotMachineCollectionName { get; set; }
        string UserCollectionName { get; set; }
        string ConfigurationCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
