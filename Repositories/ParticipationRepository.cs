using proj_int.Models;
using proj_int.Context;
using Microsoft.EntityFrameworkCore;

namespace proj_int.Repositories
{
    public interface IParticipationRepository
    {
        Task<IEnumerable<Participation>> GetParticipationsByEventIdAsync(int eventId);
        Task RequestParticipationAsync(int eventId, int userId);
        Task ApproveParticipationAsync(int participationId);
        Task RejectParticipationAsync(int participationId);
    }

    public class ParticipationRepository : IParticipationRepository
    {
        private readonly AppDbContext _context;

        public ParticipationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Participation>> GetParticipationsByEventIdAsync(int eventId)
        {
            return await _context.Participations
                                 .Include(p => p.User)
                                 .Where(p => p.EventId == eventId)
                                 .ToListAsync();
        }

        public async Task RequestParticipationAsync(int eventId, int userId)
        {
            var participation = new Participation
            {
                EventId = eventId,
                UserId = userId,
                Status = "pendente"
            };
            await _context.Participations.AddAsync(participation);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveParticipationAsync(int participationId)
        {
            var participation = await _context.Participations.FindAsync(participationId);
            if (participation != null)
            {
                participation.Status = "aprovado";
                await _context.SaveChangesAsync();
            }
        }

        public async Task RejectParticipationAsync(int participationId)
        {
            var participation = await _context.Participations.FindAsync(participationId);
            if (participation != null)
            {
                participation.Status = "rejeitado";
                await _context.SaveChangesAsync();
            }
        }
    }

}
