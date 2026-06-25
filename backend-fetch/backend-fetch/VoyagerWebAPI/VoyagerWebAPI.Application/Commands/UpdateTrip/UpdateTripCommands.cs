using System;
using MediatR;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Domain;

namespace VoyagerWebAPI.Application.Commands.UpdateTrip;

public record UpdateTripCommand(
    int Id,
    string Name, 
    string DestinationName, 
    string DestinationCountry, 
    DateTime StartDate, 
    DateTime EndDate, 
    TripStatus Status, 
    decimal Budget) : IRequest<TripDto>;