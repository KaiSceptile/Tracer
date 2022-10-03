using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLibrary;

namespace TestTracerApp
{
    [TestClass]
    public class TestTracer
    {
        public Tracer tracer = new Tracer();

        void Method1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        void Method2()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        void MethodWithInnerMethodCall()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Method1();
            tracer.StopTrace();
        }

        void TwoThreadsCallOneMethod()
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 2; i++)
            {
                Thread thread = new Thread(Method1);
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        [TestMethod]
        public void ShouldTrace_SingleThread_SingleMethodOnTheFirstLvl()
        {
            Method1();

            TraceResult tracerResult = tracer.GetTraceResult();

            ResultThreadTrace[] threadTracersResults = new ResultThreadTrace[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(1, threadTracersResults.Length); // should be only one ThreadTracerResult
            Assert.AreEqual(1, threadTracersResults[0].MethodTracersResult.Count); // should be only one method on the first lvl

            // check that the first method on the first lvl is correct
            string methodNameFromTraceResult = threadTracersResults[0].MethodTracersResult[0].MethodName;
            string classNameFromTraceResult = threadTracersResults[0].MethodTracersResult[0].ClassName;
            Assert.AreEqual("Method1", methodNameFromTraceResult);
            Assert.AreEqual("TestTracer", classNameFromTraceResult);
        }


        [TestMethod]
        public void ShouldTrace_SingleThread_TwoMethodsOnTheFirstLvl()
        {
            Method1();
            Method2();

            TraceResult tracerResult = tracer.GetTraceResult();

            ResultThreadTrace[] threadTracersResults = new ResultThreadTrace[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(1, threadTracersResults.Length); // should be only one ThreadTracerResult
            Assert.AreEqual(2, threadTracersResults[0].MethodTracersResult.Count); // should be two methods on the first lvl

            // check that the first method on the first lvl is correct
            string methodNameFromTraceResult1 = threadTracersResults[0].MethodTracersResult[0].MethodName;
            string classNameFromTraceResult1 = threadTracersResults[0].MethodTracersResult[0].ClassName;
            Assert.AreEqual("Method1", methodNameFromTraceResult1);
            Assert.AreEqual("TestTracer", classNameFromTraceResult1);

            // check that the second method on the first lvl is correct
            string methodNameFromTraceResult2 = threadTracersResults[0].MethodTracersResult[1].MethodName;
            string classNameFromTraceResult2 = threadTracersResults[0].MethodTracersResult[1].ClassName;
            Assert.AreEqual("Method2", methodNameFromTraceResult2);
            Assert.AreEqual("TestTracer", classNameFromTraceResult2);
        }

        [TestMethod]
        public void ShouldTrace_SingleThread_SingleMethodOnTheFirstLvl_with_InnerMethodCall()
        {
            MethodWithInnerMethodCall();

            TraceResult tracerResult = tracer.GetTraceResult();

            ResultThreadTrace[] threadTracersResults = new ResultThreadTrace[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(1, threadTracersResults.Length); // should be only one ThreadTracerResult
            Assert.AreEqual(1, threadTracersResults[0].MethodTracersResult.Count); // should be only one method on the first lvl

            // check that the first method on the first lvl is correct
            string methodNameFromTraceResult1 = threadTracersResults[0].MethodTracersResult[0].MethodName;
            string classNameFromTraceResult1 = threadTracersResults[0].MethodTracersResult[0].ClassName;
            Assert.AreEqual("MethodWithInnerMethodCall", methodNameFromTraceResult1);
            Assert.AreEqual("TestTracer", classNameFromTraceResult1);

            // check that the first method has one inner method
            Assert.AreEqual(1, threadTracersResults[0].MethodTracersResult[0].ListInnerMethodTraceResults.Count);

            // check that the first method has correct inner method
            Assert.AreEqual("Method1", threadTracersResults[0].MethodTracersResult[0].ListInnerMethodTraceResults[0].MethodName);
            Assert.AreEqual("TestTracer", threadTracersResults[0].MethodTracersResult[0].ListInnerMethodTraceResults[0].ClassName);
        }

        [TestMethod]
        public void ShouldTrace_TwoThreads_each_with_SingleMethod()
        {
            TwoThreadsCallOneMethod();

            TraceResult tracerResult = tracer.GetTraceResult();

            ResultThreadTrace[] threadTracersResults = new ResultThreadTrace[tracer.GetTraceResult().dThreadTracerResults.Count];
            tracer.GetTraceResult().dThreadTracerResults.Values.CopyTo(threadTracersResults, 0);

            Assert.AreEqual(2, threadTracersResults.Length); // should be two ThreadTracerResult
            Assert.AreEqual(1, threadTracersResults[0].MethodTracersResult.Count); // should be only one method on the first lvl in the first ThreadTracerResult
            Assert.AreEqual(1, threadTracersResults[1].MethodTracersResult.Count); // should be only one method on the first lvl in the second ThreadTracerResult

            // check that the first ThreadTraceResult has correct method on the first lvl
            string methodNameFromTraceResult1 = threadTracersResults[0].MethodTracersResult[0].MethodName;
            string classNameFromTraceResult1 = threadTracersResults[0].MethodTracersResult[0].ClassName;
            Assert.AreEqual("Method1", methodNameFromTraceResult1);
            Assert.AreEqual("TestTracer", classNameFromTraceResult1);

            // check that the second ThreadTraceResult has correct method on the first lvl
            string methodNameFromTraceResult2 = threadTracersResults[1].MethodTracersResult[0].MethodName;
            string classNameFromTraceResult2 = threadTracersResults[1].MethodTracersResult[0].ClassName;
            Assert.AreEqual("Method1", methodNameFromTraceResult1);
            Assert.AreEqual("TestTracer", classNameFromTraceResult1);
        }
    }
}
