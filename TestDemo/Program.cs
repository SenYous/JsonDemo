using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Threading.Tasks;

namespace TestDemo
{
    class Program
    {
        public char FindTheDifference(string s, string t)
        {
            if (t == "")
                return new char();
            var item = s.GroupBy(x => x).Select(q => new { key = q.Key, keyCnt = q.Count() });
            var item2 = t.GroupBy(x => x).Select(q => new { key = q.Key, keyCnt = q.Count() });

            var apk = (from a in item2
                       join d in item on a.key equals d.key into temp
                       from tt in temp.DefaultIfEmpty()
                       where a.keyCnt > (tt == null ? 0 : tt.keyCnt)
                       select new
                       {
                           a.key
                       }).ToList();

            if (apk == null)
                return new char();
            else
                return apk[0].key;
        }

        public static int ThirdMax(int[] nums)
        {
            var numsAr = nums.GroupBy(x => x).OrderByDescending(a => a.Key).ToList();
            if (numsAr.Count() >= 3)
                return numsAr[2].Key;
            else
                return numsAr[0].Key;
        }



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
