using System;

namespace core.Common
{
  public class ApiOperationException : Exception
  {
     public ApiOperationException()
     {
     }

     public ApiOperationException(string message) : base(message)
     {
     }

     public ApiOperationException(string message, Exception inner) : base(message, inner)
     {
     }
  }
}
