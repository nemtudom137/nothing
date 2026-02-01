using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Milestone4;

internal class Network
{
    private const double MARGIN = 20;
    private const int BIG_NODE_LIMIT = 100;
    private const char SEPARATOR = ';';
    internal List<Node> Nodes { get; set; }
    internal List<Link> Links { get; set; }
    internal Node? StartNode { get; set; }
    internal Node? EndNode { get; set; }

    internal Network()
    {
        Clear();
    }

    internal Network(string filename)
    {
        ReadFromFile(filename);
    }

    private void Clear()
    {
        Nodes = new List<Node>();
        Links = new List<Link>();
    }

    internal void AddNode(Node node)
    {
        ArgumentNullException.ThrowIfNull(node);

        node.Index = Nodes.Count;
        Nodes.Add(node);
    }

    internal void AddLink(Link link) => Links.Add(link ?? throw new ArgumentNullException(nameof(link)));

    internal string Serialization()
    {
        var sb = new StringBuilder();
        sb.Append(Nodes.Count).Append("# Num nodes.").AppendLine();
        sb.Append(Links.Count).Append("# Num links.").AppendLine();

        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";

        sb.AppendLine().Append("# Nodes.").AppendLine();
        foreach (Node n in Nodes)
        {
            sb.Append(n.Center.X).Append(SEPARATOR).Append(n.Center.Y).Append(SEPARATOR).Append(n.Text).AppendLine();
        }

        sb.AppendLine().Append("# Links.").AppendLine();
        foreach (Link l in Links)
        {
            sb.Append(l.FromNode.Index).Append(SEPARATOR).Append(l.ToNode.Index).Append(SEPARATOR).Append(l.Cost).AppendLine();
        }

        return sb.ToString().TrimEnd();
    }

    internal string SaveIntoFile(string path)
    {
        string text = Serialization();
        File.WriteAllText(path, text);

        return text;
    }

    internal void Deserialize(string serialization)
    {
        if (string.IsNullOrEmpty(serialization))
        {
            throw new ArgumentException(nameof(serialization));
        }

        Clear();

        using var sr = new StringReader(serialization);

        int numberOfNode = ToNonNegativeInt(ReadNextLine(sr));
        int numberOfLinks = ToNonNegativeInt(ReadNextLine(sr));

        for (int i = 0; i < numberOfNode; i++)
        {
            string line = ReadNextLine(sr) ?? throw new ArgumentException(nameof(serialization));

            string[] parts = line.Split(SEPARATOR);
            if (parts.Length != 3)
            {
                throw new ArgumentException(nameof(serialization));
            }

            Point center = new Point(ToNonNegativeDouble(parts[0]), ToNonNegativeDouble(parts[1]));
            string text = parts[2];
            _ = new Node(this, center, text);
        }

        for (int i = 0; i < numberOfLinks; i++)
        {
            string line = ReadNextLine(sr) ?? throw new ArgumentException(nameof(serialization));

            string[] parts = line.Split(SEPARATOR);
            if (parts.Length != 3)
            {
                throw new ArgumentException(nameof(serialization));
            }

            int fromIndex = ToNonNegativeInt(parts[0]);
            int toIndex = ToNonNegativeInt(parts[1]);
            double cost = ToNonNegativeDouble(parts[2]);

            if (fromIndex >= numberOfNode || toIndex >= numberOfNode)
            {
                throw new ArgumentException(nameof(serialization));
            }

            _ = new Link(this, Nodes[fromIndex], Nodes[toIndex], cost);
        }
    }

    internal static string? ReadNextLine(StringReader sr)
    {
        while (true)
        {
            string? line = sr.ReadLine();

            if (line is null)
            {
                return null;
            }

            int index = line.IndexOf('#');
            if (index >= 0)
            {
                line = line[..index];
            }

            line = line.Trim();

            if (!string.IsNullOrEmpty(line))
            {
                return line;
            }
        }
    }

    private static int ToNonNegativeInt(string? s)
    {
        if (s is null || !int.TryParse(s, out int value) || value < 0)
        {
            throw new ArgumentException(nameof(s));
        }

        return value;
    }

    private static double ToNonNegativeDouble(string? s)
    {
        if (s is null || !double.TryParse(s, out double value) || value < 0)
        {
            throw new ArgumentException(nameof(s));
        }

        return value;
    }

    internal void ReadFromFile(string filename) => Deserialize(File.ReadAllText(filename));

    internal void Draw(Canvas canvas)
    {
        Rect bounds = GetBounds();
        canvas.Width = bounds.Right + 2 * MARGIN;
        canvas.Height = bounds.Bottom + 2 * MARGIN;

        bool drawLabels = Nodes.Count <= BIG_NODE_LIMIT;

        foreach (var link in Links)
        {
            link.Draw(canvas);
        }

        if (drawLabels)
        {
            foreach (var link in Links)
            {
                link.DrawLabel(canvas);
            }
        }

        foreach (var node in Nodes)
        {
            node.Draw(canvas, drawLabels);
        }
    }

    internal Rect GetBounds()
    {
        double minX = double.PositiveInfinity;
        double maxX = double.NegativeInfinity;
        double minY = double.PositiveInfinity;
        double maxY = double.NegativeInfinity;
        foreach (var center in Nodes.Select(x => x.Center))
        {
            if (center.X < minX)
            {
                minX = center.X;
            }

            if (center.X > maxX)
            {
                maxX = center.X;
            }

            if (center.Y < minY)
            {
                minY = center.Y;
            }

            if (center.Y > maxY)
            {
                maxY = center.Y;
            }
        }

        double width = Math.Max(maxX - minX, 1);
        double height = Math.Max(maxY - minY, 1);
        double x = Math.Min(minX, 0);
        double y = Math.Min(minY, 0);
        return new Rect(x, y, width, height);
    }

    internal void ellipse_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Node? node = (sender as Ellipse)?.Tag as Node;

        if (node is not null)
        {
            NodeClicked(node, e);
        }
    }

    internal void label_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Node? node = (sender as Label)?.Tag as Node;

        if (node is not null)
        {
            NodeClicked(node, e);
        }
    }

    private void NodeClicked(Node node, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            if (StartNode != null)
            {
                SetNode(StartNode, l => l.IsInTree = false, n => n.IsStartNode = false);
            }

            SetNode(node, l => l.IsInTree = true, n => n.IsStartNode = true);
            StartNode = node;
        }

        if (e.ChangedButton == MouseButton.Right)
        {
            if (EndNode != null)
            {
                SetNode(EndNode, l => l.IsInPath = false, n => n.IsEndNode = false);
            }

            SetNode(node, l => l.IsInPath = true, n => n.IsEndNode = true);
            EndNode = node;
        }
    }

    private static void SetNode(Node node, Action<Link> setLink, Action<Node> setFlag)
    {
        foreach (var link in node.Links)
        {
            setLink(link);
        }

        setFlag(node);
    }
}
