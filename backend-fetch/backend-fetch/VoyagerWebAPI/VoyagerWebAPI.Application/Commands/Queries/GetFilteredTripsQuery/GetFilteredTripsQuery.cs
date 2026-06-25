using MediatR;
using VoyagerWebAPI.Domain;
using System.Collections.Generic;

namespace VoyagerWebAPI.Application.Queries.GetFilteredTrips;

public class GetFilteredTripsQuery : IRequest<List<Trip>>
{
    public string? DestinationName { get; set; }
    public TripStatus? Status { get; set; }
    public decimal? MaxBudget { get; set; }
}