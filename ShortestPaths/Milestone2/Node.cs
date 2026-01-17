using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone2;

internal class Node
{
    private static Brush strokeColor = Brushes.Black;
    private static Brush backgroundColor = Brushes.White;
    private static Brush textColor = Brushes.Black;
    private static double strokeThickness = 2;
    private static double fontSize = 10;
    private static double nodeRadius = 15;

    internal int Index { get; set; }
    internal Network Network { get; set; }
    internal Point Center { get; set; }
    internal string Text { get; set; }
    internal List<Link> Links { get; set; }

    public Node(Network network, Point center, string text)
    {
        Network = network;
        Center = center;
        Text = text;
        Index = -1;
        Links = [];
        Network.AddNode(this);
    }

    public override string ToString() => $"[{Text}]";

    internal void AddLink(Link link) => Links.Add(link ?? throw new ArgumentNullException(nameof(link)));

    internal void Draw(Canvas canvas)
    {
        Point circleCenter = new Point(Center.X - nodeRadius, Center.Y - nodeRadius);
        canvas.DrawEllipse(new Rect(circleCenter.X, circleCenter.Y, 2 * nodeRadius, 2 * nodeRadius), backgroundColor, strokeColor, strokeThickness);
        canvas.DrawString(Text, nodeRadius, nodeRadius, Center, 0, fontSize, textColor);
    }
}
