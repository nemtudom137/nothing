using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone6;
/// <summary>
/// Something disgusting...
/// </summary>
internal class OrgChart
{
    private const double BOX_HALF_HEIGHT = 20;
    private const double X_SPACING = 30;
    private const double Y_SPACING = 30;
    private const double DIFF_Y = 2 * BOX_HALF_HEIGHT + Y_SPACING;
    private const double HALF_TWIG_INDENTATION = 10;
    private const double STROKE_THICKNESS = 0.8;
    private const double FONT_SIZE = 18;
    private const double TEXT_PADDING = 5;
    private static Brush LinkColor = Brushes.Black;
    private static Brush NodeStrokeColor = Brushes.DarkGoldenrod;
    private static Brush NodeFillColor = Brushes.Azure;
    private static Brush TwigNodeFillColor = Brushes.Cornsilk;

    public double PixelsPerDip { get; init; }
    public Canvas Canvas { get; init; }

    internal OrgChart(double dpi, Canvas canvas)
    {
        PixelsPerDip = dpi;
        Canvas = canvas;
    }

    internal void ArrangeSubtree<T>(NaryNode<T> node, double x0, double y0)
    {
        if (node.IsTwig)
        {
            ArrangeTwig(node, x0, y0);
        }
        else if (node.IsLeaf)
        {
            ArrangeANode(node, x0, y0);
        }
        else
        {
            ArrangeInternalNode(node, x0, y0);
        }
    }

    internal void DrawSubtreeLinks<T>(NaryNode<T> node)
    {
        if (node.IsTwig)
        {
            var verticalLineX = node.Center.X - CalculateWidth(node) / 2 + HALF_TWIG_INDENTATION;
            DrawLink(new Point(verticalLineX, node.Center.Y), new Point(verticalLineX, node.Children[^1].Center.Y));
            foreach (var child in node.Children)
            {
                DrawLink(new Point(verticalLineX, child.Center.Y), child.Center);
            }
        }
        else if (node.Children.Count == 1)
        {
            DrawLink(node.Center, node.Children[0].Center);
            DrawSubtreeLinks(node.Children[0]);
        }
        else if (node.Children.Count > 1)
        {
            var horizontalLineY = node.Center.Y + Y_SPACING / 2 + BOX_HALF_HEIGHT;
            DrawLink(node.Center, new Point(node.Center.X, horizontalLineY));
            DrawLink(new Point(node.Children[0].Center.X, horizontalLineY), new Point(node.Children[^1].Center.X, horizontalLineY));

            foreach (var child in node.Children)
            {
                DrawLink(new Point(child.Center.X, horizontalLineY), child.Center);
                DrawSubtreeLinks(child);
            }
        }
    }

    internal void DrawSubtreeNodes<T>(NaryNode<T> node)
    {
        DrawNode(node, NodeFillColor);

        if (node.IsTwig)
        {
            foreach (var child in node.Children)
            {
                DrawNode(child, TwigNodeFillColor);
            }
        }
        else
        {
            foreach (var child in node.Children)
            {
                DrawSubtreeNodes(child);
            }
        }
    }

    private void ArrangeTwig<T>(NaryNode<T> node, double x0, double y0)
    {
        var childX0 = x0 + 2 * HALF_TWIG_INDENTATION;
        var childY0 = y0 + DIFF_Y;

        foreach (NaryNode<T> child in node.Children)
        {
            ArrangeANode(child, childX0, childY0);
            childY0 += DIFF_Y;
        }

        var nodeWidth = CalculateWidth(node);
        var childrenWidth = node.Children.Select(x => x.SubtreeBounds.Right - x0).Max();
        var twigWidth = Math.Max(childrenWidth, nodeWidth);
        var twigHeight = node.Children[^1].SubtreeBounds.Bottom - y0;

        node.Center = new Point(x0 + nodeWidth / 2, y0 + BOX_HALF_HEIGHT);
        node.SubtreeBounds = new Rect(x0, y0, twigWidth, twigHeight);
    }

    private void ArrangeANode<T>(NaryNode<T> node, double x0, double y0)
    {
        var nodeWidth = CalculateWidth(node);
        node.Center = new Point(x0 + nodeWidth / 2, y0 + BOX_HALF_HEIGHT);
        node.SubtreeBounds = new Rect(x0, y0, nodeWidth, 2 * BOX_HALF_HEIGHT);
    }

    private void ArrangeInternalNode<T>(NaryNode<T> node, double x0, double y0)
    {
        var childX0 = x0;
        var childY0 = y0 + DIFF_Y;

        foreach (NaryNode<T> child in node.Children)
        {
            ArrangeSubtree(child, childX0, childY0);
            childX0 = child.SubtreeBounds.Right + X_SPACING;
        }

        var nodeWidth = CalculateWidth(node);
        var nodeCenterX = x0 + nodeWidth / 2;
        var childrenCenterX = (node.Children[0].Center.X + node.Children[^1].Center.X) / 2;

        if (nodeCenterX > childrenCenterX)
        {
            var offset = nodeCenterX - childrenCenterX;
            OffsetChildren(node, offset);
        }
        else
        {
            var offset = node.Children[0].SubtreeBounds.Left - x0;
            OffsetChildren(node, -offset);
            nodeCenterX = (node.Children[0].Center.X + node.Children[^1].Center.X) / 2;
        }

        var width = Math.Max(node.Children[^1].SubtreeBounds.Right - node.Children[0].SubtreeBounds.Left, nodeWidth);
        var height = node.Children.Select(x => x.SubtreeBounds.Height).Max() + DIFF_Y;

        node.Center = new Point(nodeCenterX, y0 + BOX_HALF_HEIGHT);
        node.SubtreeBounds = new Rect(x0, y0, width, height);
    }   

    private void OffsetChildren<T>(NaryNode<T> node, double dx)
    {
        foreach (var child in node.Children)
        {
            child.Center = child.Center with { X = child.Center.X + dx };
            child.SubtreeBounds = child.SubtreeBounds with { X = child.SubtreeBounds.X + dx };
            OffsetChildren(child, dx);
        }
    }

    private Rect GetNodeDrawBox<T>(NaryNode<T> node)
    {
        var width = CalculateWidth(node);
        return new Rect(node.Center.X - width / 2, node.Center.Y - BOX_HALF_HEIGHT, width, 2 * BOX_HALF_HEIGHT);
    }
    private double CalculateWidth<T>(NaryNode<T> node)
    {
        FormattedText formattedText = new FormattedText(
           node.Value.ToString(),
           CultureInfo.GetCultureInfo("en-us"),
           FlowDirection.LeftToRight,
           new Typeface("Veranda"),
           FONT_SIZE,
           Brushes.Black,
           PixelsPerDip);

        return formattedText.Width + 2 * TEXT_PADDING;
    }

    private void DrawNode<T>(NaryNode<T> node, Brush color)
    {
        var bounds = GetNodeDrawBox(node);
        Canvas.DrawRectangle(bounds, color, NodeStrokeColor, STROKE_THICKNESS);
        Canvas.DrawLabel(bounds, node.Value, Brushes.Transparent, Brushes.Black, HorizontalAlignment.Center, VerticalAlignment.Center, FONT_SIZE, 0);
    }

    private void DrawLink(Point p1, Point p2) => Canvas.DrawLine(p1, p2, LinkColor, STROKE_THICKNESS);
}
