namespace Milestone4;

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
        if (Value.Equals(value))
        {
            return this;
        }

        if (LeftChild is not null)
        {
            var tmp = LeftChild.FindNode(value);
            if (tmp is not null)
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

    public List<BinaryNode<T>> TraversePreorder()
    {
        var result = new List<BinaryNode<T>>();
        result.Add(this);
        if (LeftChild is not null)
        {
            result.AddRange(LeftChild.TraversePreorder());
        }

        if (RightChild is not null)
        {
            result.AddRange(RightChild.TraversePreorder());
        }

        return result;
    }

    public List<BinaryNode<T>> TraverseInorder()
    {
        var result = new List<BinaryNode<T>>();
        if (LeftChild is not null)
        {
            result.AddRange(LeftChild.TraverseInorder());
        }

        result.Add(this);

        if (RightChild is not null)
        {
            result.AddRange(RightChild.TraverseInorder());
        }

        return result;
    }

    public List<BinaryNode<T>> TraversePostorder()
    {
        var result = new List<BinaryNode<T>>();
        if (LeftChild is not null)
        {
            result.AddRange(LeftChild.TraversePostorder());
        }

        if (RightChild is not null)
        {
            result.AddRange(RightChild.TraversePostorder());
        }

        result.Add(this);

        return result;
    }

    public List<BinaryNode<T>> TraverseBreadthFirst()
    {
        var result = new List<BinaryNode<T>>();
        var nodes = new Queue<BinaryNode<T>>();
        nodes.Enqueue(this);

        while (nodes.Count > 0)
        { 
            var tmp = nodes.Dequeue();
            result.Add(tmp);
            if (tmp.LeftChild is not null)
            {
                nodes.Enqueue(tmp.LeftChild);
            }

            if (tmp.RightChild is not null)
            {
                nodes.Enqueue(tmp.RightChild);
            }
        }

        return result;
    }
}
