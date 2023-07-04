namespace SpaceTruckerCompany.API.Models
{
    public class SpaceStation : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<TradeItemEntry> TradeItems { get; set; }
        public List<SpaceShipEntry> Ships { get; set; }
        public int[] Coordinates { get; set; }

        public SpaceStation()
        {
            Id = System.Guid.NewGuid().ToString();
            Name = "";
            TradeItems = new List<TradeItemEntry>();
            Ships = new List<SpaceShipEntry>();
            Coordinates = new int[2] { 0, 0 };
        }
    }
}
