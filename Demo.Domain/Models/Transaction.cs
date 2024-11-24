using Demo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Models
{
    [Table("TRANSACTIONS")]
    public class Transaction : EntityBase
    {
        [Required]
        [Column ("amount", TypeName="decimal(24,4)")]
        public decimal Amount { get; set; } = 0;

        [Required]
        [Column("timestamp", TypeName ="DATETIME2")]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(200)]
        [Column("description")]
        public string Description { get; set; } = "Default";

        [MaxLength(200)]
        [Column("reference_no")]
        public string? ReferenceNumber { get; set; }

        [Required]
        [Column("transaction_type", TypeName = "int")]
        public TransactionTypes TransactionType { get; set; } = TransactionTypes.None;

        [NotMapped]
        public decimal Credit
        {
            get { return (this.Amount < 0) ? 0 : this.Amount; }
        }

        [NotMapped]
        public decimal Debit
        {
            get { return (this.Amount < 0) ? (this.Amount * -1) : 0; }
        }

    }
}
