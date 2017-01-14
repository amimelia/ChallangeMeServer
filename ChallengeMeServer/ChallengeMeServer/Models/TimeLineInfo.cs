using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Models
{
    public class TimeLineInfo
    {
        public TimeLineInfo (List<post> posts)
        {
            Posts = new List<PostInfo>();
            posts.ForEach(post =>
            {
                Posts.Add(new PostInfo(post));
            });
            Posts = Posts.OrderBy(post => post.PostCreateDate).ToList();
        }

        public List<PostInfo> Posts { get; set; }

        
    }
}