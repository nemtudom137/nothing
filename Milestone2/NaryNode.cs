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

    public string ToString(Func<T, string> nullHandler)
    {
        var tmp = new Queue<NaryNode<T>>();
        tmp.Enqueue(this);
        var result = new StringBuilder();
        while (tmp.Count > 0)
        {
            var node = tmp.Dequeue();
            result.Append(nullHandler(node.Value)).Append(':');

            foreach (var child in node.Children)
            {
                result.Append(' ').Append(nullHandler(child.Value));
                tmp.Enqueue(child);
            }

            result.AppendLine();
        }

        return result.ToString().TrimEnd();
    }

    public static void ToString(string spaces, StringBuilder result, NaryNode<T> node)
    {
        if (node is null)
        {
            return;
        }

        result.Append(spaces).Append(node!.Value).Append(':').AppendLine();
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
