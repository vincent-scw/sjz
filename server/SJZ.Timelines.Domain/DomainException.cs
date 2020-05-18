using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.Timelines.Domain
{
    public class DomainException : Exception
    {
        public string ErrorCode { get; set; }
        public DomainException(string errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
