using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TracerLibrary.Serializing
{
    public class xmlSerializer : ISerializer
    {
        public void SaveTraceResult(TextWriter textWriter, TraceResult traceResult)
        {
            XDocument doc = new XDocument(
                new XElement("root", from threadTracerResult in traceResult.dThreadTracerResults.Values
                                     select SaveThread(threadTracerResult)
                ));

            using (XmlTextWriter xmlWriter = new XmlTextWriter(textWriter))
            {
                xmlWriter.Formatting = Formatting.Indented;
                doc.WriteTo(xmlWriter);
            }
        }

        private XElement SaveThread(ResultThreadTrace threadTracer)
        {
            return new XElement("thread",
                new XAttribute("id", threadTracer.ID),
                new XAttribute("time", threadTracer.time.Milliseconds + "ms"),
                from methodTracerResult in threadTracer.MethodTracersResult
                select SaveMethod(methodTracerResult)
                );
        }

        private XElement SaveMethod(ResultMethodTrace methodTracer)
        {
            XElement savedTracedMetod = new XElement("method",
                new XAttribute("name", methodTracer.MethodName),
                new XAttribute("time", methodTracer.time.Milliseconds + "ms"),
                new XAttribute("class", methodTracer.ClassName));

            if (methodTracer.ListInnerMethodTraceResults.Any())
                savedTracedMetod.Add(from innerMethod in methodTracer.ListInnerMethodTraceResults
                                     select SaveMethod(innerMethod));
            return savedTracedMetod;
        }
    }
}
