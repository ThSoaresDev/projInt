using proj_int.Models;
using proj_int.Context;
using Microsoft.EntityFrameworkCore;

namespace proj_int.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int eventId);
        Task CreateEventAsync(Event eventEntity);
        Task UpdateEventAsync(Event eventEntity);
        Task DeleteEventAsync(int id);
    }

    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Event> GetEventByIdAsync(int eventId)
        {
            return await _context.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task UpdateEventAsync(Event eventEntity)
        {
            _context.Events.Update(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateEventAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity != null)
            {
                _context.Events.Remove(eventEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.Include(e => e.Creator).ToListAsync();
        }
    }

}
