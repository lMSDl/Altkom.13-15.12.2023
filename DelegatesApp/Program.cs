﻿
using DelegatesApp;

DelegatesExample example = new DelegatesExample();
example.Test();



example.Check(LargerThan, 4, 7);
example.Check(SmallerThan, 4, 7);

bool LargerThan(int a, int b)
{
    return a > b;
}
bool SmallerThan(int a, int b)
{
    return a < b;
}


MulticastDelegateExample multicastExample = new MulticastDelegateExample();
multicastExample.Test();



BuildInDelegatesExample buildInExample = new BuildInDelegatesExample();
buildInExample.Test();


LinqExample linqExample= new LinqExample();
linqExample.Test();