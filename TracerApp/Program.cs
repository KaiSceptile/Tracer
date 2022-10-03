using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TracerLibrary;
using TracerLibrary.Serializing;

namespace TracerApp
{
    class Program
    {
        public Tracer Tracer { get; set; }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Perform();
        }

        public void Perform()
        {
            this.Tracer = new Tracer();

            TestMethod_1();
            

            SaveToXml();
            SaveToJson();

            Console.ReadLine();
        }

        public void TestMethod_1()
        {
            this.Tracer.StartTrace();
            TestMethod_1_1();
            TestMethod_1_2();
            TestMethod_1_3();
            this.Tracer.StopTrace();
        }

        public void TestMethod_1_1()
        {
            this.Tracer.StartTrace();
            Thread.Sleep(100);
            this.Tracer.StopTrace();
        }

        public void TestMethod_1_2()
        {
            this.Tracer.StartTrace();
            Thread.Sleep(100);
            this.Tracer.StopTrace();
        }

        public void TestMethod_1_3()
        {
            this.Tracer.StartTrace();
            Thread.Sleep(100);
            this.Tracer.StopTrace();
        }

        

        public void SaveToXml()
        {
            string pathToSave = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\TestResults\\XmlTraсeResult.xml");

            FileStream fileStream = new FileStream(pathToSave, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            TraceResult tracerResult = this.Tracer.GetTraceResult();

            xmlSerializer XmlSerializer = new xmlSerializer();
            XmlSerializer.SaveTraceResult(streamWriter, tracerResult);
            XmlSerializer.SaveTraceResult(Console.Out, tracerResult);
            Console.WriteLine();
            Console.WriteLine();
        }

        public void SaveToJson()
        {
            string pathToSave = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\TestResults\\JsonTraсeResult.json");

            FileStream fileStream = new FileStream(pathToSave, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            TraceResult tracerResult = this.Tracer.GetTraceResult();

            JSONSerializer jsonSerializer = new JSONSerializer();
            jsonSerializer.SaveTraceResult(streamWriter, tracerResult);
            jsonSerializer.SaveTraceResult(Console.Out, tracerResult);
        }
    }
}
