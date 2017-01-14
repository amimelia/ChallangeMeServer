using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChallengeMeServer.Models
{
    public class NewsFeedInfo
    {
        public NewsFeedInfo(List<post> newsFeed)
        {
            NewsFeed = new List<PostInfo>();
            newsFeed.ForEach(post =>
            {
                NewsFeed.Add(new PostInfo(post));
            });
            NewsFeed = NewsFeed.OrderBy(post => post.PostCreateDate).ToList();
        }
        public List<PostInfo> NewsFeed { get; set; }
    }
}