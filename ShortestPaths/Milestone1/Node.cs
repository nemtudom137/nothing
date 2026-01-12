using System.Windows;

namespace Milestone1;

internal class Node
{
    internal int Index { get; set; }
    internal Network Network { get; set; }
    internal Point Center { get; set; }
    internal string Text { get; set; }
    internal List<Link> Links { get; set; }

    public Node(Point center, string text)
    {
        Index = -1;
        Network = new Network();
        Center = center;
        Text = text;
        Links = [];        
    }

    public override string ToString() => $"[{Text}]";

    internal void AddLink(Link link) => Links.Add(link ?? throw new ArgumentNullException(nameof(link)));
}
