using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace TracerLibrary
{
    public class Tracer : ITracer
    {
        TraceResult tracerResult { get; set; }
        private ConcurrentDictionary<int, ThreadTracer> cdThreadTracers;
        static private object locker = new object();

        public Tracer()
        {
            cdThreadTracers = new ConcurrentDictionary<int, ThreadTracer>();
        }
        public void StartTrace()
        {
            ThreadTracer curThreadTracer = AddOrGetThreadTracer(Thread.CurrentThread.ManagedThreadId);
            curThreadTracer.StartTrace();
        }

        public void StopTrace()
        {
            ThreadTracer currThreadTracer = AddOrGetThreadTracer(Thread.CurrentThread.ManagedThreadId);
            currThreadTracer.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            tracerResult = new TraceResult(cdThreadTracers);
            return tracerResult;
        }

        private ThreadTracer AddOrGetThreadTracer(int id)
        {
            // synchronization
            lock (locker)
            {
                // check if exists
                if (!cdThreadTracers.TryGetValue(id, out ThreadTracer threadTracer))
                {
                    threadTracer = new ThreadTracer(id);
                    cdThreadTracers[id] = threadTracer;
                }
                return threadTracer;
            }
        }

    }

}
