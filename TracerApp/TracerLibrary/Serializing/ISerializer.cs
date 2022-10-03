using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary.Serializing
{
    interface ISerializer
    {
        void SaveTraceResult(TextWriter textWriter, TraceResult traceResult);
    }
}
