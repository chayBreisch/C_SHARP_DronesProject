using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome9080();
            Welcome0477();
            Console.ReadKey();
        }
        static partial void Welcome0477();
        private static void Welcome9080()
        {
            Console.Write("Enter your name: ");
            string Name = Console.ReadLine();
            Console.Write("{0} welcome to my first console application", Name);
        }
    }
}
