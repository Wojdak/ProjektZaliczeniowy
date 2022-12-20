using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt_zaliczeniowy.Models
{
    [Table("Teams")]
    public class Team
    {
        public Team()
        {
            Players = new List<Player>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("country")]
        public string Country { get; set; }
        [Required]
        [Column("city")]
        public string City { get; set; }
        [Required]
        [Column("stadium")]
        public string Stadium { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
