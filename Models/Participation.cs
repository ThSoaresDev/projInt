using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace proj_int.Models
{
    public class Participation
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

}
