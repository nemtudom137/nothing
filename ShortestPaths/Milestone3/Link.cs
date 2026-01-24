using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone3;

internal class Link
{
    private static Brush strokeColor = Brushes.DarkGreen;
    private static Brush labelColor = Brushes.Blue;
    private const double STROKE_THICKNESS = 2;
    private const double LABEL_FONT_SIZE = 10;
    private const double LABEL_SIZE = 20;

    internal Network Network { get; set; }
    internal Node FromNode { get; set; }
    internal Node ToNode { get; set; }
    internal double Cost { get; set; }
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
        canvas.DrawLine(FromNode.Center, ToNode.Center, strokeColor, STROKE_THICKNESS);
    }
}