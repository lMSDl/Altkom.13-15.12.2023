using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesApp
{
    internal class BuildInDelegatesExample
    {


        public void Add(int a, int b)
        {
            int result = a + b;
            Console.WriteLine(result);
        }

        public bool Substract(int a, int b)
        {
            int result = a - b;
            Console.WriteLine(result);
            return result % 2 != 0;
        }

        public void Test()
        {
            Method(Add, Substract);
        }

        /*public delegate void Method1Delegate(int a, int b);
        public delegate bool Method2Delegate(int a, int b);
        void Method(Method1Delegate method1, Method2Delegate method2)*/
        void Method(Action<int, int> method1, Func<int, int, bool> method2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    method1(i, j);
                    if(method2(i, j))
                        Console.WriteLine("@@@");
                }
            }
        }




    }
}
