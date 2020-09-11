using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotesParables.Models
{
    [Table("Favorite")]
    public class Favorite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteCategoryId { get; set; }

        [Required]
        [ForeignKey("LogonAccount")]
                public int? LogonAcountId { get; set; }
        public virtual LogonAccount LogonAccount { get; set; }

        [Required]
        [ForeignKey("Quote")]
        
        public int QuoteId { get; set; }
        public virtual Quote Quote { get; set; }
    }
}