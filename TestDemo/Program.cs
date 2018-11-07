using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Threading.Tasks;
using System.IO;

namespace TestDemo
{
    class Program
    {
        #region LeetCode算法题目

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

        public static IList<string> FizzBuzz(int n)
        {
            List<string> ret = new List<string>();
            for (int i = 1; i <= n; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    ret.Add("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    ret.Add("Fizz");
                }
                else if (i % 5 == 0)
                {
                    ret.Add("Buzz");
                }
                else
                    ret.Add(i.ToString());
            }

            return ret;
        }

        public static int LongestPalindrome(string s)
        {
            var sList = s.GroupBy(x => x).Select(a => new { key = a.Key, count = a.Count(), bSingle = a.Count() % 2 != 0 }).ToList();
            int iList = sList.Select(x => new { sumCount = x.bSingle ? x.count - 1 : x.count }).Sum(a => a.sumCount);
            if (sList.Any(x => x.bSingle == true))
                return iList + 1;
            else
                return iList;

        }

        public static string ToHex(int num)
        {
            return num.ToString("X").ToLower();
        }

        public static int FindNthDigit(int n)
        {
            if (n < 9)
                return n;
            int inew = 9;
            long iMinSum = 0;
            long iMaxSum = 0;
            int i = 1;
            for (; n > iMaxSum; i++)
            {
                iMinSum = inew * Convert.ToInt64(i.ToString().PadRight(i, '0'));
                iMaxSum += iMinSum;
            }
            long mult = i - 1;
            long number = n - (iMaxSum - iMinSum);
            int than = Convert.ToInt32(number % mult);
            int thanMin = 0;
            if (than == 0)
                thanMin = 1;
            double ret = Math.Truncate(Convert.ToDouble(number - thanMin) / mult * 10) / 10 + Convert.ToInt32(1.ToString().PadRight(i - 1, '0'));

            int iret = (int)ret;
            if (than == 0)
            {
                Console.WriteLine(Convert.ToInt32(iret.ToString()[iret.ToString().Length - 1].ToString()));
                return Convert.ToInt32(iret.ToString()[iret.ToString().Length - 1].ToString());
            }
            else
            {
                Console.WriteLine(Convert.ToInt32(iret.ToString()[than - 1].ToString()));
                return Convert.ToInt32(iret.ToString()[than - 1].ToString());
            }
        }

        public static int FindNthDigis(int n)
        {
            string s = "";
            for (int i = 1; i <= n; i++)
                s += i.ToString();
            Console.WriteLine(Convert.ToInt32(s[n - 1].ToString()));
            Console.WriteLine(s.Length);
            return Convert.ToInt32(s[n - 1].ToString());
        }

        public static int GetMoneyAmount(int n)
        {
            int ret = 0;
            int retLast = 0;
            for (int i = 0; n > 1; i++)
            {
                n = n / 2;
                if (i != 0)
                    ret += (n % 2 == 0 ? 0 : 1);
                ret += n;
                retLast = retLast + ret;
            }
            return retLast;
        }


        #endregion

        #region 硬盘内文件读取
        /// <summary>
        /// 获得指定路径下所有文件名
        /// </summary>
        /// <param name="sw">文件写入流</param>
        /// <param name="path">文件写入流</param>
        /// <param name="indent">输出时的缩进量</param>
        public static void getFileName(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (FileInfo f in root.GetFiles())
            {
                Console.WriteLine(f.Name + "------" + LastWriteTime(path, f.Name));
            }
        }

        /// <summary>
        /// 获得指定路径下所有子目录名
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="indent">输出时的缩进量</param>
        public static void getDirectory(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (DirectoryInfo d in root.GetDirectories())
            {
                Console.WriteLine("文件夹：" + d.Name + "------" + LastWriteTime(path, d.Name));
                //getDirectory(d.FullName);
            }
            getFileName(path);
        }

        /// <summary>
        /// 文件最后写入时间
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string LastWriteTime(string path, string name)
        {
            FileInfo fi = new FileInfo(path + "\\" + name);
            return fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="path"></param>
        public static void Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line.ToString());
            }
            sr.Close();
            sr.Dispose();
        }


        #endregion

        static void Main(string[] args)
        {
            #region 注释

            //GetMoneyAmount(7);
            //FindNthDigis(370);
            //FindNthDigit(1000);

            #region 获取文件目录

            //获取当前程序所在的文件路径
            //string rootPath = Directory.GetCurrentDirectory();
            //string parentPath = Directory.GetParent(rootPath).FullName;//上级目录
            //string topPath = Directory.GetParent(parentPath).FullName;//上上级目录
            //getDirectory(topPath);

            #endregion
            //Read("E:\\项目\\退件代码.txt");

            //Order or = new Order();
            //or.id = 1;
            //or.name = "方法一";
            //or.passengers = new List<Passenger>();
            //for (int i = 0; i < 3; i++)
            //{
            //    Passenger pass = new Passenger();
            //    pass.birthday = DateTime.Now;
            //    pass.passengerName = i.ToString();
            //    or.passengers.Add(pass);
            //}
            //Console.WriteLine($"{or.name}==={or.passengers.Count}");

            //or = new Order();

            //or.id = 2;
            //or.name = "方法二";
            //List<Passenger> list = new List<Passenger>();
            //for (int i = 0; i < 3; i++)
            //{
            //    Passenger pass = new Passenger();
            //    pass.birthday = DateTime.Now;
            //    pass.passengerName = i.ToString();
            //    list.Add(pass);
            //}
            //or.passengers = list;
            //Console.WriteLine($"{or.name}==={or.passengers.Count}");
            //Console.ReadKey();

            #endregion


            Console.ReadKey();
        }

    }

    #region 类
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
    #endregion
}
