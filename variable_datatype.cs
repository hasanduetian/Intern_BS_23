using System;
namespace hasan
{
    class Program
    {
        public static MatchCasing()
        {
            int num = 11;
            double bigName = num;
            Console.WriteLine(bigName);

            // explicit conversion -> possible data loss
            double c = 10.13;
            int d = (int)c;   // casting
            Console.WriteLine(d);

            // convert string
            string tmp="123";
            int res=Convert.ToInt32(tmp);
            Console.WriteLine(res);
            Console.WriteLine(res.GetType());

            float k=10.54774;// high precision
            Console.WriteLine(k);

            // decimal datatype
            decimal a=10.54774;// high precision
            Console.WriteLine(a);
        }
    }
}  
  
 