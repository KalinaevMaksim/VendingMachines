using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VendingMachineWebAPINet5.Models
{
    public partial class VendingMachine
    {
        public VendingMachine()
        {
            VendingMachineCoins = new HashSet<VendingMachineCoin>();
            VendingMachineDrinks = new HashSet<VendingMachineDrink>();
        }

        public int Id { get; set; }
        public byte[] SecretCode { get; set; }

        [JsonIgnore]
        public virtual ICollection<VendingMachineCoin> VendingMachineCoins { get; set; }
        [JsonIgnore]
        public virtual ICollection<VendingMachineDrink> VendingMachineDrinks { get; set; }
    }
}
