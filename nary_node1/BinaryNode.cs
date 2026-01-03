using System.Text;

namespace Milestone2;

internal class BinaryNode<T>
{
    public T Value { get; set; }
    public BinaryNode<T>? LeftChild { get; private set; }
    public BinaryNode<T>? RightChild { get; private set; }
    public BinaryNode(T value)
    {
        Value = value;
    }

    public void AddLeft(BinaryNode<T> node) => LeftChild = node;

    public void AddRight(BinaryNode<T> node) => RightChild = node;

    public static void ToString(string spaces, StringBuilder result, BinaryNode<T> node)
    {
        if (node is null)
        {
            result.Append(spaces).Append("null").AppendLine();
            return;
        }

        result.Append(spaces).Append(node!.Value).Append(':').AppendLine();

        if (node.LeftChild is null && node.RightChild is null)
        {
            return;
        }

        spaces += "  ";
        ToString(spaces, result, node.LeftChild!);
        ToString(spaces, result, node.RightChild!);
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        ToString(string.Empty, result, this);
        return result.ToString().TrimEnd();
    }
}
