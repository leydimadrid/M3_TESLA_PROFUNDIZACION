using System;

namespace TeslaACDC.Data.Models;

public class Album : BaseEntity<int>
{
    public string Name { get; set; } = String.Empty;
    public int Year { get; set; }
    public Artist Artist { get; set; }
    public Gender Gender { get; set; }
    public string GenderName => Gender.ToString();
}

public enum Gender
{
    Reggaeton,
    Vallenato,
    Champeta,
    Unknown
}

