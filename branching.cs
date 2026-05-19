using System;
namespace hasan
{
    class Program
    {
        public static Main()
        {
            int manAge = 20;

            if (manAge < 13){
            Console.WriteLine("Child");
            }
            else if (manAge < 20){
            Console.WriteLine("Teenager");
            }
            else{
            Console.WriteLine("Adult");
            }

            // swithc
            int age =20;
            switch (age)
            {
            case 20:
                Console.WriteLine("hasan");
                break;                
            case 15:
                Console.WriteLine("imran");
                break;                
            case 10:
                Console.WriteLine("rrrr");
                break;                
            case 7:
                Console.WriteLine("rusha");
                break;
            default:
                Console.WriteLine("undefine");
                break;
            }

        }
    }
}  
  
 