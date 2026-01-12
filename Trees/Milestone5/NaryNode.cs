using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone5;

internal class NaryNode<T>
{
    internal const double NODE_RADIUS = 25;
    internal const double X_SPACING = 30;
    internal const double Y_SPACING = 30;
    public T Value { get; set; }
    public List<NaryNode<T>> Children { get; init; }
    internal Point Center { get; private set; }
    internal Rect SubtreeBounds { get; private set; }

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

    public List<NaryNode<T>> TraversePreorder()
    {
        var result = new List<NaryNode<T>>();
        result.Add(this);
        foreach (var child in Children)
        {
            result.AddRange(child.TraversePreorder());
        }

        return result;
    }
    public List<NaryNode<T>> TraversePostorder()
    {
        var result = new List<NaryNode<T>>();

        foreach (var child in Children)
        {
            result.AddRange(child.TraversePostorder());
        }

        result.Add(this);

        return result;
    }

    public List<NaryNode<T>> TraverseBreadthFirst()
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

    private void ArrangeSubtree(double x0, double y0)
    {
        var centerY = y0 + NODE_RADIUS;

        if (Children.Count == 0)
        {
            Center = new Point(x0 + NODE_RADIUS, centerY);
            SubtreeBounds = new Rect(x0, y0, 2 * NODE_RADIUS, 2 * NODE_RADIUS);
            return;
        }

        var childX0 = x0;
        var childY0 = y0 + 2 * NODE_RADIUS + Y_SPACING;

        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].ArrangeSubtree(childX0, childY0);
            childX0 = Children[i].SubtreeBounds.Right + X_SPACING;
        }

        var width = Children[^1].SubtreeBounds.Right - Children[0].SubtreeBounds.Left;
        var height = Children.Select(x => x.SubtreeBounds.Height).Max() + Y_SPACING + 2 * NODE_RADIUS;

        Center = new Point((Children[0].Center.X + Children[^1].Center.X) / 2, centerY);
        SubtreeBounds = new Rect(x0, y0, width, height);
    }

    private void DrawSubtreeLinks(Canvas canvas)
    {
        Brush stroke = new SolidColorBrush(Colors.Black);

        if (Children.Count == 1)
        {
            canvas.DrawLine(Center, Children[0].Center, stroke, 0.5);
            Children[0].DrawSubtreeLinks(canvas);
        }
        else if (Children.Count > 1)
        {
            var horizontalLineY = Center.Y + Y_SPACING / 2 + NODE_RADIUS;
            canvas.DrawLine(Center, new Point(Center.X, horizontalLineY), stroke, 0.5);
            canvas.DrawLine(new Point(Children[0].Center.X, horizontalLineY), new Point(Children[^1].Center.X, horizontalLineY), stroke, 0.5);

            foreach (var child in Children)
            {
                canvas.DrawLine(new Point(child.Center.X, horizontalLineY), child.Center, stroke, 0.5);
                child.DrawSubtreeLinks(canvas);
            }
        }
    }

    private void DrawSubtreeNodes(Canvas canvas)
    {
        var bounds = new Rect(Center.X - NODE_RADIUS, Center.Y - NODE_RADIUS, 2 * NODE_RADIUS, 2 * NODE_RADIUS);
        canvas.DrawEllipse(bounds, Brushes.WhiteSmoke, Brushes.DarkGreen, 0.8);
        canvas.DrawLabel(bounds, Value, Brushes.Transparent, Brushes.Red, HorizontalAlignment.Center, VerticalAlignment.Center, 18, 0);

        foreach(var child in Children)
        {
            child.DrawSubtreeNodes(canvas);
        }       
    }

    public void ArrangeAndDrawSubtree(Canvas canvas, double x0, double y0)
    {
        ArrangeSubtree(x0, y0);
        DrawSubtreeLinks(canvas);
        DrawSubtreeNodes(canvas);
    }
}
