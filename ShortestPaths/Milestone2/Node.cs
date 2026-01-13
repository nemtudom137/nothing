using System.Windows;

namespace Milestone2;

internal class Node
{
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
}
