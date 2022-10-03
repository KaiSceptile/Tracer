using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TracerLibrary.Serializing
{
    public class JSONSerializer : ISerializer
    {
        public void SaveTraceResult(TextWriter textWriter, TraceResult traceResult)
        {
            var jtokens = from threadTracerResult in traceResult.dThreadTracerResults.Values
                          select SaveThreads(threadTracerResult);
            JObject traceResultJSON = new JObject
            {
                { "thread", new JArray(jtokens) }
            };

            using (var jsonWriter = new JsonTextWriter(textWriter))
            {
                jsonWriter.Formatting = Formatting.Indented;
                traceResultJSON.WriteTo(jsonWriter);
            }
        }

        private JToken SaveThreads(ResultThreadTrace threadTracerResult)
        {
            var lFirstLvlMethods = from methodTracerResult in threadTracerResult.MethodTracersResult
                                   select SaveMethods(methodTracerResult);
            return new JObject
            {
                { "id", threadTracerResult.ID },
                { "time", threadTracerResult.time.Milliseconds + "ms"},
                { "methods", new JArray(lFirstLvlMethods) }
            };
        }

        private JToken SaveMethods(ResultMethodTrace methodTracerResult)
        {
            var savedTracedMetod = new JObject
            {
                { "name", methodTracerResult.MethodName },
                { "class", methodTracerResult.ClassName },
                { "time", methodTracerResult.time.Milliseconds + "ms" }
            };

            if (methodTracerResult.ListInnerMethodTraceResults.Any())
                savedTracedMetod.Add("methods", new JArray(from mt in methodTracerResult.ListInnerMethodTraceResults
                                                           select SaveMethods(mt)));
            return savedTracedMetod;
        }
    }
}
