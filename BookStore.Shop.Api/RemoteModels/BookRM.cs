using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.RemoteModels
{
    public class BookRM
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Guid? AuthoBook { get; set; }
    }
}
