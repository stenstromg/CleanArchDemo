using Demo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Domain.Models
{

    [Table("ACCOUNTS")]
    public class Account : EntityBase
    {
        #region properties

        [Required]
        [MaxLength(50)]
        [Column("account_number")]
        public string? AccountNumber { get; set; }

        [MaxLength(120)]
        [Column("nickname")]
        public string? Nickname { get; set; }

        [Required]
        [Column("account_type", TypeName ="int")]
        public AccountTypes Type { get; set; } = AccountTypes.None;


        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        #endregion properties

    }
}
