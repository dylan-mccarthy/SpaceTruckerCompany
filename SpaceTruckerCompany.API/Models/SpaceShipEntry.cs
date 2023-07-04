namespace SpaceTruckerCompany.API.Models
{
    public class SpaceShipEntry : IEntity
    {
        public string Id { get; set; }
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
            Id = System.Guid.NewGuid().ToString();
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
