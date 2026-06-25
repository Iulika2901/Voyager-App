using System;

namespace VoyagerWebAPI.Domain;

public class Destination
{
    public string Name { get; set; }
    public string Country { get; set; }

    Destination() { }
    
    public Destination(string name, string country)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country cannot be empty");

        Name = name;
        Country = country;
    }
}