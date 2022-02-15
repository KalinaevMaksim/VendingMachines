using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VendingMachineWebAPINet5.Models
{
    public partial class VendingMachineCoin
    {
        public int Id { get; set; }
        public int? IdVendingMachine { get; set; }
        public int? IdCoin { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }

        public virtual Coin IdCoinNavigation { get; set; }
        [JsonIgnore]
        public virtual VendingMachine IdVendingMachineNavigation { get; set; }
    }
}
