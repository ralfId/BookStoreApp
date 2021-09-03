using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Authors.Api.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorGuid { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime? Birthday { get; set; }

        public ICollection<Academic> AcademicDegreeList { get; set; }
    }
}
