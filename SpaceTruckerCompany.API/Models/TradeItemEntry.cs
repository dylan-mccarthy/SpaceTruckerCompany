namespace SpaceTruckerCompany.API.Models
{
    public class TradeItemEntry : IEntity
    {
        public string Id { get; set; }
        public TradeItem Item { get; set; }
        public SpaceShipEntry Ship { get; set; }

        public int Quantity { get; set; }

        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }

        public string TradeType { get; set; }

        public TradeItemEntry()
        {
            Id = System.Guid.NewGuid().ToString();
            Item = new TradeItem();
            Ship = new SpaceShipEntry();
            Quantity = 0;
            BuyPrice = 100;
            SellPrice = 100;
            TradeType = "Buy";
        }

    }
}
