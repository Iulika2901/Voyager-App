namespace VoyagerWebAPI.Application.Commands.AddTrip; 

using MediatR;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Domain;
public record AddTripCommand(
    string Name,
    Destination Destination,
    DateTime StartDate,
    DateTime EndDate,
    TripStatus Status,
    decimal Budget
) : IRequest<TripDto>;