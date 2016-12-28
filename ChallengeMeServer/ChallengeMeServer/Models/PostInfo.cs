using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Models
{
    public class PostInfo
    {
        private static List<PostCommentInfo> _fillPostCommentList(List<post_comments> list)
        {
            var postCommentInfo = new List<PostCommentInfo>();
            list = list.OrderBy(x => x.PostCommentCreateDate).ToList();
            list.ForEach(postComments =>
            {
                postCommentInfo.Add(new PostCommentInfo(postComments));
            });
            return postCommentInfo;
        }

        public PostInfo(post post)
        {
            PostContent = post.PostContent;
            PostDescription = post.PostDiscription;
            PostLikes = post.PostLikes;
            PostCreateDate = post.PostCreateDate;
            PostComments = _fillPostCommentList(post.post_comments.ToList());

        }
        public int PostID { get; set; }
        public string PostContent { get; set; }
        public string PostDescription { get; set; }
        public int? PostLikes { get; set; }
        public DateTime? PostCreateDate { get; set; }
        public virtual ICollection<PostCommentInfo> PostComments { get; set; }
    }
}