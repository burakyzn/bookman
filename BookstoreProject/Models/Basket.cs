using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreProject.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public bool Active { get; set; }
    }
}
