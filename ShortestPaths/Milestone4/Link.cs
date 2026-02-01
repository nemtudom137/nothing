using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Milestone4;

internal class Link
{
    private static Brush strokeColor = Brushes.DarkGreen;
    private static Brush labelColor = Brushes.Blue;
    private const double STROKE_THICKNESS = 2;
    private const double LABEL_FONT_SIZE = 10;
    private const double LABEL_SIZE = 20;
    private bool isInTree;
    private bool isInPath;

    internal Network Network { get; set; }
    internal Node FromNode { get; set; }
    internal Node ToNode { get; set; }
    internal double Cost { get; set; }
    internal Line? MyLine { get; set; }
    internal bool IsInTree
    {
        get => isInTree;
        set
        {
            isInTree = value;
            IsInPath = !isInTree && IsInPath;
            SetLinkAppearance();
        }
    }

    internal bool IsInPath
    {
        get => isInPath;
        set
        {
            isInPath = value;
            isInTree = !isInPath && isInTree;
            SetLinkAppearance();
        }
    }

    public Link(Network network, Node fromNode, Node toNode, double cost)
    {
        Network = network;
        FromNode = fromNode;
        ToNode = toNode;
        Cost = cost;
        Network.AddLink(this);
        FromNode.AddLink(this);
    }

    public override string ToString() => $"{FromNode} --> {ToNode} ({Cost})";

    internal void DrawLabel(Canvas canvas)
    {
        double alpha = Math.Atan2(ToNode.Center.X - FromNode.Center.X, ToNode.Center.Y - FromNode.Center.Y) * 180 / Math.PI - 90;
        Point labelCenter = new Point((2 * FromNode.Center.X + ToNode.Center.X) / 3, (2 * FromNode.Center.Y + ToNode.Center.Y) / 3);
        canvas.DrawEllipse(new Rect(labelCenter.X - LABEL_SIZE / 2, labelCenter.Y - LABEL_SIZE / 2, LABEL_SIZE, LABEL_SIZE), Brushes.White, null!, 0);
        canvas.DrawString(Cost.ToString(), LABEL_SIZE, LABEL_SIZE, labelCenter, alpha, LABEL_FONT_SIZE, labelColor);
    }

    internal void Draw(Canvas canvas)
    {
        MyLine = canvas.DrawLine(FromNode.Center, ToNode.Center, strokeColor, STROKE_THICKNESS);
    }

    private void SetLinkAppearance()
    {
        if (MyLine is null)
        {
            return;
        }

        if (IsInPath)
        {
            MyLine.SetShapeProperties(Brushes.Red, 6);
        }
        else if (IsInTree)
        {
            MyLine.SetShapeProperties(Brushes.Lime, 6);
        }
        else
        {
            MyLine.SetShapeProperties(strokeColor, STROKE_THICKNESS);
        }
    }
}