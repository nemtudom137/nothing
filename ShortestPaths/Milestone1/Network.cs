using System.IO;
using System.Text;
using System.Windows;

namespace Milestone1;

internal class Network
{
    internal List<Node> Nodes { get; set; }
    internal List<Link> Links { get; set; }

    public Network()
    {
        Clear();
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
        sb.AppendLine().Append("# Nodes.").AppendLine();
        foreach (Node n in Nodes)
        {
            sb.Append(n.Center.X).Append(',').Append(n.Center.Y).Append(',').Append(n.Text).AppendLine();
        }
        sb.AppendLine().Append("# Links.").AppendLine();
        foreach (Link l in Links)
        {
            sb.Append(l.FromNode.Index).Append(',').Append(l.ToNode.Index).Append(',').Append(l.Cost).AppendLine();
        }

        return sb.ToString().TrimEnd();
    }

    internal string SaveIntoFile(string path)
    {
        string text = Serialization();
        File.WriteAllText(path, text);

        return text;
    }

    internal string? ReadNextLine(StringReader sr)
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

        if (string.IsNullOrEmpty(line))
        {
            return ReadNextLine(sr);
        }

        return line;
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

            string[] parts = line.Split(',');
            if (parts.Length != 3)
            {
                throw new ArgumentException(nameof(serialization));
            }

            Point center = new Point(ToNonNegativeInt(parts[0]), ToNonNegativeInt(parts[1]));
            string text = parts[2];
            AddNode(new Node(center, text));
        }

        for (int i = 0; i < numberOfLinks; i++)
        {
            string line = ReadNextLine(sr) ?? throw new ArgumentException(nameof(serialization));

            string[] parts = line.Split(',');
            if (parts.Length != 3)
            {
                throw new ArgumentException(nameof(serialization));
            }

            int fromIndex = ToNonNegativeInt(parts[0]);
            int toIndex = ToNonNegativeInt(parts[1]);
            int cost = ToNonNegativeInt(parts[2]);

            if (fromIndex >= numberOfNode || toIndex >= numberOfNode)
            {
                throw new ArgumentException(nameof(serialization));
            }

            _ = new Link(this, Nodes[fromIndex], Nodes[toIndex], cost);
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

    internal void ReadFromFile(string filename)
    {
        string text = File.ReadAllText(filename);
        Deserialize(text);
    }
}
