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
            Teams = new List<Team>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("host_team")]
        [Display(Name = "Host team")]
        public int HostId { get; set; }
        [Required]
        [Column("guest_team")]
        [Display(Name = "Guest team")]
        public int GuestId { get; set; }
        [Required]
        [Column("date")]
        public DateTime Date { get; set; }
        [Required]
        [Column("tickets")]
        [Display(Name = "Number of tickets")]
        public int Tickets_amount { get; set; }
        [Column("score")]
        public string? Score { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
