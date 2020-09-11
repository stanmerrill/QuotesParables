
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuotesParables.Models
{
    [Table("QuoteType")]
    public class QuoteType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteTypeId { get; set; }

        [Required]
        [MaxLength(30), MinLength(1)]
        [StringLength(30)]
        [Display(Name = "Quote Type Description")]
        public string QuoteTypeDescription { get; set; }


    }
}