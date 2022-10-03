using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TracerLibrary
{
    public class MethodTracer
    {
        public string MethodName { get; private set; }
        public string ClassName { get; private set; }
        public TimeSpan time { get; private set; }
        private Stopwatch StopWatch;
        public List<MethodTracer> ListInnerMethodTracers { get; private set; }

        public MethodTracer()
        {
            StackFrame sf = new StackFrame(3);
            MethodName = sf.GetMethod().Name;
            ClassName = sf.GetMethod().DeclaringType.Name;
            time = new TimeSpan();
            StopWatch = new Stopwatch();
            ListInnerMethodTracers = new List<MethodTracer>();
        }
        public void StartTrace()
        {
            StopWatch.Start();
        }
        public void StopTrace()
        {
            StopWatch.Stop();
            time = StopWatch.Elapsed;
        }
    }
}
