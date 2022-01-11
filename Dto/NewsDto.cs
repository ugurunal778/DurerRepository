using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class NewsDto
    {
        public int Id { get; set; }
        public int LocaleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Lang { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
