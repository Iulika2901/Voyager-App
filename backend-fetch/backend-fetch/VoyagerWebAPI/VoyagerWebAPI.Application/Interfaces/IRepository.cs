using System.Collections.Generic;
using System.Threading.Tasks;
using VoyagerWebAPI.Domain;

namespace VoyagerWebAPI.Application.Interfaces;

public interface ITripRepository
{
    Task<IEnumerable<Trip>> GetAllAsync();
    Task<Trip> GetByIdAsync(int id);
    Task<Trip> CreateAsync(Trip trip);
    Task<Trip> UpdateAsync(int id, Trip trip);
    Task<bool> DeleteAsync(int id);
}