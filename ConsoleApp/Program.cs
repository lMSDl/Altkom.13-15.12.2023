﻿using Models;

//Instrukcja najwyższego poziomu
Console.WriteLine("Hello, World!");


//Metody napisane jako instrukcje najwyższego poziomu nie posiadają modyfikatorów dostępu,
//ponieważ kod w pliku z instrukcjami najwyższego poziomu zostanie otoczony metodą Main, a metody w metodzie nie mogą posiadać moedyfikatorów dostępu
void DoSth()
{
    Console.WriteLine("I am working...");
}


Person person = new Person(DateTime.Now.AddYears(-30));
person.FirstName = "Ewa";
Console.WriteLine(person.FirstName);


Person person2 = new Person("Monika", "Monikowska", DateTime.Now.AddYears(-25));
Console.WriteLine(person2.FullName);

//wykorzystanie inicjalizatora klas - w nawiasach klamrowych (po konstruktorze) wpisujemy wartości początkowe dla właściwości
Pizza pizza = new Pizza() { Cheese = true, Tomato = true };
Pizza pizza2 = new Pizza() { Ham = true, Tomato = true };
Pizza pizza3 = new Pizza() { Ham = true, Tomato = true, Sauce = true };
Pizza pizza4 = new Pizza(true) { Ham = true, Tomato = true, Sauce = true };



Nullable(null);

void Nullable(string? ala)
{
    int? a = 0;
    Nullable<int> b = null;
    int c;

    if(a - b == 0 || a - b == null)
    {
        /*if(a == null || b == null)
            c = 0;
        else*/
            c = (a + b) ?? 0; // ?? - jeśli wynik po lewej jest null to użyj wartości po prawej stronie
    }
    else
    {
        int? result = a - b;

        //if (result != null)
        //if (result is not null)
        /*if (result.HasValue)
        {
            c = result.Value;
        }
        else
        {
            c = 0;
        }*/

        c = result.HasValue ? result.Value : 0; // ? : - operator warunkowy:  (warunek) ? (tak) : (nie);
    }

    c = a.HasValue ? a.Value : b.HasValue ? b.Value : 0;


    c = (a - b == 0 || a - b == null) ? ((a + b) ?? 0) : ((a - b).HasValue ? (a - b).Value : 0);

    c = ((a - b == 0 || a - b == null) ? (a + b) : (a - b)) ?? 0;


}