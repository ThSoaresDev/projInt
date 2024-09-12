using proj_int.Repositories;

namespace proj_int.Services
{
    public class ParticipationService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public ParticipationService(IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<string> RegisterForEventAsync(int eventId, int userId)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(eventId);
            if (eventEntity == null)
            {
                return "Evento não encontrado.";
            }

            // Verificar se o evento já atingiu a quantidade máxima de participantes
            if (eventEntity.CurrentParticipants >= eventEntity.MaxParticipants)
            {
                return "Não é possível se inscrever. O evento atingiu o limite de vagas.";
            }

            // Inscrever o aluno no evento
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return "Usuário não encontrado.";
            }

            // Adicionar o participante e atualizar a contagem de participantes
            eventEntity.Participants.Add(user);
            eventEntity.CurrentParticipants++;
            await _eventRepository.UpdateEventAsync(eventEntity);

            return "Inscrição realizada com sucesso!";
        }
    }

}
