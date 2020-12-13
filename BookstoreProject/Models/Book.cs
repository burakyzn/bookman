using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name="Türkçe İsim")]
        public string Name_TR { get; set; }

        [Display(Name = "İngilizce İsim")]
        public string Name_EN { get; set; }

        [Display(Name = "Türkçe Tanım")]
        public string Description_TR { get; set; }

        [Display(Name = "İngilizce Tanım")]
        public string Description_EN { get; set; }

        [Display(Name = "Fiyat")]
        public int Price { get; set; }
        [Display(Name = "Stok")]

        public int Stock { get; set; }

        [Display(Name = "Basim Yıl")]
        public int PublishYear { get; set; }

        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        [Display(Name = "Yazar")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Aktiflik")]
        public bool Active { get; set; }

        [Display(Name = "Ana Fotograf")]
        public string MainPhoto { get; set; }

        [Display(Name = "İkinci Fotograf")]
        public string SecondPhoto { get; set; }

        [Display(Name = "Üçüncü Fotograf")]
        public string ThirdPhoto { get; set; }

    }
}
