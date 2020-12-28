using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Language
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z]$", ErrorMessage = "Sadece Harf Giriniz.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Minimum {2} Karakter Maximum {1} Karakter Girilebilir.")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz")]
        [Display(Name = "Türkçe İsim")]
        public string Name_TR { get; set; }

        [RegularExpression(@"^[a-zA-Z]{3,50}$", ErrorMessage = "Sadece Harf Giriniz.")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz")]
        [Display(Name = "İngilizce İsim")]
        public string Name_EN { get; set; }

        [Display(Name = "Aktiflik")]
        public bool Active { get; set; }
    }
}
