using System.Text;

namespace binary_node1;

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

    public string ToString(Func<T, string> nullHandler)
    {
        var tmp = new Queue<BinaryNode<T>>();
        tmp.Enqueue(this);
        var result = new StringBuilder();
        while (tmp.Count > 0)
        {
            var node = tmp.Dequeue();
            result.Append(nullHandler(node.Value)).Append(':');
            if (node.LeftChild is null)
            {
                result.Append(" null");
            }
            else
            {
                result.Append(' ').Append(nullHandler(node.LeftChild.Value));
                tmp.Enqueue(node.LeftChild);
            }

            if (node.RightChild is null)
            {
                result.Append(" null");
            }
            else
            {
                result.Append(' ').Append(nullHandler(node.RightChild.Value));
                tmp.Enqueue(node.RightChild);
            }

            result.AppendLine();
        }

        return result.ToString().TrimEnd();
    }

    public override string ToString() => ToString(value => value?.ToString() ?? "null");    
}
