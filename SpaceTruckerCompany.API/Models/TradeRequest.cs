namespace SpaceTruckerCompany.API.Models
{
    public class TradeRequest
    {
        public string Id { get; set; }
        public SpaceStation Station { get; set; }
        public TradeItemEntry Item { get; set; }
        public SpaceShipEntry Ship { get; set; }

        public TradeRequest()
        {
            Id = System.Guid.NewGuid().ToString();
            Station = new SpaceStation();
            Item = new TradeItemEntry();
            Ship = new SpaceShipEntry();
        }

    }
}
