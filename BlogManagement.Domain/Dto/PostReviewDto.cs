using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Domain.Dto
{
    public class PostReviewDto
    {
        public long UserId { get; set; }

        public long[] PostIds { get; set; }
    }
}
