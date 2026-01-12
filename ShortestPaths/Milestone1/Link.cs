namespace Milestone1;

internal class Link
{
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
}