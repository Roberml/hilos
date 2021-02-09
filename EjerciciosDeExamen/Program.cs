using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjerciciosDeExamen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

        class Program
        {


            public static bool running = true;
            public static bool dormirse = false;
            public static int PTortuga = 0;
            public static int PLiebre = 0;
            public static Random rand = new Random();
            public static readonly object l = new object();



            static void Main(string[] args)
            {
                Thread tLiebre = new Thread(correLiebre);
                Thread tTortuga = new Thread(correTortuga);
                tLiebre.Start();
                tTortuga.Start();

                tLiebre.Join();


                Console.ReadLine();



            }



            static void correLiebre()
            {
                while (running)
                {
                    lock (l)
                    {
                        if (running)
                        {
                            PLiebre += 6;

                            if (PLiebre >= 25)
                            {


                                Console.WriteLine("la liebre ha ganado");
                                running = false;

                            }
                            else
                            {
                                Console.WriteLine("la liebre ha dado: " + PLiebre + " pasos");

                                if (rand.Next(101) <= 60)
                                {
                                    dormirse = true;
                                    Console.WriteLine("se durmió");
                                    Monitor.Wait(l);


                                    Thread tDormir = new Thread(dormir);
                                    tDormir.Start();

                                }
                            }

                        }

                    }
                    Thread.Sleep(300);

                }

            }

            static void dormir()
            {
                while (dormirse)
                {
                    Thread.Sleep(2500);
                    lock (l)
                    {
                        if (running)
                        {
                            Console.WriteLine("la liebre se despertó");
                            Monitor.Pulse(l);

                        }


                    }

                }
            }


            static void correTortuga()
            {
                while (running)
                {
                    lock (l)
                    {
                        if (running)
                        {
                            PTortuga += 1;

                            if (PTortuga == 25)
                            {


                                Console.WriteLine("la tortuga ha ganado");
                                running = false;

                            }
                            else
                            {
                                Console.WriteLine("la tortuga ha dado: " + PTortuga + " pasos");




                                if (PTortuga == PLiebre)
                                {
                                    if (rand.Next(101) >= 50)
                                    {
                                        Console.WriteLine("La tortuga hizo ruido");
                                        Monitor.Pulse(l);
                                    }

                                }





                            }



                        }

                    }
                    Thread.Sleep(200);

                }



            }
        }
    }

