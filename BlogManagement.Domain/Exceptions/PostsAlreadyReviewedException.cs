using System;

namespace BlogManagement.Domain.Exceptions
{
    public class PostsAlreadyReviewedException : Exception
    {
        public PostsAlreadyReviewedException()
            :base("Cannot perform operation. There is no post to review !")
        {

        }
    }
}
