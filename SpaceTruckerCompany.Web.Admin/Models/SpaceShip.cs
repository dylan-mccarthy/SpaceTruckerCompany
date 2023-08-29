using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaceTruckerCompany.Web.Admin.Models
{
    public class SpaceShip : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Fuel { get; set; }
        public int Cargo { get; set; }
        public List<TradeItemEntry> TradeItems { get; set; }
        public int MaxFuel { get; set; }
        public int MaxCargo { get; set; }
        public int Speed { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }

        public SpaceShip()
        {
            Id =-1;
            Fuel = 100;
            Cargo = 0;
            TradeItems = new List<TradeItemEntry>();
            MaxFuel = 100;
            MaxCargo = 100;
            Speed = 1;
            Price = 1000;
            Size = "Small";
        }
    }
}
