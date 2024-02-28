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
        public static void SortToys(this BinaryTree<Toy> tree, Func<Toy, int> sortByFunc) 
        {
            var sortedTrains = tree.OrderBy(sortByFunc);
            var sortedTrainsArr = sortedTrains.ToArray();

            tree.Clear();
            tree.AddRange(sortedTrainsArr);
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
