using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(long userId) 
            : base($"Cannot perform operation. User with userId : {userId} does not exist !")
        {

        }
    }
}
