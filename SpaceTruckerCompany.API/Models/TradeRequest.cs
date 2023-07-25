using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaceTruckerCompany.API.Models
{
    public class TradeRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public SpaceStation Station { get; set; }
        public TradeItemEntry Item { get; set; }
        public SpaceShipEntry Ship { get; set; }

        public TradeRequest()
        {
            Id = -1;
            Station = new SpaceStation();
            Item = new TradeItemEntry();
            Ship = new SpaceShipEntry();
        }

    }
}
