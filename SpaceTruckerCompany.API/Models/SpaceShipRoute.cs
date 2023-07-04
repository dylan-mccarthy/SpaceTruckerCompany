namespace SpaceTruckerCompany.API.Models
{
    public class SpaceShipRoute : IEntity
    {
        public string Id { get; set; }
        public string PlayerId { get; set; }
        public SpaceShipEntry Ship { get; set; }
        public int[] CurrentCoordinates { get; set; }
        public int[] DestinationCoordinates { get; set; }

        public SpaceShipRoute()
        {
            Id = System.Guid.NewGuid().ToString();
            PlayerId = "";
            Ship = new SpaceShipEntry();
            CurrentCoordinates = new int[2] { 0, 0 };
            DestinationCoordinates = new int[2] { 0, 0 };
        }
    }
}
