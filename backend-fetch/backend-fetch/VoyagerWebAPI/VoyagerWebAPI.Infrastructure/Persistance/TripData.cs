using System;
using System.Collections.Generic;
using VoyagerWebAPI.Domain;

namespace VoyagerWebAPI.Infrastructure.Persistence;

public class TripData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DestinationData Destination { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TripStatus Status { get; set; }
    public decimal Budget { get; set; }
    public List<ExpenseData> Expenses { get; set; } = new();
}