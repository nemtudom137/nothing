using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone5;

internal class BinaryNode<T>
{

    internal const double NODE_RADIUS = 25;
    internal const double X_SPACING = 30;
    internal const double Y_SPACING = 30;
    internal T Value { get; set; }
    internal BinaryNode<T>? LeftChild { get; private set; }
    internal BinaryNode<T>? RightChild { get; private set; }
    internal Point Center { get; private set; }
    internal Rect SubtreeBounds { get; private set; }

    internal BinaryNode(T value)
    {
        Value = value;
    }

    internal void AddLeft(BinaryNode<T> node) => LeftChild = node;

    internal void AddRight(BinaryNode<T> node) => RightChild = node;

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

    internal BinaryNode<T>? FindNode(T value)
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

    internal List<BinaryNode<T>> TraversePreorder()
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

    internal List<BinaryNode<T>> TraverseInorder()
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

    internal List<BinaryNode<T>> TraversePostorder()
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

    internal List<BinaryNode<T>> TraverseBreadthFirst()
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

    private void ArrangeSubtree(double x0, double y0)
    {
        var centerY = y0 + NODE_RADIUS;

        if (LeftChild is null && RightChild is null)
        {
            Center = new Point(x0 + NODE_RADIUS, centerY);
            SubtreeBounds = new Rect(x0, y0, 2 * NODE_RADIUS, 2 * NODE_RADIUS);
            return;
        }

        var childY0 = y0 + 2 * NODE_RADIUS + Y_SPACING;        

        if (RightChild is null)
        {
            LeftChild.ArrangeSubtree(x0, childY0);
            Center = new Point(x0 + LeftChild!.SubtreeBounds.Width / 2, centerY);
            SubtreeBounds = new Rect(x0, y0, LeftChild.SubtreeBounds.Width, LeftChild.SubtreeBounds.Height + Y_SPACING + 2 * NODE_RADIUS);
        }
        else if (LeftChild is null)
        {
            RightChild.ArrangeSubtree(x0, childY0);
            Center = new Point(x0 + RightChild.SubtreeBounds.Width / 2, centerY);
            SubtreeBounds = new Rect(x0, y0, RightChild.SubtreeBounds.Width, RightChild.SubtreeBounds.Height + Y_SPACING + 2 * NODE_RADIUS);
        }
        else
        {
            LeftChild.ArrangeSubtree(x0, childY0);
            RightChild.ArrangeSubtree(LeftChild.SubtreeBounds.Right + X_SPACING, childY0);
            
            var width = LeftChild.SubtreeBounds.Width + RightChild.SubtreeBounds.Width + X_SPACING;
            var height = Math.Max(RightChild.SubtreeBounds.Height, LeftChild.SubtreeBounds.Height) + Y_SPACING + 2 * NODE_RADIUS;
            
            Center = new Point((LeftChild.Center.X + RightChild.Center.X)/ 2, centerY);
            SubtreeBounds = new Rect(x0, y0, width, height);
        }
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
