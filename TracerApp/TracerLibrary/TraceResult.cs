using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace TracerLibrary
{
    public class TraceResult
    {
        public IDictionary<int, ResultThreadTrace> dThreadTracerResults { get; private set; }

        public TraceResult(ConcurrentDictionary<int, ThreadTracer> cdThreadTracers)
        {
            dThreadTracerResults = new Dictionary<int, ResultThreadTrace>();
            foreach (var threadTracer in cdThreadTracers)
            {
                dThreadTracerResults[threadTracer.Key] = ResultThreadTrace.GetTraceResult(threadTracer.Value);
            }
        }
    }
}
