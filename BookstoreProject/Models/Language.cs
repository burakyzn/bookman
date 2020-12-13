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

        [Display(Name = "Türkçe İsim")]
        public string Name_TR { get; set; }

        [Display(Name = "İngilizce İsim")]
        public string Name_EN { get; set; }
    }
}
