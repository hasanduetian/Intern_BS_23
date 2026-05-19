using System;
namespace hasan{
class Program
{
    // Function
    static int Add(int a, int b)
    {
        return a + b;
    }

    static void Main()
    {
        int result = Add(5, 3);

        Console.WriteLine("Sum = " + result);
    }
}

}