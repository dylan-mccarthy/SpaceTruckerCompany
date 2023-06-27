
namespace SpaceTruckerCompany.API.Models
{
    public class TradeItem : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int BasePrice { get; set; }
        public int Quantity { get; set; }
        public int SellPrice { get; set; }
        public int BuyPrice { get; set; }
        public int Volume { get; set; }

        public TradeItem()
        {
            Id = System.Guid.NewGuid().ToString();
            Name = "Item";
            Category = "General";
            BasePrice = 100;
            Quantity = 0;
            SellPrice = 100;
            BuyPrice = 100;
            Volume = 1;
        }
    }
}
