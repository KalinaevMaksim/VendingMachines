using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VendingMachineWebAPINet5.Models
{
    public partial class Coin
    {
        public Coin()
        {
            VendingMachineCoins = new HashSet<VendingMachineCoin>();
        }

        public int Id { get; set; }
        public int Nominal { get; set; }

        [JsonIgnore]
        public virtual ICollection<VendingMachineCoin> VendingMachineCoins { get; set; }
    }
}
