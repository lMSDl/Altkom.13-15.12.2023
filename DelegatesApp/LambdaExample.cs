using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesApp
{
    internal class LambdaExample
    {
        Func<int, int, int> Calculator { get; set; }
        Func<string> SomeFunc { get; set; }
        Action<int> SomeAction { get; set; }
        Action AnotherAction { get; set; }


        public void Test()
        {
            Calculator += Plus;

                        //<prametry> <operator> <ciało>


            Calculator += //delegate (int a, int b) { return a - b; };
                          //(int a, int b) => { return a - b; };
                          //(a, b) => { return a - b; };
                            (a, b) => a - b;

            SomeFunc += //delegate { return "Hello!"; };
                        () => "Hello!";

            SomeAction += //delegate (int a) { Console.WriteLine(a); };
                            //(a) => Console.WriteLine(a);
                         a => Console.WriteLine(a);

            AnotherAction += //delegate { Console.WriteLine(); };
                              () => Console.WriteLine();


            SomeMethod(x => Console.WriteLine(x.ToUpper()), "Hello!");
            SomeMethod(x => Console.WriteLine(x.ToLower()), "Hello!");
        }

        private int Plus(int x, int y)
        { return x + y; }



        void SomeMethod(Action<string> stringAction, string value)
        {
            stringAction?.Invoke(value);
        }
    }
}
