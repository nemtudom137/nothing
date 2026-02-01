using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Milestone4;

internal class Node
{
    private const double STROKE_THICKNESS = 1;
    private const double FONT_SIZE = 10;
    private const double LARGE_RADIUS = 10;
    private const double SMALL_RADIUS = 4;
    private static Brush strokeColor = Brushes.Black;
    private static Brush fillColor = Brushes.White;
    private static Brush textColor = Brushes.Black;
    private bool isStartNode;
    private bool isEndNode;

    internal int Index { get; set; }
    internal Network Network { get; set; }
    internal Point Center { get; set; }
    internal string Text { get; set; }
    internal List<Link> Links { get; set; }
    internal Ellipse? MyEllipse { get; set; }
    internal Label? MyLabel { get; set; }
    internal bool IsStartNode
    {
        get => isStartNode;
        set
        {
            isStartNode = value;
            isEndNode = !IsStartNode && isEndNode;
            SetNodeAppearance();
        }
    }

    internal bool IsEndNode
    {
        get => isEndNode;
        set
        {
            isEndNode = value;
            isStartNode = !isEndNode && IsStartNode;
            SetNodeAppearance();
        }
    }

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
            MyLabel = canvas.DrawString(Text, 2 * LARGE_RADIUS, 2 * LARGE_RADIUS, Center, 0, FONT_SIZE, textColor);
            MyLabel.Tag = this;
            MyLabel.MouseDown += Network.label_MouseDown;
        }
        else
        {
            DrawCircle(canvas, SMALL_RADIUS);
        }
    }

    private void DrawCircle(Canvas canvas, double radius)
    {
        Point circleCenter = new Point(Center.X - radius, Center.Y - radius);
        MyEllipse = canvas.DrawEllipse(new Rect(circleCenter.X, circleCenter.Y, 2 * radius, 2 * radius), fillColor, strokeColor, STROKE_THICKNESS);
        MyEllipse.Tag = this;
        MyEllipse.MouseDown += Network.ellipse_MouseDown;
    }

    private void SetNodeAppearance()
    {
        if (MyEllipse is null)
        {
            return;
        }

        if (IsStartNode)
        {
            MyEllipse.SetShapeProperties(Brushes.Pink, Brushes.Red, 2);
        }
        else if (IsEndNode)
        {
            MyEllipse.SetShapeProperties(Brushes.LightGreen, Brushes.Green, 2);
        }
        else
        {
            MyEllipse.SetShapeProperties(fillColor, strokeColor, STROKE_THICKNESS);
        }
    }
}
