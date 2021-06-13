using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Domain.Exceptions
{
    public class OperationDeniedException : Exception
    {
        public OperationDeniedException(long userId) 
            : base($"Cannot perform operation. User with userId : {userId} is not an administrator and he is not allowed to review the posts !")
        {

        }
    }
}
