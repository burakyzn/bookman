using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Kitap
    {
        public int Id { get; set; }
        public string Ad_TR { get; set; }

        public string Ad_EN { get; set; }

        public string Tanim_TR { get; set; }

        public string Tanim_EN { get; set; }

        public int Fiyat { get; set; }

        public int Stok { get; set; }

        public int BasimYili { get; set; }

        public int KategoriId { get; set; }

        public Kategori Kategori { get; set; }

        public int DilId { get; set; }

        public Dil Dil { get; set; }
    }
}
