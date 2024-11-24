using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Demo.Domain.Extensions;

namespace Demo.Domain.Models
{
    [Table("PHONE_NUMBERS")]
    public partial class PhoneNumber : EntityBase
    {
        #region properties

        /// <summary>
        /// Gets/Sets the optional country code used when dialing out of the country.
        /// </summary>
        /// <remarks>
        ///  Phone numbers components are stored as strings to allow for proeceeding "0"s
        /// </remarks>
        [Column("country_code")]
        [Length(minimumLength:3, maximumLength:3)]
        public string? CountryCode { get; set; }

        /// <summary>
        /// Gets/Sets the 3-digit area code
        /// </summary>
        /// <remarks>
        ///  Phone numbers components are stored as strings to allow for proeceeding "0"s
        /// </remarks>
        [Column("area_code")]
        [Length(minimumLength: 3, maximumLength: 3)]
        public string? AreaCode{ get; set; }

        /// <summary>
        /// Gets/Sets the 3 digit phone number prefix
        /// </summary>
        /// <remarks>
        ///  Phone numbers components are stored as strings to allow for proeceeding "0"s
        /// </remarks>
        [Required]
        [Column("prefix")]
        [Length(minimumLength: 3, maximumLength: 3)]
        public string? Prefix { get; set; }

        /// <summary>
        /// Gets/Sets the 4-digit line number
        /// </summary>
        /// <remarks>
        ///  Phone numbers components are stored as strings to allow for proeceeding "0"s
        /// </remarks>
        [Required]
        [Column("line_number")]
        [Length(minimumLength: 4, maximumLength: 4)]
        public string? LineNumber { get; set; }

        /// <summary>
        /// Gets/Sets the optional extension of the phone number
        /// </summary>
        /// <remarks>
        ///  Phone numbers components are stored as strings to allow for proeceeding "0"s
        /// </remarks>
        [Column("extension")]
        [Length(minimumLength: 2, maximumLength: 9)]
        public string? Extension { get; set; }

        /// <summary>
        /// Gets/Sets the label of the Phone Number (e.g. Home, Cell, Business, etc.)
        /// </summary>
        [Column("label")]
        [MaxLength(120)]
        public string? Label { get; set; }

        #endregion properties

        #region navigation properties


        [ForeignKey("Contact")]
        [Column("contact_id")]
        public long? ContactId { get; set; }

        public virtual Contact Contact { get; set; } = null!;

        #endregion navigation properties

        #region public

        public static bool TryParse(string phoneNumber, out PhoneNumber phone)
        {
            String stage    = phoneNumber.ToLower();
            phone = new PhoneNumber();

            // remove all whitespace first 
            //
            stage = Regex.Replace(stage, @"\s", "");

            // look for an extension 
            //
            Int32 xtIDX = stage.LastIndexOf('x');
            if (xtIDX != -1)
            {
                phone.Extension = stage.Substring(xtIDX).RemoveAlpha();
                stage = stage.Substring(0, xtIDX);
            }
            //
            // strip out any remaining Alpha characters and parse out the number BACKWARDS.
            stage = stage.RemoveAlpha();
            stage = stage.Replace(".", String.Empty);
            phone.LineNumber    = (stage.Length - 4 >= 0) ? stage.Substring(stage.Length - 4) : null;
            phone.Prefix        = (stage.Length - 7 >= 0) ? stage.Substring(stage.Length - 7, 3) : null;
            phone.AreaCode      = (stage.Length - 10 >= 0) ? stage.Substring(stage.Length - 10, 3) : null;
            phone.CountryCode   = (stage.Length - 10 > 0) ? stage.Substring(0, stage.Length - 10) : null;

            return true;
        }

        #endregion public

    }
}
