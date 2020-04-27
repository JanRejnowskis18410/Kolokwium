using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MyProject.Exceptions
{
    public class MedicamentNotInDatabaseExecption : Exception
    {
        public MedicamentNotInDatabaseExecption()
        {
        }

        public MedicamentNotInDatabaseExecption(string message) : base(message)
        {
        }

        public MedicamentNotInDatabaseExecption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MedicamentNotInDatabaseExecption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
