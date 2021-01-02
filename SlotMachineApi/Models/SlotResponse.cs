using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlotMachineApi.Models
{
    public class SlotResponse
    {
        public string UserId { get; set; }
        
        public decimal Balance { get; set; }

        public decimal WinAmount { get; set; }

        public int[] spinArray { get; set; }

    }
}
