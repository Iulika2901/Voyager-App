using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VoyagerWebAPI.Application.Interfaces;
using VoyagerWebAPI.Domain;
using VoyagerWebAPI.Infrastructure.Persistance;

namespace VoyagerWebAPI.Infrastructure.Persistence;

public class TripRepository : ITripRepository
{
    private readonly ApplicationDbContext _context;

    public TripRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trip>> GetAllAsync()
    {
         return await _context.Trips
            .Include(t => t.Expenses)
            .ToListAsync();
    }

    public async Task<Trip> GetByIdAsync(int id)
    {
        return await _context.Trips
            .Include(t => t.Expenses)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Trip> CreateAsync(Trip trip)
    {
        await _context.Trips.AddAsync(trip);
        
        await _context.SaveChangesAsync();
        
        return trip;
    }

    public async Task<Trip> UpdateAsync(int id, Trip trip)
    {
         _context.Trips.Update(trip);
        
        await _context.SaveChangesAsync();
        
        return trip;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var trip = await _context.Trips.FindAsync(id);
        
        if (trip == null) return false;

        _context.Trips.Remove(trip);
        
        await _context.SaveChangesAsync();
        
        return true;
    }
}