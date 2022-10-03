using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary
{
    public class ResultMethodTrace
    {
        public string MethodName { get; private set; }
        public string ClassName { get; private set; }
        public TimeSpan time { get; private set; }
        public List<ResultMethodTrace> ListInnerMethodTraceResults { get; private set; }

        public static ResultMethodTrace GetTraceResult(MethodTracer methodTracer)
        {
            ResultMethodTrace methodTracerResult = new ResultMethodTrace();
            methodTracerResult.ListInnerMethodTraceResults = new List<ResultMethodTrace>();
            methodTracerResult.MethodName = methodTracer.MethodName;
            methodTracerResult.ClassName = methodTracer.ClassName;
            methodTracerResult.time = methodTracer.time;

            foreach (var innerMethodTracer in methodTracer.ListInnerMethodTracers)
            {
                methodTracerResult.ListInnerMethodTraceResults.Add(ResultMethodTrace.GetTraceResult(innerMethodTracer));
            }

            return methodTracerResult;
        }
    }
}
