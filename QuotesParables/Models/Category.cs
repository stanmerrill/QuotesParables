using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotesParables.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        //[Required]
        [MaxLength(30), MinLength(1)]
        [StringLength(30)]
        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}