using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace accountmanager
{
    public class Post
    {
        int postID;
        string postTitle;
        string postText;
        string actionableItem;
        string postUsername;
        string postDateTime;
        string isSensored;

        public int PostID { get => postID; set => postID = value; }
        public string PostTitle { get => postTitle; set => postTitle = value; }
        public string PostText { get => postText; set => postText = value; }
        public string ActionableItem { get => actionableItem; set => actionableItem = value; }
        public string PostUsername { get => postUsername; set => postUsername = value; }
        public string PostDateTime { get => postDateTime; set => postDateTime = value; }
        public string IsSensored { get => isSensored; set => isSensored = value; }
    }
}