using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WalletCash.DTOs;

public class StoresQuery
{
    [Required]
    [FromQuery(Name = "latitude")]
    public double Latitude { get; set; }

    [Required]
    [FromQuery(Name = "longitude")]
    public double Longitude { get; set; }

    [FromQuery(Name = "radius")]
    public double Radius { get; set; } = 5;
}
