using System;

namespace Model
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
    
}