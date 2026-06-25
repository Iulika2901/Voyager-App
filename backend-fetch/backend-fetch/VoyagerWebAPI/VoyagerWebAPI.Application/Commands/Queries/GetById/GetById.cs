using MediatR;
using VoyagerWebAPI.Application.DTOs;

namespace VoyagerWebAPI.Application.Queries.GetById;

public record GetTripByIdQuery(int Id) : IRequest<TripDto>;