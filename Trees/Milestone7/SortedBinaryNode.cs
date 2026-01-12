using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone7;

internal class SortedBinaryNode<T> where T : IComparable<T>
{

    private const double NODE_RADIUS = 20;
    private const double X_SPACING = 25;
    private const double Y_SPACING = 25;
    internal T Value { get; set; }
    internal SortedBinaryNode<T>? LeftChild { get; private set; }
    internal SortedBinaryNode<T>? RightChild { get; private set; }
    internal Point Center { get; private set; }
    internal Rect SubtreeBounds { get; private set; }

    internal SortedBinaryNode(T value)
    {
        Value = value;
    }

    internal void AddLeft(SortedBinaryNode<T> node) => LeftChild = node;

    internal void AddRight(SortedBinaryNode<T> node) => RightChild = node;

    internal void AddNode(T newValue)
    {
        switch (Value.CompareTo(newValue))
        {
            case < 0:
                if (RightChild is null)
                {
                    RightChild = new SortedBinaryNode<T>(newValue);
                }
                else
                {
                    RightChild.AddNode(newValue);
                }
                break;
            case > 0:
                if (LeftChild is null)
                {
                    LeftChild = new SortedBinaryNode<T>(newValue);
                }
                else
                {
                    LeftChild.AddNode(newValue);
                }
                break;
            default:
                throw new ArgumentException(nameof(newValue));
        }
    }
    internal void DeleteAllChildren()
    {
        LeftChild = null;
        RightChild = null;
    }

    internal void RemoveNode(T value)
    {
        RemoveNode(this, value);
    }

    private static SortedBinaryNode<T>? RemoveNode(SortedBinaryNode<T>? node, T value)
    {
        if (node is null)
        {
            return null;
        }

        switch (node.Value.CompareTo(value))
        {
            case < 0:
                node.RightChild = RemoveNode(node.RightChild, value);
                break;
            case > 0:
                node.LeftChild = RemoveNode(node.LeftChild, value);
                break;
            default:
                if (node.RightChild is null && node.LeftChild is null)
                {
                    return null;
                }
                else if (node.LeftChild is null)
                {
                    return node.RightChild;
                }
                else if (node.RightChild is null)
                {
                    return node.LeftChild;
                }
                else
                {
                    node.Value = FindMax(node.LeftChild).Value;
                    node.LeftChild = RemoveNode(node.LeftChild, node.Value);
                }
                break;
        }

        return node;
    }

    private static SortedBinaryNode<T> FindMax(SortedBinaryNode<T> node)
    {
        while (node.RightChild is not null)
        {
            node = node.RightChild;
        }

        return node;
    }

    private string ToString(string spaces)
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

    internal SortedBinaryNode<T>? FindNode(T value)
    {
        switch (Value.CompareTo(value))
        {
            case < 0:
                return RightChild?.FindNode(value);
            case > 0:
                return LeftChild?.FindNode(value);
            default:
                return this;
        }
    }

    internal List<SortedBinaryNode<T>> TraversePreorder()
    {
        var result = new List<SortedBinaryNode<T>>();
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

    internal List<SortedBinaryNode<T>> TraverseInorder()
    {
        var result = new List<SortedBinaryNode<T>>();
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

    internal List<SortedBinaryNode<T>> TraversePostorder()
    {
        var result = new List<SortedBinaryNode<T>>();
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

    internal List<SortedBinaryNode<T>> TraverseBreadthFirst()
    {
        var result = new List<SortedBinaryNode<T>>();
        var nodes = new Queue<SortedBinaryNode<T>>();
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

    private void ArrangeSubtree(double x0, double y0)
    {
        var centerY = y0 + NODE_RADIUS;

        if (LeftChild is null && RightChild is null)
        {
            Center = new Point(x0 + NODE_RADIUS, centerY);
            SubtreeBounds = new Rect(x0, y0, 2 * NODE_RADIUS, 2 * NODE_RADIUS);
            return;
        }

        if (RightChild is null)
        {
            SetNodeWithOneChild(LeftChild, x0, y0);
        }
        else if (LeftChild is null)
        {
            SetNodeWithOneChild(RightChild, x0, y0);
        }
        else
        {
            var childY0 = y0 + 2 * NODE_RADIUS + Y_SPACING;

            LeftChild.ArrangeSubtree(x0, childY0);
            RightChild.ArrangeSubtree(LeftChild.SubtreeBounds.Right + X_SPACING, childY0);

            var width = LeftChild.SubtreeBounds.Width + RightChild.SubtreeBounds.Width + X_SPACING;
            var height = Math.Max(RightChild.SubtreeBounds.Height, LeftChild.SubtreeBounds.Height) + Y_SPACING + 2 * NODE_RADIUS;

            Center = new Point((LeftChild.Center.X + RightChild.Center.X) / 2, centerY);
            SubtreeBounds = new Rect(x0, y0, width, height);
        }
    }

    private void SetNodeWithOneChild(SortedBinaryNode<T> child, double x0, double y0)
    {
        var childY0 = y0 + 2 * NODE_RADIUS + Y_SPACING;

        child.ArrangeSubtree(x0, childY0);
        Center = new Point(child.Center.X, y0 + NODE_RADIUS);
        SubtreeBounds = new Rect(x0, y0, child.SubtreeBounds.Width, child.SubtreeBounds.Height + Y_SPACING + 2 * NODE_RADIUS);
    }

    private void DrawSubtreeLinks(Canvas canvas)
    {
        Brush stroke = new SolidColorBrush(Colors.Black);

        if (LeftChild is not null)
        {
            canvas.DrawLine(Center, LeftChild.Center, stroke, 0.5);
            LeftChild.DrawSubtreeLinks(canvas);
        }

        if (RightChild is not null)
        {
            canvas.DrawLine(Center, RightChild.Center, stroke, 0.5);
            RightChild.DrawSubtreeLinks(canvas);
        }
    }

    private void DrawSubtreeNodes(Canvas canvas)
    {
        var bounds = new Rect(Center.X - NODE_RADIUS, Center.Y - NODE_RADIUS, 2 * NODE_RADIUS, 2 * NODE_RADIUS);
        canvas.DrawEllipse(bounds, Brushes.WhiteSmoke, Brushes.DarkGreen, 0.8);
        canvas.DrawLabel(bounds, Value, Brushes.Transparent, Brushes.Red, HorizontalAlignment.Center, VerticalAlignment.Center, 18, 0);

        LeftChild?.DrawSubtreeNodes(canvas);
        RightChild?.DrawSubtreeNodes(canvas);
    }

    public void ArrangeAndDrawSubtree(Canvas canvas, double x0, double y0)
    {
        ArrangeSubtree(x0, y0);
        DrawSubtreeLinks(canvas);
        DrawSubtreeNodes(canvas);
    }
}
