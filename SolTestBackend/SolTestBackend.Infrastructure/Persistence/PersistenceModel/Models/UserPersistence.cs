using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Infrastructure.Persistence.PersistenceModel.Models
{
    [Table("users")]
    internal class UserPersistence
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Column("email")]
        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
