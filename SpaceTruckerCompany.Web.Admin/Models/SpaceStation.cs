using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaceTruckerCompany.Web.Admin.Models
{
    public class SpaceStation : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TradeItemEntry> TradeItems { get; set; }
        public List<SpaceShipEntry> Ships { get; set; }
        public double CoordinatesX { get; set; }
        public double CoordinatesY { get; set; }

        public SpaceStation()
        {
            Id = -1;
            Name = "";
            TradeItems = new List<TradeItemEntry>();
            Ships = new List<SpaceShipEntry>();
            CoordinatesX = 0;
            CoordinatesY = 0;
        }
    }
}
