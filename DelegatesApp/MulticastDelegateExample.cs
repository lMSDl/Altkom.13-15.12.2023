using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesApp
{
    internal class MulticastDelegateExample
    {

        public delegate void MulticastDelegate(string input);

        public void Message1(string message)
        {
            Console.WriteLine("1st message: " + message);
        }
        public void Message2(string message)
        {
            Console.WriteLine("2st message: " + message);
        }
        public void Message3(string message)
        {
            Console.WriteLine("3st message: " + message);
        }


        public void Test()
        {
            MulticastDelegate multicastDelegate = null;

            multicastDelegate += Message1;
            multicastDelegate += Message2;
            multicastDelegate += Message3;
            multicastDelegate += Console.WriteLine;

            multicastDelegate("Hi!");

            multicastDelegate -= Message2;

            multicastDelegate("Bye!");

            multicastDelegate = Message3;
            multicastDelegate("message3");
        }
    }
}
