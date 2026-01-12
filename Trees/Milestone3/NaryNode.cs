namespace Milestone3;

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

    public string ToString(string spaces)
    {
        var result = $"{spaces}{Value}:\n";
        spaces += "  ";

        foreach (var child in Children)
        {
            result += child.ToString(spaces);
        }

        return result;
    }

    public override string ToString() => ToString(string.Empty);

    public NaryNode<T>? FindNode(T value)
    {
        if (Value.Equals(value))
        {
            return this;
        }

        foreach (var child in Children)
        {
            var tmp = child.FindNode(value);
            if (tmp is not null)
            {
                return tmp;
            }
        }

        return null;
    }
}
