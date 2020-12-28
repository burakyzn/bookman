using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Sadece Harf Giriniz.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} En Az {2} En Fazla {1} Karakter Uzunluğunda Olmalıdır.")]
        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        [Display(Name = "Türkçe İsim")]
        public string Name_TR { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Sadece Harf Giriniz.")]
        [Required(ErrorMessage = "Bu Alan Boş Bırakılamaz")]
        [Display(Name = "İngilizce İsim")]
        public string Name_EN { get; set; }

        [Display(Name = "Aktiflik")]
        public bool Active { get; set; }
    }
}
