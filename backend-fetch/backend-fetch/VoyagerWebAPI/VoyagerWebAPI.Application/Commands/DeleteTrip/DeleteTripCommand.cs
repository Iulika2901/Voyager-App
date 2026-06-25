using System;
using MediatR;
using VoyagerWebAPI.Application.DTOs;
using VoyagerWebAPI.Domain;

namespace VoyagerWebAPI.Application.Commands.DeleteTrip;

using MediatR;

public record DeleteTripCommand(int Id) : IRequest<bool>;