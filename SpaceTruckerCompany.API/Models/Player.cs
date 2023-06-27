using System;

namespace SpaceTruckerCompany.API.Models;
public class Player : IEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Credits { get; set; }
    public int NumberOfShips { get; set; }
    public List<SpaceShipEntry> Ships { get; set; }

    public Player(string name)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Credits = 1000;
        NumberOfShips = 1;
        Ships = new List<SpaceShipEntry>();
    }
}