namespace SpaceTruckerCompany.API.Models
{
    public class SpaceStation : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<TradeItemEntry> TradeItems { get; set; }
        public List<SpaceShipEntry> Ships { get; set; }
        public double CoordinatesX { get; set; }
        public double CoordinatesY { get; set; }

        public SpaceStation()
        {
            Id = System.Guid.NewGuid().ToString();
            Name = "";
            TradeItems = new List<TradeItemEntry>();
            Ships = new List<SpaceShipEntry>();
            CoordinatesX = 0;
            CoordinatesY = 0;
        }
    }
}
