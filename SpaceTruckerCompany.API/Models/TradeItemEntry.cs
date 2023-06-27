namespace SpaceTruckerCompany.API.Models
{
    public class TradeItemEntry : IEntity
    {
        public string Id { get; set; }
        public TradeItem Item { get; set; }
        public SpaceShip Ship { get; set; }

        public int Quantity { get; set; }

        public TradeItemEntry()
        {
            Id = System.Guid.NewGuid().ToString();
            Item = new TradeItem();
            Ship = new SpaceShip();
            Quantity = 0;
        }

    }
}
