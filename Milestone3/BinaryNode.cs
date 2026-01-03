namespace Milestone3;

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

    public string ToString(string spaces)
    {
        var result = $"{spaces}{Value}:\n";
        spaces += "  ";
        if (LeftChild is not null || RightChild is not null)
        {
            if (LeftChild is null)
            {
                result += $"{spaces}null\n";
            }
            else
            {
                result += LeftChild.ToString(spaces);
            }

            if (RightChild is null)
            {
                result += $"{spaces}null\n";
            }
            else
            {
                result += RightChild.ToString(spaces);
            }
        }

        return result;
    }

    public override string ToString() => ToString(string.Empty);

    public BinaryNode<T>? FindNode(T value)
    {
        if(Value.Equals(value))
        {
            return this;
        }

        if(LeftChild is not null)
        {
            var tmp = LeftChild.FindNode(value);
            if(tmp is not null)
            {
                return tmp;
            }
        }

        if (RightChild is not null)
        {
            var tmp = RightChild.FindNode(value);
            if (tmp is not null)
            {
                return tmp;
            }
        }

        return null;
    }
}
