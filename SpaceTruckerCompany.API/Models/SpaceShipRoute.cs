namespace SpaceTruckerCompany.API.Models
{
    public class SpaceShipRoute : IEntity
    {
        public string Id { get; set; }
        public string PlayerId { get; set; }
        public string ShipId { get; set; }
        public string StationId { get; set; }
        public int[] CurrentCoordinates { get; set; }
        public int[] DestinationCoordinates { get; set; }

        public SpaceShipRoute()
        {
            Id = System.Guid.NewGuid().ToString();
            PlayerId = "";
            ShipId = "";
            StationId = "";
            CurrentCoordinates = new int[2] { 0, 0 };
            DestinationCoordinates = new int[2] { 0, 0 };
        }
    }
}
