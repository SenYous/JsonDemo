using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    class DesignPattern
    {
        #region 单例模式
        /// <summary>
        /// 单例模式的实现
        /// </summary>
        public class Singleton
        {
            // 定义一个静态变量来保存类的实例
            private static Singleton uniqueInstance;

            // 定义一个标识确保线程同步
            private static readonly object locker = new object();

            // 定义私有构造函数，使外界不能创建该类实例
            private Singleton()
            {
            }

            /// <summary>
            /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
            /// </summary>
            /// <returns></returns>
            public static Singleton GetInstance()
            {
                // 当第一个线程运行到这里时，此时会对locker对象 "加锁"，
                // 当第二个线程运行该方法时，首先检测到locker对象为"加锁"状态，该线程就会挂起等待第一个线程解锁
                // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
                // 双重锁定只需要一句判断就可以了
                if (uniqueInstance == null)
                {
                    lock (locker)
                    {
                        // 如果类的实例不存在则创建，否则直接返回
                        if (uniqueInstance == null)
                        {
                            uniqueInstance = new Singleton();
                        }
                    }
                }
                return uniqueInstance;
            }

            public DateTime GetDateTimeNow()
            {
                return DateTime.Now;
            }
        }
        #endregion

        #region 简单工厂模式
        /// <summary>
        /// 菜抽象类
        /// </summary>
        public abstract class Food
        {
            // 输出点了什么菜
            public abstract void Print();
        }

        /// <summary>
        /// 西红柿炒鸡蛋这道菜
        /// </summary>
        public class TomatoScrambledEggs : Food
        {
            public override void Print()
            {
                Console.WriteLine("一份西红柿炒蛋！");
            }
        }

        /// <summary>
        /// 土豆肉丝这道菜
        /// </summary>
        public class ShreddedPorkWithPotatoes : Food
        {
            public override void Print()
            {
                Console.WriteLine("一份土豆肉丝");
            }
        }

        /// <summary>
        /// 简单工厂类, 负责 炒菜
        /// </summary>
        public class FoodSimpleFactory
        {
            public static Food CreateFood(string type)
            {
                Food food = null;
                if (type.Equals("土豆肉丝"))
                {
                    food = new ShreddedPorkWithPotatoes();
                }
                else if (type.Equals("西红柿炒蛋"))
                {
                    food = new TomatoScrambledEggs();
                }

                return food;
            }
        }
        #endregion

        #region 工厂方法模式
        /// <summary>
        /// 菜抽象类
        /// </summary>
        public abstract class Food2
        {
            // 输出点了什么菜
            public abstract void Print();
        }

        /// <summary>
        /// 西红柿炒鸡蛋这道菜
        /// </summary>
        public class TomatoScrambledEggs2 : Food2
        {
            public override void Print()
            {
                Console.WriteLine("西红柿炒蛋好了！");
            }
        }

        /// <summary>
        /// 土豆肉丝这道菜
        /// </summary>
        public class ShreddedPorkWithPotatoes2 : Food2
        {
            public override void Print()
            {
                Console.WriteLine("土豆肉丝好了");
            }
        }

        /// <summary>
        /// 抽象工厂类
        /// </summary>
        public abstract class Creator
        {
            // 工厂方法
            public abstract Food2 CreateFoddFactory();
        }

        /// <summary>
        /// 西红柿炒蛋工厂类
        /// </summary>
        public class TomatoScrambledEggsFactory2 : Creator
        {
            /// <summary>
            /// 负责创建西红柿炒蛋这道菜
            /// </summary>
            /// <returns></returns>
            public override Food2 CreateFoddFactory()
            {
                return new TomatoScrambledEggs2();
            }
        }

        /// <summary>
        /// 土豆肉丝工厂类
        /// </summary>
        public class ShreddedPorkWithPotatoesFactory2 : Creator
        {
            /// <summary>
            /// 负责创建土豆肉丝这道菜
            /// </summary>
            /// <returns></returns>
            public override Food2 CreateFoddFactory()
            {
                return new ShreddedPorkWithPotatoes2();
            }
        }
        #endregion

        #region 抽象工厂模式

        /// <summary>
        /// 抽象工厂类，提供创建两个不同地方的鸭架和鸭脖的接口
        /// </summary>
        public abstract class AbstractFactory
        {
            // 抽象工厂提供创建一系列产品的接口，这里作为例子，只给出了绝味中鸭脖和鸭架的创建接口
            public abstract YaBo CreateYaBo();
            public abstract YaJia CreateYaJia();
        }

        /// <summary>
        /// 南昌绝味工厂负责制作南昌的鸭脖和鸭架
        /// </summary>
        public class NanChangFactory : AbstractFactory
        {
            // 制作南昌鸭脖
            public override YaBo CreateYaBo()
            {
                return new NanChangYaBo();
            }
            // 制作南昌鸭架
            public override YaJia CreateYaJia()
            {
                return new NanChangYaJia();
            }
        }

        /// <summary>
        /// 上海绝味工厂负责制作上海的鸭脖和鸭架
        /// </summary>
        public class ShangHaiFactory : AbstractFactory
        {
            // 制作上海鸭脖
            public override YaBo CreateYaBo()
            {
                return new ShangHaiYaBo();
            }
            // 制作上海鸭架
            public override YaJia CreateYaJia()
            {
                return new ShangHaiYaJia();
            }
        }

        /// <summary>
        /// 鸭脖抽象类，供每个地方的鸭脖类继承
        /// </summary>
        public abstract class YaBo
        {
            /// <summary>
            /// 打印方法，用于输出信息
            /// </summary>
            public abstract void Print();
        }

        /// <summary>
        /// 鸭架抽象类，供每个地方的鸭架类继承
        /// </summary>
        public abstract class YaJia
        {
            /// <summary>
            /// 打印方法，用于输出信息
            /// </summary>
            public abstract void Print();
        }

        /// <summary>
        /// 南昌的鸭脖类，因为江西人喜欢吃辣的，所以南昌的鸭脖稍微会比上海做的辣
        /// </summary>
        public class NanChangYaBo : YaBo
        {
            public override void Print()
            {
                Console.WriteLine("南昌的鸭脖");
            }
        }

        /// <summary>
        /// 上海的鸭脖没有南昌的鸭脖做的辣
        /// </summary>
        public class ShangHaiYaBo : YaBo
        {
            public override void Print()
            {
                Console.WriteLine("上海的鸭脖");
            }
        }

        /// <summary>
        /// 南昌的鸭架
        /// </summary>
        public class NanChangYaJia : YaJia
        {
            public override void Print()
            {
                Console.WriteLine("南昌的鸭架子");
            }
        }

        /// <summary>
        /// 上海的鸭架
        /// </summary>
        public class ShangHaiYaJia : YaJia
        {
            public override void Print()
            {
                Console.WriteLine("上海的鸭架子");
            }
        }
        #endregion

        #region 建造者模式
        /// <summary>
        /// 小王和小李难道会自愿地去组装嘛，谁不想休息的，这必须有一个人叫他们去组装才会去的
        /// 这个人当然就是老板了，也就是建造者模式中的指挥者
        /// 指挥创建过程类
        /// </summary>
        public class Director
        {
            // 组装电脑
            public void Construct(Builder builder)
            {
                builder.BuildPartCPU();
                builder.BuildPartMainBoard();
            }
        }

        /// <summary>
        /// 电脑类
        /// </summary>
        public class Computer
        {
            // 电脑组件集合
            private IList<string> parts = new List<string>();

            // 把单个组件添加到电脑组件集合中
            public void Add(string part)
            {
                parts.Add(part);
            }

            public void Show()
            {
                Console.WriteLine("电脑开始在组装.......");
                foreach (string part in parts)
                {
                    Console.WriteLine("组件" + part + "已装好");
                }

                Console.WriteLine("电脑组装好了");
            }
        }

        /// <summary>
        /// 抽象建造者，这个场景下为 "组装人" ，这里也可以定义为接口
        /// </summary>
        public abstract class Builder
        {
            // 装CPU
            public abstract void BuildPartCPU();
            // 装主板
            public abstract void BuildPartMainBoard();

            // 当然还有装硬盘，电源等组件，这里省略

            // 获得组装好的电脑
            public abstract Computer GetComputer();
        }

        /// <summary>
        /// 具体创建者，具体的某个人为具体创建者，例如：装机小王啊
        /// </summary>
        public class ConcreteBuilder1 : Builder
        {
            Computer computer = new Computer();
            public override void BuildPartCPU()
            {
                computer.Add("CPU1");
            }

            public override void BuildPartMainBoard()
            {
                computer.Add("Main board1");
            }

            public override Computer GetComputer()
            {
                return computer;
            }
        }

        /// <summary>
        /// 具体创建者，具体的某个人为具体创建者，例如：装机小李啊
        /// 又装另一台电脑了
        /// </summary>
        public class ConcreteBuilder2 : Builder
        {
            Computer computer = new Computer();
            public override void BuildPartCPU()
            {
                computer.Add("CPU2");
            }

            public override void BuildPartMainBoard()
            {
                computer.Add("Main board2");
            }

            public override Computer GetComputer()
            {
                return computer;
            }
        }
        #endregion

        #region 原型模式
        /// <summary>
        /// 孙悟空原型
        /// </summary>
        public abstract class MonkeyKingPrototype
        {
            public string Id { get; set; }
            public MonkeyKingPrototype(string id)
            {
                this.Id = id;
            }

            // 克隆方法，即孙大圣说“变”
            public abstract MonkeyKingPrototype Clone();
        }

        /// <summary>
        /// 创建具体原型
        /// </summary>
        public class ConcretePrototype : MonkeyKingPrototype
        {
            public ConcretePrototype(string id)
                : base(id)
            { }

            /// <summary>
            /// 浅拷贝
            /// </summary>
            /// <returns></returns>
            public override MonkeyKingPrototype Clone()
            {
                // 调用MemberwiseClone方法实现的是浅拷贝，另外还有深拷贝
                return (MonkeyKingPrototype)this.MemberwiseClone();
            }
        }
        #endregion

        #region 适配器模式

        #endregion
    }


}
