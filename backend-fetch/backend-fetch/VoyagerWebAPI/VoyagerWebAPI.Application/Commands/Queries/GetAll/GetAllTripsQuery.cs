using System.Collections.Generic;
using MediatR;
using VoyagerWebAPI.Application.DTOs;

namespace VoyagerWebAPI.Application.Queries.GetAll;

public record GetAllTripsQuery() : IRequest<IEnumerable<TripDto>>;