namespace SpaceTruckerCompany.API.Models
{
    public class SpaceShipRoute : IEntity
    {
        public string Id { get; set; }
        public string PlayerId { get; set; }
        public string ShipId { get; set; }
        public string StationId { get; set; }
        public double CurrentCoordinatesX { get; set; }
        public double CurrentCoordinatesY { get; set; }
        public double DestinationCoordinatesX { get; set; }
        public double DestinationCoordinatesY { get; set; }

        public SpaceShipRoute()
        {
            Id = System.Guid.NewGuid().ToString();
            PlayerId = "";
            ShipId = "";
            StationId = "";
            CurrentCoordinatesX = 0;
            CurrentCoordinatesY = 0;
            DestinationCoordinatesX = 0;
            DestinationCoordinatesY = 0;
        }
    }
}
