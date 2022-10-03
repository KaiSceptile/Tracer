using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLibrary
{
    public class ThreadTracer
    {
        public int ID { get; private set; }
        public List<MethodTracer> ListFirstLvlMethodTracers { get; private set; }
        private Stack<MethodTracer> StackUnstoppedMethodTracers;
        public TimeSpan time { get; private set; }


        public ThreadTracer(int id)
        {
            ID = id;
            ListFirstLvlMethodTracers = new List<MethodTracer>();
            StackUnstoppedMethodTracers = new Stack<MethodTracer>();
            time = new TimeSpan();
        }

        public void StartTrace()
        {
            MethodTracer methodTracer = new MethodTracer();

            // check thread inner methods
            if (StackUnstoppedMethodTracers.Count > 0)
            {
                MethodTracer lastMethodTracer = StackUnstoppedMethodTracers.Peek();
                lastMethodTracer.ListInnerMethodTracers.Add(methodTracer);
            }
            //push this method
            StackUnstoppedMethodTracers.Push(methodTracer);
            methodTracer.StartTrace();
        }

        public void StopTrace()
        {
            MethodTracer lastMethodTracer = StackUnstoppedMethodTracers.Pop();
            lastMethodTracer.StopTrace();
            if (!StackUnstoppedMethodTracers.Any())
            {
                ListFirstLvlMethodTracers.Add(lastMethodTracer);
                time += lastMethodTracer.time;
            }
        }
    }
}
