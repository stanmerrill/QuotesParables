﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuotesParables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Collections;

namespace QuotesParables.ViewModels
{
    public class EditQuoteViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteId { get; set; }

        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Quote Text")]
        public string QuoteText { get; set; }

        [StringLength(10000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Quote Text Raw")]
        public string QuoteTextRaw { get; set; }

        [MaxLength(30), MinLength(1)]
        [StringLength(30)]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }

        [Required]
        [Display(Name = "Category 1")]
        [DefaultValue(33)]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        [Display(Name = "Category 2")]
        [DefaultValue(33)]
        public int CategoryId2 { get; set; }

        //[Required]
        [Display(Name = "Category 3")]
        [DefaultValue(33)]
        public int CategoryId3 { get; set; }

        [Required]
        [ForeignKey("QuoteType")]
        public int QuoteTypeId { get; set; }
        public virtual QuoteType QuoteType { get; set; }

        [Display(Name = "Likes")]
        public int Likes { get; set; }

        [MaxLength(1)]
        [StringLength(1)]
        [Display(Name = "Approved")]
        public string Approved { get; set; }

        [MaxLength(30), MinLength(1)]
        [StringLength(30)]
        [Display(Name = "Contributor")]
        public string Contributor { get; set; }

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
        public IDictionary<int, string> CategoryDictionary;
        public ArrayList CategoryArraylist;
        public string imageLink { get; set; }
        public string correctValidationValue { get; set; }
        public string initialValidationValue { get; set; }
        public string categorySelectBox { get; set; }
        public string category2SelectBox { get; set; }
        public string category3SelectBox { get; set; }
        public string typeSelectBox { get; set; }
        public string validationImage { get; set; }
        public string getUpdateUser()
        {
            try
            {
                LogonAccountRepository lar = new LogonAccountRepository();
                LogonAccount updateUser = lar.GetById(UpdatedByLogonAccountId);
                return updateUser.Name;

            }
            catch (Exception ex)
            {
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
        public string repeatUpdate { get; set; }
    }
}