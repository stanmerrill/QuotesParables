using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotesParables.Models
{
    [Table("QuoteCategory")]
    public class QuoteCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteCategoryId { get; set; }

        [Required]
        [ForeignKey("Quote")]
        public int QuoteId { get; set; }
        public virtual Quote Quote { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}