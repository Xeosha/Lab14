
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Lab_10lib;

namespace Lab14
{

    class Program
    {
        static void Main()
        {
            int qLen = 3, dLen = 5;

            var queue = new Queue<Dictionary<Goods, Toy>>();
            FillQueue(queue, qLen, dLen);

            Console.WriteLine("\tЭлементы стека: ");
            foreach(var item in queue)
            {
                Console.WriteLine("Элементы словаря");
                foreach(var value in item.Keys)
                {
                    Console.WriteLine(value);
                }
            }
               

            Query1(queue);
            Query2(queue);
            Query3(queue);


            Console.ReadKey();
        }

        static void FillQueue(Queue<Dictionary<Goods, Toy>> queue, int sizeQueue, int sizeDict)
        {
            for (int i = 0; i < sizeQueue; i++)
            {
                var goodsToyDict = new Dictionary<Goods, Toy>();
                FillDictionary(goodsToyDict, sizeDict);
                queue.Enqueue(goodsToyDict);
            }
        }

        static void FillDictionary(Dictionary<Goods,Toy> goodsToyDict, int size)
        {
            for (int i = 0; i < size; i++)
            {
                var toy = new Toy();
                var goods = toy.BaseGood;
                goodsToyDict.Add(goods, toy);
            }
        }

        static void Query1(Queue<Dictionary<Goods, Toy>> queue)
        {
            Console.WriteLine("Вывести все уникальные товары: ");

            var uniqueNameLinq = (from dict in queue from toy in dict.Values orderby toy.Name select toy.Name).Distinct();

            Console.WriteLine("С помощью LINQ: ");

            foreach (string model in uniqueNameLinq)
                Console.WriteLine(model);

            var uniqueNameMethods = queue.SelectMany(d => d.Values.Select(t => t.Name)).OrderBy(t => t).Distinct();

            Console.WriteLine("С помощью методов: ");

            foreach (string name in uniqueNameMethods)
                Console.WriteLine(name);
        }

        static void Query2(Queue<Dictionary<Goods, Toy>> queue)
        {
            Console.WriteLine("Вывести количество продуктов с весом ниже 50: ");

            int countLinq = (from dict in queue from product in dict.Values where product.Weight < 50 select product).Count();

            Console.WriteLine("С помощью LINQ: " + countLinq.ToString());

            int countMethod = queue.SelectMany(d => d.Values.Where(p => p.Weight < 50)).Count();

            Console.WriteLine("С помощью методов: " + countMethod.ToString());
        }

        static void Query3(Queue<Dictionary<Goods, Toy>> queue)
        {
            Console.WriteLine("Самая дорогая игрушка в магазине(наименование и стоимость)");

            var maxPriceLinq = (from dict in queue
                                from toy in dict.Values
                                orderby toy.Price descending
                                select toy).FirstOrDefault();

            Console.WriteLine("С помощью LINQ: " + maxPriceLinq?.Name + " - " + maxPriceLinq?.Price);

            var minPriceMethod = queue.SelectMany(d => d.Values)
                                      .OrderByDescending(t => t.Price)
                                      .FirstOrDefault();

            Console.WriteLine("С помощью методов: " + minPriceMethod?.Name + " - " + minPriceMethod?.Price);
        }

        static void Query4(Queue<Dictionary<Goods, Toy>> queue)
        {
            // объединение 
        }

        static void Query5(Queue<Dictionary<Goods, Toy>> queue)
        {
            // группировка данных
        }

    }

}