using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Authors.Api.Models
{
    public class Academic
    {
        public int AcademicId { get; set; }
        public string AcademicGuid { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
        public DateTime? DateGraduation { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
