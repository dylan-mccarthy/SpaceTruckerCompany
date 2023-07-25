using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaceTruckerCompany.API.Models
{
    public class SpaceShipRoute : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int ShipId { get; set; }
        public int StationId { get; set; }
        public double CurrentCoordinatesX { get; set; }
        public double CurrentCoordinatesY { get; set; }
        public double DestinationCoordinatesX { get; set; }
        public double DestinationCoordinatesY { get; set; }

        public SpaceShipRoute()
        {
            PlayerId = 0;
            ShipId = 0;
            StationId = 0;
            CurrentCoordinatesX = 0;
            CurrentCoordinatesY = 0;
            DestinationCoordinatesX = 0;
            DestinationCoordinatesY = 0;
        }
    }
}
