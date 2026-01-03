namespace binary_node1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var root = new BinaryNode<string>("Root");
            
            var A = new BinaryNode<string>(null!);
            var B = new BinaryNode<string>("B");
            var C = new BinaryNode<string>("C");
            var D = new BinaryNode<string>("D");
            var E = new BinaryNode<string>("E");
            var F = new BinaryNode<string>("F");
            root.AddLeft(A);
            root.AddRight(B);
            A.AddLeft(C);
            A.AddRight(D);
            B.AddRight(E);
            E.AddLeft(F);
            Console.WriteLine(root);
            Console.WriteLine();
            Console.WriteLine(E);
            Console.WriteLine();
            Console.WriteLine(C);
        }
    }
}
