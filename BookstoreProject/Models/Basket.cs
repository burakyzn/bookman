using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Basket
    {
        [Display(Name = "Sipariş Numarası")]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Durum { get; set; }
        public bool Active { get; set; }
    }
}
