namespace proj_int.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public int MaxParticipants { get; set; } // Quantidade máxima de vagas
        public int CurrentParticipants { get; set; } = 0; // Contagem de participantes atuais
        public int CreatedBy { get; set; } // Chave estrangeira
        public User Creator { get; set; } // Relação muitos-para-um (professor)
        public ICollection<User> Participants { get; set; } // Relação muitos-para-muitos (alunos)
    }

}
