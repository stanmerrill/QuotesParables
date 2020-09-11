
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuotesParables.Models
{
    [Table("Rating")]
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; }

        [Required]
        [MaxLength(30), MinLength(1)]
        [StringLength(30)]
        [Display(Name = "Rating Description")]
        public string RatingTypeDescription { get; set; }


    }
}