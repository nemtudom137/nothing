namespace Milestone4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var binTree = GetExampleBinTree();

            DisplayList("Preorder:", binTree.TraversePreorder());
            DisplayList("Inorder:", binTree.TraverseInorder());
            DisplayList("Postorder:", binTree.TraversePostorder());
            DisplayList("Breadth First:", binTree.TraverseBreadthFirst());
            Console.WriteLine();

            var naryTree = GetExampleNaryTree();

            DisplayList("Preorder:", naryTree.TraversePreorder());
            DisplayList("Postorder:", naryTree.TraversePostorder());
            DisplayList("Breadth First:", naryTree.TraverseBreadthFirst());
        }

        static void DisplayList<T>(string type, List<BinaryNode<T>> list)
        {
            Console.Write($"{type,-15}");
            foreach (BinaryNode<T> node in list)
            {
                Console.Write($"{node.Value} ");
            }
            Console.WriteLine();
        }

        static void DisplayList<T>(string type, List<NaryNode<T>> list)
        {
            Console.Write($"{type,-15}");
            foreach (NaryNode<T> node in list)
            {
                Console.Write($"{node.Value} ");
            }
            Console.WriteLine();
        }

        static BinaryNode<string> GetExampleBinTree()
        {
            var root = new BinaryNode<string>("Root");
            var a = new BinaryNode<string>("a");
            var b = new BinaryNode<string>("b");
            var c = new BinaryNode<string>("c");
            var d = new BinaryNode<string>("d");
            var e = new BinaryNode<string>("e");
            var f = new BinaryNode<string>("f");
            root.AddLeft(a);
            root.AddRight(b);
            a.AddLeft(c);
            a.AddRight(d);
            b.AddRight(e);
            e.AddLeft(f);

            return root;
        }

        static NaryNode<string> GetExampleNaryTree()
        {
            var root = new NaryNode<string>("Root");
            var a = new NaryNode<string>("A");
            var b = new NaryNode<string>("B");
            var c = new NaryNode<string>("C");
            var d = new NaryNode<string>("D");
            var e = new NaryNode<string>("E");
            var f = new NaryNode<string>("F");
            var g = new NaryNode<string>("G");
            var h = new NaryNode<string>("H");
            var i = new NaryNode<string>("I");

            root.AddChild(a);
            root.AddChild(b);
            root.AddChild(c);
            a.AddChild(d);
            a.AddChild(e);
            c.AddChild(f);
            d.AddChild(g);
            f.AddChild(h);
            f.AddChild(i);

            return root;
        }
    }   
}
