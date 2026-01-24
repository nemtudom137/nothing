using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Milestone3;

internal class Node
{
    private const double STROKE_THICKNESS = 2;
    private const double FONT_SIZE = 10;
    private const double LARGE_RADIUS = 10;
    private const double SMALL_RADIUS = 3;
    private static Brush strokeColor = Brushes.Black;
    private static Brush backgroundColor = Brushes.White;
    private static Brush textColor = Brushes.Black;

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

    internal void Draw(Canvas canvas, bool drawLabels)
    {
        if (drawLabels)
        {
            DrawCircle(canvas, LARGE_RADIUS);
            canvas.DrawString(Text, 2 * LARGE_RADIUS, 2 * LARGE_RADIUS, Center, 0, FONT_SIZE, textColor);
        }
        else
        {
            DrawCircle(canvas, SMALL_RADIUS);
        }
    }

    private void DrawCircle(Canvas canvas, double radius)
    {
        Point circleCenter = new Point(Center.X - radius, Center.Y - radius);
        canvas.DrawEllipse(new Rect(circleCenter.X, circleCenter.Y, 2 * radius, 2 * radius), backgroundColor, strokeColor, STROKE_THICKNESS);
    }
}
