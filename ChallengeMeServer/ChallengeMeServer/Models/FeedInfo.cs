using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Models
{
    public class FeedInfo
    {
        public FeedInfo (List<post> posts)
        {
            Posts = new List<PostInfo>();
            posts.ForEach(post =>
            {
                Posts.Add(new PostInfo
                {
                    PostContent = post.PostContent,
                    PostDescription = post.PostDiscription,
                    PostLikes = post.PostLikes,
                    PostCreateDate = post.PostCreateDate,
                    PostComments = _fillPostCommentList(post.post_comments.ToList())
                });
            });
            Posts = Posts.OrderBy(post => post.PostCreateDate).ToList();
        }

        public List<PostInfo> Posts { get; set; }

        private static List<PostCommentInfo> _fillPostCommentList(List<post_comments> list)
        {
            var postCommentInfo = new List<PostCommentInfo>();
            list.ForEach(x =>
            {
                postCommentInfo.Add(new PostCommentInfo
                {
                    PostCommentContent = x.PostCommentContent,
                    PostCommentDescription = x.PostCommentDiscription,
                    PostCommentLike = x.PostCommentLike
                });
            });
            return postCommentInfo;
        }
    }
}