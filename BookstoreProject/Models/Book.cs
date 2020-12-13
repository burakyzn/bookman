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
        [Display(Name="Turkce Isim")]
        public string Name_TR { get; set; }
        [Display(Name = "Ingilizce Isim")]
        public string Name_EN { get; set; }
        [Display(Name = "Turkce Tanim")]
        public string Description_TR { get; set; }
        [Display(Name = "Ingilizce Tanim")]
        public string Description_EN { get; set; }
        [Display(Name = "Fiyat")]
        public int Price { get; set; }
        [Display(Name = "Stok")]
        public int Stock { get; set; }
        [Display(Name = "Basim Yil")]
        public int PublishYear { get; set; }
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public Category Category { get; set; }
        [Display(Name = "Dil")]
        public int LanguageId { get; set; }
        [Display(Name = "Dil")]
        public Language Language { get; set; }
        [Display(Name = "Yazar")]
        public int AuthorId { get; set; }
        [Display(Name = "Yazar")]
        public Author Author { get; set; }
        [Display(Name = "Eklenme Tarihi")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Aktiflik")]
        public bool Active { get; set; }

        [Display(Name = "Ana Fotograf")]
        public string MainPhoto { get; set; }
        [Display(Name = "Ikinci Fotograf")]
        public string SecondPhoto { get; set; }
        [Display(Name = "Ucuncu Fotograf")]
        public string ThirdPhoto { get; set; }

    }
}
