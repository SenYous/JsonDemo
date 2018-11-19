using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Threading;

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

            //var s = new Square(10);
            //Resize(s);

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

            Program p = new Program();
            p.Print();


            Console.ReadKey();
        }
        void Print()
        {
            #region 单例模式
            //Task.Run(() =>
            //{
            //    var model = DesignPattern.Singleton.GetInstance();
            //    Console.WriteLine("任务1：" + model.GetDateTimeNow());
            //});


            //Task.Run(() =>
            //{
            //    Thread.Sleep(2000);
            //    var model = DesignPattern.Singleton.GetInstance();
            //    Console.WriteLine("任务2：" + model.GetDateTimeNow());
            //});
            #endregion

            #region 简单工厂模式
            //// 客户想点一个西红柿炒蛋        
            //DesignPattern.Food food1 = DesignPattern.FoodSimpleFactory.CreateFood("西红柿炒蛋");
            //food1.Print();

            //// 客户想点一个土豆肉丝
            //DesignPattern.Food food2 = DesignPattern.FoodSimpleFactory.CreateFood("土豆肉丝");
            //food2.Print();

            #endregion

            #region 工厂方法模式
            //// 初始化做菜的两个工厂（）
            //DesignPattern.Creator shreddedPorkWithPotatoesFactory = new DesignPattern.ShreddedPorkWithPotatoesFactory2();
            //DesignPattern.Creator tomatoScrambledEggsFactory = new DesignPattern.TomatoScrambledEggsFactory2();

            //// 开始做西红柿炒蛋
            //DesignPattern.Food2 tomatoScrambleEggs = tomatoScrambledEggsFactory.CreateFoddFactory();
            //tomatoScrambleEggs.Print();

            ////开始做土豆肉丝
            //DesignPattern.Food2 shreddedPorkWithPotatoes = shreddedPorkWithPotatoesFactory.CreateFoddFactory();
            //shreddedPorkWithPotatoes.Print();

            //Console.Read();
            #endregion

            #region 抽象工厂模式
            //// 南昌工厂制作南昌的鸭脖和鸭架
            //DesignPattern.AbstractFactory nanChangFactory = new DesignPattern.NanChangFactory();
            //DesignPattern.YaBo nanChangYabo = nanChangFactory.CreateYaBo();
            //nanChangYabo.Print();
            //DesignPattern.YaJia nanChangYajia = nanChangFactory.CreateYaJia();
            //nanChangYajia.Print();

            //// 上海工厂制作上海的鸭脖和鸭架
            //DesignPattern.AbstractFactory shangHaiFactory = new DesignPattern.ShangHaiFactory();
            //shangHaiFactory.CreateYaBo().Print();
            //shangHaiFactory.CreateYaJia().Print();

            #endregion

            #region 建造者模式
            //// 客户找到电脑城老板说要买电脑，这里要装两台电脑
            //// 创建指挥者和构造者
            //DesignPattern.Director director = new DesignPattern.Director();
            //DesignPattern.Builder b1 = new DesignPattern.ConcreteBuilder1();
            //DesignPattern.Builder b2 = new DesignPattern.ConcreteBuilder2();

            //// 老板叫员工去组装第一台电脑
            //director.Construct(b1);

            //// 组装完，组装人员搬来组装好的电脑
            //DesignPattern.Computer computer1 = b1.GetComputer();
            //computer1.Show();

            //// 老板叫员工去组装第二台电脑
            //director.Construct(b2);
            //DesignPattern.Computer computer2 = b2.GetComputer();
            //computer2.Show();

            #endregion

            #region 原型模式
            //// 孙悟空 原型
            //DesignPattern.MonkeyKingPrototype prototypeMonkeyKing = new DesignPattern.ConcretePrototype("MonkeyKing");

            //// 变一个
            //DesignPattern.MonkeyKingPrototype cloneMonkeyKing = prototypeMonkeyKing.Clone() as DesignPattern.ConcretePrototype;
            //Console.WriteLine("Cloned1:\t" + cloneMonkeyKing.Id);

            //// 变两个
            //DesignPattern.MonkeyKingPrototype cloneMonkeyKing2 = prototypeMonkeyKing.Clone() as DesignPattern.ConcretePrototype;
            //Console.WriteLine("Cloned2:\t" + cloneMonkeyKing2.Id);
            #endregion

            #region 适配器模式
            //// 现在客户端可以通过电适配要使用2个孔的插头了
            //DesignPattern.IThreeHole threehole = new DesignPattern.PowerAdapter();
            //threehole.Request();
            #endregion

            #region 桥接模式

            #endregion
        }

        public static void Resize(Quadrangle r)
        {
            while (r.Height >= r.Width)
            {
                r.Width += 1;
            }
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


    #region 依赖注入
    // 四边形
    public abstract class Quadrangle
    {
        public virtual long Width { get; set; }
        public virtual long Height { get; set; }
    }
    // 矩形
    public class Rectangle : Quadrangle
    {
        public override long Height { get; set; }

        public override long Width { get; set; }

    }
    // 正方形
    public class Square : Quadrangle
    {
        public long _side;

        public Square(long side)
        {
            _side = side;
        }
    }

    #endregion
}
