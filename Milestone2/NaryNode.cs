using System.Text;

namespace Milestone2;

internal class NaryNode<T>
{
    public T Value { get; set; }
    public List<NaryNode<T>> Children { get; init; }

    public NaryNode(T value)
    {
        Value = value;
        Children = new List<NaryNode<T>>();
    }

    public void AddChild(NaryNode<T> node) => Children.Add(node);

    public static void ToString(string spaces, StringBuilder result, NaryNode<T> node)
    {
        result.Append(spaces).Append(node.Value).Append(':').AppendLine();
        spaces += "  ";

        foreach (var child in node.Children)
        {
           ToString(spaces, result, child);
        }        
    }

    public override string ToString()
    {
        var result = new StringBuilder();
        ToString(string.Empty, result, this);
        return result.ToString().TrimEnd();
    }
}
