using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class PageFileDto
    {
        public int Id { get; set; }
        public int LocaleId { get; set; }
        public int PageId { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public int Order { get; set; }
        public string Lang { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
