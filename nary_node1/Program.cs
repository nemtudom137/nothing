namespace nary_node1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var root = new NaryNode<string>("Root");
            var A = new NaryNode<string>("A");
            var B = new NaryNode<string>("B");
            var C = new NaryNode<string>("C");
            var D = new NaryNode<string>("D");
            var E = new NaryNode<string>("E");
            var F = new NaryNode<string>("F");
            var G = new NaryNode<string>("G");
            var H = new NaryNode<string>("H");
            var I = new NaryNode<string>("I");

            root.AddChild(A);
            root.AddChild(B);
            root.AddChild(C);
            A.AddChild(D);
            A.AddChild(E);
            C.AddChild(F);
            D.AddChild(G);
            F.AddChild(H);
            F.AddChild(I);

            Console.WriteLine(root);
        }
    }
}
