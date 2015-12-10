/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace BAMvvm.Models
{
    public class ValidationTestModel
    {
        [Required]
        [Display(Name = "Monkey Tails")]
        public string MonkeyTails { get; set; }

        [Compare("MonkeyTails", ErrorMessage = "MonkeyTails must match")]
        public string ConfirmMonkeyTails { get; set; }

        [Required]
        [CreditCard]
        public string CreditCard { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Required { get; set; }

        [MinLength(6)]
        [MaxLength(255)]
        [Required]
        public string MinSixMaxTwoFiftySix { get; set; }
    }
}