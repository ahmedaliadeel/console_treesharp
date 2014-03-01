using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSharp;

namespace ConsoleApplication2
{
    class Program
    {
        static Sequence _ps;
        static TreeSharp.Parallel _pseq;

        static bool test1 = true;
        static bool test2 = true;



        static void Main(string[] args)
        {

            _pseq = new TreeSharp.Parallel(
              new Decorator(canRun => test1,
                   new TreeSharp.Action(delegate { Console.WriteLine("Action0"); })),
               new Decorator(canRun => test1,
                   new TreeSharp.Action(delegate { Console.WriteLine("Action1"); return RunStatus.Running; })),
               new Decorator(canRun => test2,
                   new TreeSharp.Action(delegate { Console.WriteLine("Action2"); }))
               );

            _ps = new Sequence(_pseq);

            object context = new object();

            _ps.Start(context);

            while (true && Console.NumberLock != false)
            {
             //   Console.WriteLine();
                var input = Console.ReadLine();

                if (input == "1")
                {
                    test1 = !test1;
                }
                if (input == "2")
                {
                    test2 = !test2;
                }

                //Pulse the tree
                //_ps.Tick(context);


                _ps.Tick(context);
                // If the last status wasn't running, stop the tree, and restart it.
                if (_ps.LastStatus == RunStatus.Running )
                {
                    _ps.Stop(context);
                    _ps.Start(context);
                }
            }

        }
    }
}
