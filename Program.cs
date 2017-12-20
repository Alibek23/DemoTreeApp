using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DemoTreeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo Tree and Collection filtering\nChoose the demo:\n1) Compare Trees\n2) Collect unique list");
            var k = Console.ReadKey();
            if (k.KeyChar == '1')
                CheckTrees();
            if (k.KeyChar == '2')
                CollectList();
            if (k.KeyChar != '1' && k.KeyChar != '2')
                Console.WriteLine("Sorry, entered param could not recognized, please try again");

            Console.ReadLine();
        }

        static void CheckTrees()
        {
            Console.Clear();
            Console.WriteLine("Enter numbers for tree A separated by space and press Enter");
            var strA = Console.ReadLine();
            var treeA = FillTree(strA);

            Console.WriteLine("Enter numbers for tree B separated by space and press Enter");
            var strB = Console.ReadLine();
            var treeB = FillTree(strB);

            var isEqual = CompareTrees(treeA, treeB);
            Console.WriteLine(isEqual ? "Trees are equal" : "Trees are not equal");
        }

        static Btn FillTree(string data)
        {
            Btn tree = new Btn { Val = 1 };
            var vals = data.Split(' ');

            foreach (var str in vals)
            {
                var s = Regex.Match(str, @"\d+").Value;
                var num = int.Parse(s);
                var isLeft = num % 2 == 0;
                var leaf = new Btn { Val = num };
                tree = AddLeaf(tree, leaf, isLeft);
            }
            return tree;
        }
        static Btn AddLeaf(Btn tree, Btn leaf, bool isLeft)
        {
            if(tree.Left != null || tree.Right != null)
            {
                if (tree.Left != null)
                    AddLeaf(tree.Left, leaf, isLeft);

                if (tree.Right != null)
                    AddLeaf(tree.Right, leaf, isLeft);
            }
            else
            {
                if (isLeft)
                    tree.Left = leaf;
                else
                    tree.Right = leaf;
            }

            return tree;
        }
        static bool CompareTrees(Btn treeA, Btn treeB)
        {
            if ((treeA == null && treeB != null) || (treeA != null && treeB == null))
                return false;

            if (treeA == null && treeB == null)
                return true;

            if (treeA.Val != treeB.Val)
                return false;

            return CompareTrees(treeA.Left, treeB.Left) && CompareTrees(treeA.Right, treeB.Right);
        }


        static void CollectList()
        {
            Console.Clear();
            Console.WriteLine("For testing porposes, the following data will be used:");
            var elements = new List<Element>
            {
                new Element { num = 5, age = 23, name = "Tomas" },
                new Element { num = 1, age = 35, name = "Chester" },
                new Element { num = 3, age = 41, name = "Brad" },
                new Element { num = 9, age = 13, name = "John" },
                new Element { num = 5, age = 28, name = "Sara" }
            };
            WriteList(elements);
            var unique = FilterList(elements);
            Console.WriteLine("Result:");
            WriteList(unique);
        }

        static IEnumerable<Element> FilterList(IList<Element> elements)
        {
            var nums = new List<int>();
            var list = new List<Element>();

            foreach (var e in elements)
            {
                if (!nums.Contains(e.num) && e.age > 20)
                {
                    list.Add(e);
                    nums.Add(e.num);
                }
            }

            return list;
        }

        static void WriteList(IEnumerable<Element> elements)
        {
            foreach (var u in elements)
            {
                Console.WriteLine(u.num + " " + u.name + " " + u.age);
            }
        }
    }
}
