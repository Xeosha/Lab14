
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Lab_10lib;
using BinarySearchTree;

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
            Query4(queue);  
            Query5(queue);

            var treeToys = new BinaryTree<Toy>();
            FillTree(treeToys, dLen);

            MethodQuery1(treeToys);
            MethodQuery2(treeToys);
            MethodQuery3(treeToys);



            Console.ReadKey();
        }

        static void FillTree(BinaryTree<Toy> toys, int size)
        {
            var trainArr = new Toy[size];

            for (int i = 0; i < size; i++)
            {
                trainArr[i] = new Toy();
            }

            toys.AddRange(trainArr);
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
            Console.WriteLine("Объединение товаров с возрастным ограничением >10 и <5");

            var toysLinq = (from dict in queue
                            from toy in dict.Values
                            where toy.AgeRestriction < 5
                            select toy).Union
                            (from dict in queue
                             from toy in dict.Values
                             where toy.AgeRestriction > 10
                             select toy);


            Console.WriteLine("С помощью LINQ: ");
            foreach (var toy in toysLinq)
                Console.WriteLine(toy.Name + " - " + toy.AgeRestriction);

            var toysOver18 = queue.SelectMany(d => d.Values)
                                .Where(t => t.AgeRestriction > 10);

            var toysUnder6 = queue.SelectMany(d => d.Values)
                                  .Where(t => t.AgeRestriction < 5);

            var combinedToys = toysOver18.Union(toysUnder6);

            Console.WriteLine("С помощью методов: ");
            foreach (var toy in combinedToys)
                Console.WriteLine(toy.Name + " - " + toy.AgeRestriction);


        }

        static void Query5(Queue<Dictionary<Goods, Toy>> queue)
        {
            Console.WriteLine("Группировка игрушек по названию");

            var groupByLinq = (from dict in queue
                               from toy in dict.Values
                               group toy by toy.Name into g
                               select new { Name = g.Key, Count = g.Count() }).ToList();

            foreach (var item in groupByLinq)
            {
                Console.WriteLine("С помощью LINQ: " + item.Name + " - " + item.Count);
            }

            var groupByMethod = queue.SelectMany(d => d.Values)
                                    .GroupBy(t => t.Name)
                                    .Select(g => new { Name = g.Key, Count = g.Count() }).ToList();

            foreach (var item in groupByMethod)
            {
                Console.WriteLine("С помощью методов: " + item.Name + " - " + item.Count);
            }
        }
        
        // Не работает
        static void MethodQuery1(BinaryTree<Toy> toys)
        {
            Console.WriteLine("Сортировка по возрастному ограничению: ");

            var newToys = toys.SortToys(t => t.AgeRestriction);

            foreach (var toy in newToys)
                Console.WriteLine(toy.ToString());

            newToys.PrintTree();
            
        }

        static void MethodQuery2(BinaryTree<Toy> toys)
        {
            Console.WriteLine("Подсчет количества элементов с названием Товар_1: " + toys.CountToys(t => t.Name == "Товар_1").ToString());
        }

        static void MethodQuery3(BinaryTree<Toy> toys)
        {
            Console.WriteLine("Вывести все поезда с 1 в названии: ");

            var zeroToys = toys.SelectToys(t => t.Name.Contains("1"));

            foreach (var toy in zeroToys)
                Console.WriteLine(toy.ToString());
        }

    }

}