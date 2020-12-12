using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Display(Name = "Turkce Isim")]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
