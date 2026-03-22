using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Infrastructure.Persistence.PersistenceModel.Models
{
    [Table("currencies")]
    internal class CurrencyPersistence
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("symbol")]
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ProductPersistence> Products { get; set; } = new List<ProductPersistence>();
    }
}
