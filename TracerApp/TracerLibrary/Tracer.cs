using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }

}
