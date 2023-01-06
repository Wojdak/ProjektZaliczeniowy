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
        [Column("howManyPeople")]
        [Display(Name = "Number of people")]
        public int howManyPeople { get; set; }
        [Required]
        [Column("seats")]
        public string? Seats { get; set; }
        [Required]
        [Column("totalPrice")]
        [Display(Name = "Total price")]
        public int totalPrice { get; set; }
        [Column("status")]
        [Display(Name = "Status")]
        public string? Status { get; set; }
        public int MatchId { get; set; }
        public virtual Match? Match { get; set; }

        public string? UserId { get; set; }
    }
}
