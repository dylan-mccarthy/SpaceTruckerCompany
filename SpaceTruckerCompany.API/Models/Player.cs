using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SpaceTruckerCompany.API.Models;
public class Player : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public double Credits { get; set; }
    public int NumberOfShips { get; set; }
    public List<SpaceShipEntry> Ships { get; set; }

    public Player(string name)
    {
        Id = -1;
        Username = "";
        Name = name;
        Credits = 1000;
        NumberOfShips = 1;
        Ships = new List<SpaceShipEntry>();
    }
}