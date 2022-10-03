using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary
{
    public class ResultThreadTrace
    {
        public IList<ResultMethodTrace> MethodTracersResult { get; private set; }
        public int ID { get; private set; }
        public TimeSpan time { get; private set; }

        public static ResultThreadTrace GetTraceResult(ThreadTracer threadTracer)
        {
            ResultThreadTrace threadTracerResult = new ResultThreadTrace();
            threadTracerResult.MethodTracersResult = new List<ResultMethodTrace>();
            threadTracerResult.ID = threadTracer.ID;
            threadTracerResult.time = threadTracer.time;

            foreach (var firstLvlMethodTracer in threadTracer.ListFirstLvlMethodTracers)
            {
                threadTracerResult.MethodTracersResult.Add(ResultMethodTrace.GetTraceResult(firstLvlMethodTracer));
            }

            return threadTracerResult;
        }
    }
}


        
