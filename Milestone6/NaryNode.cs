using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone6;

internal class NaryNode<T>
{
    internal T Value { get; set; }
    internal List<NaryNode<T>> Children { get; init; }
    internal Point Center { get; set; }
    internal Rect SubtreeBounds { get; set; }

    internal bool IsLeaf => Children.Count == 0;
    internal bool IsTwig => Children.Count > 0 && Children.All(x => x.IsLeaf);

    internal NaryNode(T value)
    {
        Value = value;
        Children = new List<NaryNode<T>>();
    }

    internal void AddChild(NaryNode<T> node) => Children.Add(node);

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

    internal NaryNode<T>? FindNode(T value)
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

    internal List<NaryNode<T>> TraversePreorder()
    {
        var result = new List<NaryNode<T>>();
        result.Add(this);
        foreach (var child in Children)
        {
            result.AddRange(child.TraversePreorder());
        }

        return result;
    }

    internal List<NaryNode<T>> TraversePostorder()
    {
        var result = new List<NaryNode<T>>();

        foreach (var child in Children)
        {
            result.AddRange(child.TraversePostorder());
        }

        result.Add(this);

        return result;
    }

    internal List<NaryNode<T>> TraverseBreadthFirst()
    {
        var result = new List<NaryNode<T>>();
        var nodes = new Queue<NaryNode<T>>();
        nodes.Enqueue(this);

        while (nodes.Count > 0)
        {
            var tmp = nodes.Dequeue();
            result.Add(tmp);
            foreach (var child in tmp.Children)
            {
                nodes.Enqueue(child);
            }
        }

        return result;
    }

    public void ArrangeAndDrawSubtree(Canvas canvas, double x0, double y0, double dpi)
    {
        var d = new OrgChart(dpi, canvas);
        d.ArrangeSubtree(this, x0, y0);
        d.DrawSubtreeLinks(this);
        d.DrawSubtreeNodes(this);
    }
}
