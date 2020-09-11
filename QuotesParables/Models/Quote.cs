using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotesParables.Models
{
    [Table("Quote")]
    public class Quote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteId { get; set; }

        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Quote Text")]
        public string QuoteText { get; set; }
        
        [MaxLength(30), MinLength(1)]
        [StringLength(30)]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        //[Required]
        [Display(Name = "Category 2")]
        public int CategoryId2 { get; set; }

        //[Required]
        [Display(Name = "Category 3")]
        public int CategoryId3 { get; set; }

        [Required]
        [ForeignKey("QuoteType")]
        public int QuoteTypeId { get; set; }
        public virtual QuoteType QuoteType { get; set; }
        
        [Display(Name = "Likes")]
        public int Likes { get; set; }

        [Required]
        [Display(Name = "Created by")]
        public int CreatedByLogonAccountId { get; set; }

        //[Required]
        [Display(Name = "Updted by")]
        public int UpdatedByLogonAccountId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:M/d/yy}")]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        //[Required]
        [DisplayFormat(DataFormatString = "{0:M/d/yy}")]
        [Display(Name = "Update Date")]

        public DateTime UpdateDate { get; set; }

        public string getUpdateUser()
        {
            try
            {
                LogonAccountRepository lar = new LogonAccountRepository();
                LogonAccount updateUser = lar.GetById(UpdatedByLogonAccountId);
                return updateUser.Name;

           }
            catch (Exception ex){
                return ex.Message;
            }
        }

        public string getCreateUser()
        {
            try
            {
                LogonAccountRepository lar = new LogonAccountRepository();
                LogonAccount updateUser = lar.GetById(CreatedByLogonAccountId);
                return updateUser.Name;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}