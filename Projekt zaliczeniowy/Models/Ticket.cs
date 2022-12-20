using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_zaliczeniowy.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("amount")]
        public int Amount { get; set; }
        [Required]
        [Column("seats")]
        public string Seats { get; set; }
        [Required]
        [Column("price")]
        public decimal Price { get; set; }
        [HiddenInput]
        [Column("status")]
        public string? Status { get; set; }

        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
    }
}
