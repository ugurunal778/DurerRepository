using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class PageDto
    {
        public int Id { get; set; }
        public int LocaleId { get; set; }
        public int ParentId { get; set; }
        public string Permalink { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Lang { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsDurer { get; set; }
        public int? HazrefId { get; set; }
    }
}
