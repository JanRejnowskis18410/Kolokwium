using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MyProject.Exceptions
{
    public class SqlServerException : Exception
    {
        public SqlServerException()
        {
        }

        public SqlServerException(string message) : base(message)
        {
        }

        public SqlServerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SqlServerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
