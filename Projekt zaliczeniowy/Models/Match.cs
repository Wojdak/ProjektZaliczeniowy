using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_zaliczeniowy.Models
{
    [Table("Matches")]
    public class Match
    {
        public Match()
        {
            Tickets = new List<Ticket>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("host_team")]
        public Team Host { get; set; }
        [Required]
        [Column("guest_team")]
        public Team Guest { get; set; }
        [Required]
        [Column("date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("tickets")]
        public int Tickets_amount { get; set; }
        [Column("score")]
        public string? Score { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
