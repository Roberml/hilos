using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejercicio5Timer
{

    delegate void MyDelegate();
    class Program
    {
        static int counter = 0;
        static void increment()
        {
            counter++;
            Console.WriteLine(counter);
        }
        static void Main(string[] args)
        {
            MyTimer t = new MyTimer(increment);
            t.interval = 1000;
            string op = "";
            do
            {
                Console.WriteLine("Press any key to start.");
                Console.ReadKey();
                t.run();
                Console.WriteLine("Press any key to pause.");
                Console.ReadKey();
                t.pause();
                Console.WriteLine("Press 1 to restart or Enter to end.");
                op = Console.ReadLine();
            } while (op == "1");
        }
    }


    class MyTimer
    {
        public int interval;
        public MyDelegate del;
        private readonly object l = new object();
        public bool running = true;

        public MyTimer(MyDelegate del)
        {
            this.del = del;
            Thread thread = new Thread(run);
            thread.Start();
            Monitor.Wait(l);
            
        }



        public void run()
        {

            while (running)
            {
                lock (l)
                {
                    Monitor.Pulse(l);
                }

                Thread.Sleep(interval);

            }

           
           

        }



        public void pause()
        {
            lock (l)
            {
                running = false;

            }

        }


    }
}
