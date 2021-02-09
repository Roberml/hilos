using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tortillas
{
    class Program
    {

        public static bool cocinando = true;
        public static int NPotatoes = 0;
        public static int NOnions = 0;
        public static int NTortilla = 0;
        public static readonly object l = new object();


        static void Main(string[] args)
        {
            Thread tPotatoe = new Thread(() => Ingredient("potatoe", ref NPotatoes));
            Thread tOnion = new Thread(() => Ingredient("onion", ref NOnions));
            Thread tTortilla = new Thread(Omelette);


            tPotatoe.Start();
            tOnion.Start();
            tTortilla.Start();


            Console.ReadLine();



        }



        static void Omelette()
        {
            while (cocinando)
            {
                lock (l)
                {
                    if (NPotatoes < 5 || NOnions < 5)
                    {
                        Console.WriteLine("waiting...");
                        Monitor.Wait(l);
                    }
                    else
                    {
                        NOnions -= 5;
                        NPotatoes -= 5;
                        NTortilla++;


                        Console.WriteLine("{0,3} {1,3} {2,3}", NTortilla, NPotatoes, NOnions);


                    }

                    if (NTortilla >= 10)
                    {
                        cocinando = false;
                        Console.WriteLine("se acabó");

                    }

                }
            }





        }


        static void Ingredient(string name, ref int ing)
        {
            while (cocinando)
            {
                lock (l)
                {
                    if (cocinando)
                    {
                        ing++;
                        Console.WriteLine(name + ": " + ing);
                        if (ing >= 5)
                        {
                            Monitor.Pulse(l);
                        }

                    }

                }


            }

        }
    }
}
