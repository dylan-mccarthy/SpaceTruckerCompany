namespace SpaceTruckerCompany.API.Models
{
    public class TradeRequest
    {
        public string Id { get; set; }
        public TradeItemEntry Item { get; set; }
        public SpaceShipEntry Ship { get; set; }

        public string TradeType { get; set; }

        public int Amount { get; set; }

        public TradeRequest()
        {
            Id = System.Guid.NewGuid().ToString();
            Item = new TradeItemEntry();
            Ship = new SpaceShipEntry();
            Amount = 0;
            TradeType = "Buy";
        }

    }
}
