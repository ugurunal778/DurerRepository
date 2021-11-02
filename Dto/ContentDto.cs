using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    [DataContract]
    public class ContentDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CategoryId { get; set; }
        [DataMember]
        public int ContentId { get; set; }
        [DataMember]
        public int LocaleId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string FileTitle { get; set; }
        [DataMember]
        public string FileUrl { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string Lang { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
    }
}
