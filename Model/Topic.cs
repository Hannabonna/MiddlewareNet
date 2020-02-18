using System;

namespace MiddlewareNet
{
    public class Topic
    {
       public string Content { get; set; }

       public string Title { get; set; }

       public int MemberId { get; set; }
    }

    public class IdTopic : Topic
    {
        public int Id { get; set; }
    }
}
