using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Models
{
    public class PostCommentInfo
    {
        public int PostCommentID { get; set; }
        public string PostCommentDiscription { get; set; }
        public int? PostCommentLike { get; set; }
        public string PostCommentContent { get; set; }
    }
}