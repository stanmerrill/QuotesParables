using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotesParables.Models
{
    [Table("LogonAccount")]
    public class LogonAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogonAccountId { get; set; }

        //[Required]
        [MaxLength(100), MinLength(1)]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //[Required]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Invalid Email Address")]
        [StringLength(100)]
        [Display(Name = "UserId - Email Address")]
        public string EMail { get; set; }


        //[Required]
        [MaxLength(100), MinLength(1)]
        [StringLength(100)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required]
        [StringLength(1)]
        [Display(Name = "Security Level")]
        public string SecurityLevel { get; set; }
        /*
         *    5 - Super Admin - Create Users - Edit All events
         *    3 - Administer - events - create students
         *    1 - Non member
         */


    }
}
