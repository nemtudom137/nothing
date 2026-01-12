using System.Xml.Linq;

namespace Milestone3;

internal class Program
{
    static void Main(string[] args)
    {
        var root1 = new BinaryNode<string>("Root");
        var A1 = new BinaryNode<string>("A");
        var B1 = new BinaryNode<string>("B");
        var C1 = new BinaryNode<string>("C");
        var D1 = new BinaryNode<string>("D");
        var E1 = new BinaryNode<string>("E");
        var F1 = new BinaryNode<string>("F");
        root1.AddLeft(A1);
        root1.AddRight(B1);
        A1.AddLeft(C1);
        A1.AddRight(D1);
        B1.AddRight(E1);
        E1.AddLeft(F1);

        FindValue(root1, "Root");
        FindValue(root1, "E");
        FindValue(root1, "F");
        FindValue(root1, "Q");
        FindValue(B1, "F");
        Console.WriteLine();

        var root2 = new NaryNode<string>("Root");
        var A2 = new NaryNode<string>("A");
        var B2 = new NaryNode<string>("B");
        var C2 = new NaryNode<string>("C");
        var D2 = new NaryNode<string>("D");
        var E2 = new NaryNode<string>("E");
        var F2 = new NaryNode<string>("F");
        var G2 = new NaryNode<string>("G");
        var H2 = new NaryNode<string>("H");
        var I2 = new NaryNode<string>("I");

        root2.AddChild(A2);
        root2.AddChild(B2);
        root2.AddChild(C2);
        A2.AddChild(D2);
        A2.AddChild(E2);
        C2.AddChild(F2);
        D2.AddChild(G2);
        F2.AddChild(H2);
        F2.AddChild(I2);

        FindValue(root2, "Root");
        FindValue(root2, "E");
        FindValue(root2, "F");
        FindValue(root2, "Q");
        FindValue(C2, "F");

    }
    static void FindValue<T>(BinaryNode<T> node, T value)
    {
        if(node.FindNode(value) is null)
        {
            Console.WriteLine($"Value {value} not found");
        }
        else
        {
            Console.WriteLine($"Found {value}");
        }
    }

    static void FindValue<T>(NaryNode<T> node, T value)
    {
        if (node.FindNode(value) is null)
        {
            Console.WriteLine($"Value {value} not found");
        }
        else
        {
            Console.WriteLine($"Found {value}");
        }
    }
}
