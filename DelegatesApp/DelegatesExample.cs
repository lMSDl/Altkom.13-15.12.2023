using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesApp
{
    internal class DelegatesExample
    {
        public delegate void VoidWithoutParameters();

        public delegate void VoidWithParameter(string someString);
        public delegate bool BoolWithTwoIntParameter(int x, int y);


        public void Func1()
        {
            Console.WriteLine("1");
        }

        public void Func2(string @string)
        {
            Console.WriteLine(@string);
        }

        public bool Func3(int a, int b)
        {
            Console.WriteLine($"a = {a}, b = {b}");
            return a == b;
        }

        public BoolWithTwoIntParameter Delegate3 {get; set;}

        public void Test()
        {
            VoidWithoutParameters delegate1 = new VoidWithoutParameters(Func1);
            Run(delegate1);
            delegate1.Invoke();


            VoidWithParameter delegate2 = null;
            delegate2?.Invoke("Hello!"); // ? - wywołaj funkcję po znaku zapytania, jęsli obiekt jest różny od null

            delegate2 = Func2;
            delegate2.Invoke("Hello!");

            Delegate3?.Invoke(3, 5);

            Delegate3 = Func3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bool result = Delegate3.Invoke(i, j);
                    if(result)
                        Console.WriteLine("==");
                }
            }
        }

        public void Run(VoidWithoutParameters method)
        {
            method.Invoke();
        }


        public void Check(BoolWithTwoIntParameter func, int a, int b)
        {
            a = a + b;
            b = a - b;
            Console.WriteLine(func.Invoke(a, b));
        }


    }
}
