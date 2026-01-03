namespace Milestone2;

internal class Program
{
    static void Main(string[] args)
    {
        DisplayBinaryNode();
        Console.WriteLine("___________");
        DisplayNaryNode();
    }

    static void DisplayBinaryNode()
    {
        var root = new BinaryNode<string>("Root");

        var A = new BinaryNode<string>("A");
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
        Console.WriteLine("___________");
        Console.WriteLine(A);        
    }

    static void DisplayNaryNode()
    {
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
        Console.WriteLine("___________");
        Console.WriteLine(A);
    }
}
