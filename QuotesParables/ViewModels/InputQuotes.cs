using System.ComponentModel.DataAnnotations;

namespace QuotesParables.ViewModels
{
    public class InputQuotes
    {
        [StringLength(100000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Input")]

        public string QuotesInput { get; set; }
        [StringLength(100000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Duplicate Output")]
        public string QuotesDuplicates { get; set; }
    }
}