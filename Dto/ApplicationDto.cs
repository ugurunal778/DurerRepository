using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class ApplicationDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string HTML { get; set; }
        public string Form { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
