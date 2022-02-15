using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VendingMachineWebAPINet5.Models
{
    public partial class VendingMachineDrink
    {
        public int Id { get; set; }
        public int? IdVendingMachine { get; set; }
        public int? IdDrink { get; set; }
        public int Count { get; set; }

        public virtual Drink IdDrinkNavigation { get; set; }
        [JsonIgnore]
        public virtual VendingMachine IdVendingMachineNavigation { get; set; }
    }
}
