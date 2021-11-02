using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    [DataContract]
    public class NewsImageDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int NewsId { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
    }
}
