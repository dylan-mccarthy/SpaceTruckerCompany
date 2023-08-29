using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaceTruckerCompany.Web.Admin.Models
{
    public class TradeItemEntry : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public TradeItem Item { get; set; }
        public SpaceShipEntry Ship { get; set; }

        public int Quantity { get; set; }

        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }

        public string TradeType { get; set; }

        public TradeItemEntry()
        {
            Id = -1;
            Item = new TradeItem();
            Ship = new SpaceShipEntry();
            Quantity = 0;
            BuyPrice = 100;
            SellPrice = 100;
            TradeType = "Buy";
        }

    }
}
