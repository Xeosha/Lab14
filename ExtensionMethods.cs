using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinarySearchTree;
using Lab_10lib;

namespace Lab14
{
    public static class ExtensionMethods
    {
        public static BinaryTree<Toy> SortToys(this BinaryTree<Toy> tree, Func<Toy, int> sortByFunc)
        {

            var comparer = Comparer<Toy>.Create((toy1, toy2) => sortByFunc(toy1).CompareTo(sortByFunc(toy2)));

            var newTree = new BinaryTree<Toy>(comparer);

            foreach(var toy in tree)
            {
                try { newTree.Add(toy); }
                catch (Exception e) { Console.WriteLine("Не удалось добавить элемент, т.к. он уже есть: " + toy); }
            }
            
            return newTree;
        }

        public static IEnumerable<Toy> SelectToys(this BinaryTree<Toy> toys, Func<Toy, bool> selectRule)
        {
            var selection = toys.Where(selectRule);

            return selection;
        }

        public static int CountToys(this BinaryTree<Toy> trains, Func<Toy, bool> selectRule)
        {
            var selection = trains.Where(selectRule);

            return selection.Count();
        }
    }
}
