using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace VendingMachineWebAPINet5.Models
{
    public partial class Drink
    {
        public Drink()
        {
            VendingMachineDrinks = new HashSet<VendingMachineDrink>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Cost { get; set; }

        [JsonIgnore]
        public virtual ICollection<VendingMachineDrink> VendingMachineDrinks { get; set; }
    }
}
