using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class ContentBannerDto
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string ImageUrl { get; set; }
        public string Lang { get; set; }
    }
}
