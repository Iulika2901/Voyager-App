using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VoyagerWebAPI.Application.Interfaces;

namespace VoyagerWebAPI.Application.Commands.DeleteTrip;

public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, bool>
{
    private readonly ITripRepository _repository;

    public DeleteTripCommandHandler(ITripRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}