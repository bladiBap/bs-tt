using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SolTestBackend.Infrastructure.Persistence.PersistenceModel.Models
{
    [Table("products")]
    internal class ProductPersistence
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("sku")]
        [Required]
        [MaxLength(50)]
        public string Sku { get; set; } = string.Empty;

        [Column("name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("price")]
        [Required]
        public decimal PriceAmount { get; set; }

        [Column("currency_id")]
        [Required]
        public Guid CurrencyId { get; set; }

        [ForeignKey(nameof(CurrencyId))]
        public virtual CurrencyPersistence Currency { get; set; } = null!;

        [Column("stock")]
        [Required]
        public int Stock { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
