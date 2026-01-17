
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone2;

internal class Link
{
    private static Brush strokeColor = Brushes.DarkGreen;
    private static double strokeThickness = 2;
    private static Brush labelColor = Brushes.Blue;
    private static double labelFontSize = 10;
    private static double labelHight = 20;
    private static double labelWidth = 20;


    internal Network Network { get; set; }
    internal Node FromNode { get; set; }
    internal Node ToNode { get; set; }
    internal int Cost { get; set; }
    public Link(Network network, Node fromNode, Node toNode, int cost)
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
        canvas.DrawEllipse(new Rect(labelCenter.X - labelWidth / 2, labelCenter.Y - labelWidth / 2, labelWidth, labelHight), Brushes.White, Brushes.White, 0);
        canvas.DrawString(Cost.ToString(), labelWidth, labelHight, labelCenter, alpha, labelFontSize, labelColor);
    }

    internal void Draw(Canvas canvas)
    {
        canvas.DrawLine(FromNode.Center, ToNode.Center, strokeColor, strokeThickness);
    }
}