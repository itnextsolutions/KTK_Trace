using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KotakTraceAPI.CustomFilterRepo
{
    /// <summary>
    /// The base Exception Class
    /// </summary>
    public class ProcessException : Exception
    {
        public ProcessException(string message)
            : base(message)
        {

        }
    }
}