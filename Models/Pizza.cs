using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //Klasa o potencjalnie robudowanych konstruktorach (np. teleskopowych)
    //Zamiast tworzyć wiele konstruktorów możemy skorzystać ze wzorca budowniczego lub (w przypadku c#) z inicjalizatora klas
    public class Pizza : Entity
    {
        public Pizza() { }

        public Pizza(bool cheese) : this(cheese, false, false, false, false)
        {
        }
        public Pizza(bool cheese, bool ham) : this(cheese, ham, false, false, false)
        {
        }
        /*public Pizza(bool cheese, bool sauce)
        {
        }*/
        public Pizza(bool cheese, bool ham, bool sauce, bool onion, bool tomato)
        {
            Cheese = cheese;
            Ham = ham;
            Sauce = sauce;
            Onion = onion;
            Tomato = tomato;
        }

        public bool Cheese { get; set; }
        public bool Ham { get; set; }
        public bool Sauce { get; set; }
        public bool Onion { get; set; }
        public bool Tomato { get; set; }


    }
}
