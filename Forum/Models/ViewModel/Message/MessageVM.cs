using Forum.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models.ViewModel.Message
{
    public class MessageVM
    {
        public MessageVM()
        {

        }

        public MessageVM(MessageDTO row)
        {
            MessageId = row.MessageId;
            ArticleId = row.ArticleId;
            UID = row.UID;
            Content = row.Content;
            CreateTime = row.CreateTime;
        }

        public int MessageId { get; set; }
        public int ArticleId { get; set; }
        public int UID { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}