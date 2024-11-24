using Demo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Demo.Domain.Models
{
    public class EntityBase
    {
        [JsonPropertyName("id")]
        [Required]
        [Key]
        [Column("id")]
        public long ID { get; set; }

        [JsonPropertyName("createdBy")]
        [Required]
        [MaxLength(100)]
        [Column("created_by")]
        public string CreatedBy { get; set; } = "DEFAULT";

        [JsonPropertyName("createdDate")]
        [Required]
        [Column("created_date", TypeName ="DATETIME2")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("updatedBy")]
        [Required]
        [MaxLength(100)]
        [Column("updated_by")]
        public string UpdatedBy { get; set; } = "DEFAULT";

        [JsonPropertyName("updatedDate")]
        [Required]
        [Column("updated_date", TypeName = "DATETIME2")]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public EntityActions DbAction { get; set; } = EntityActions.None;
    }
}
