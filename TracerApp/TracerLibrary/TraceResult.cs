using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary
{
    public class TraceResult
    {
        public IDictionary<int, ThreadTraceResult> dThreadTracerResults { get; private set; }

        public TraceResult(ConcurrentDictionary<int, ThreadTracer> cdThreadTracers)
        {
            dThreadTracerResults = new Dictionary<int, ThreadTraceResult>();
            foreach (var threadTracer in cdThreadTracers)
            {
                dThreadTracerResults[threadTracer.Key] = ThreadTraceResult.GetTraceResult(threadTracer.Value);
            }
        }
    }
}
