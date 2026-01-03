using System.Text;

namespace nary_node1;

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

    public override string ToString() => ToString(value => value?.ToString() ?? "null");
}
