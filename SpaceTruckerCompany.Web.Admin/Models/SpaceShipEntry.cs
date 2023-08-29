using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaceTruckerCompany.Web.Admin.Models
{
    public class SpaceShipEntry : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public SpaceShip Ship { get; set; }
        public Player Owner { get; set; }
        public int CargoSpace { get; set; }
        public int UsedCargoSpace { get; set; }
        public double CurrentFuel { get; set; }
        public double FuelUsageRate { get; set; }
        public List<TradeItemEntry> Cargo { get; set; }
        public string Location { get; set; }
        public SpaceShipEntry()
        {
            Ship = new SpaceShip();
            Owner = new Player("Player");
            CargoSpace = 100;
            UsedCargoSpace = 0;
            CurrentFuel = 100;
            Cargo = new List<TradeItemEntry>();
            Location = "Earth";
        }
    }
}
