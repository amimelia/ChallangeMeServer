using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Models
{
    public class PostInfo
    {
        public int PostID { get; set; }
        public string PostContent { get; set; }
        public string PostDescription { get; set; }
        public int? PostLikes { get; set; }
        public DateTime? PostCreateDate { get; set; }
        public virtual ICollection<PostCommentInfo> PostComments { get; set; }
    }
}