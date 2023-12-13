//Instrukcja najwyższego poziomu
Console.WriteLine("Hello, World!");


//Metody napisane jako instrukcje najwyższego poziomu nie posiadają modyfikatorów dostępu,
//ponieważ kod w pliku z instrukcjami najwyższego poziomu zostanie otoczony metodą Main, a metody w metodzie nie mogą posiadać moedyfikatorów dostępu
void DoSth()
{
    Console.WriteLine("I am working...");
}