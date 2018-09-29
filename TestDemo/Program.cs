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

        static void Main(string[] args)
        {
            //测试控制器
            //string str = "测试控制器2.0";
            //Console.WriteLine(str);
            //Console.ReadKey();
            Mongo();

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

        public static void Mongo()
        {
            //建立连接
            var client = new MongoClient();
            //建立数据库
            var database = client.GetDatabase("TestDb");
            //建立collection
            var collection = database.GetCollection<BsonDocument>("foo");

            var document = new BsonDocument
            {
                {"name","MongoDB"},
                {"type","Database"},
                {"count",1},
                {"info",new BsonDocument{{"x",203},{"y",102}}}
            };
            //插入数据
            collection.InsertOne(document);

            var count = collection.Count(document);
            Console.WriteLine(count);

            //查询数据
            var document1 = collection.Find(document);
            Console.WriteLine(document1.ToString());

            //更新数据
            var filter = Builders<BsonDocument>.Filter.Eq("name", "MongoDB");
            var update = Builders<BsonDocument>.Update.Set("name", "Ghazi");

            collection.UpdateMany(filter, update);

            //删除数据
            var filter1 = Builders<BsonDocument>.Filter.Eq("count", 101);

            collection.DeleteMany(filter1);

            BsonDocument document2 = new BsonDocument();
            document2.Add("name", "MongoDB");
            document2.Add("type", "Database");
            document2.Add("count", "1");

            collection.InsertOne(document2);
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
