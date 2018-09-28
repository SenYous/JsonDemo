using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试控制器
            //string str = "测试控制器2.0";
            //Console.WriteLine(str);
            //Console.ReadKey();


            Order or = new Order();
            or.id = 1;
            or.name = "方法一";
            or.passengers = new List<Passenger>();
            for (int i = 0; i < 3; i++)
            {
                Passenger pass = new Passenger();
                pass.birthday = DateTime.Now;
                pass.passengerName = i.ToString();
                or.passengers.Add(pass);
            }
            Console.WriteLine($"{or.name}==={or.passengers.Count}");

            or = new Order();

            or.id = 2;
            or.name = "方法二";
            List<Passenger> list = new List<Passenger>();
            for (int i = 0; i < 3; i++)
            {
                Passenger pass = new Passenger();
                pass.birthday = DateTime.Now;
                pass.passengerName = i.ToString();
                list.Add(pass);
            }
            or.passengers = list;
            Console.WriteLine($"{or.name}==={or.passengers.Count}");
            Console.ReadKey();
        }

    }


    public class Order
    {
        public long id { get; set; }
        public string name { get; set; }
        public List<Passenger> passengers { get; set; }
    }

    public class Passenger
    {
        public DateTime birthday { get; set; }
        public string passengerName { get; set; }
    }
}
