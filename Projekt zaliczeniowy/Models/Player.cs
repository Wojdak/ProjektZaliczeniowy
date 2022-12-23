using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_zaliczeniowy.Models
{
    [Table("Players")]
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("first_name")]
        public string? Name { get; set; }
        [Required]
        [Column("surname")]
        public string? Surname { get; set; }
        [Required]
        [Column("nationality")]
        public string? Nationality { get; set; }
        [Required]
        [Column("date_of_birth")]
        [Display(Name="Date of birth")]
        public DateTime? Date_of_birth { get; set; }
        [Required]
        [Column("position")]
        public string? Position { get; set; }

        [Display(Name = "Team")]
        public int TeamId { get; set; }
        public virtual Team? Team { get; set; }
    }
}