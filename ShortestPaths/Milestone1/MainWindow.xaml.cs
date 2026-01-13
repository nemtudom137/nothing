using System.IO;
using System.Windows;

namespace Milestone1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        statusLabel.Content = "Press a button";
    }

    private void network1_Click(object sender, RoutedEventArgs e)
    {
        Network test1 = new Network();
        Node a = new Node(test1, new Point(20, 20), "A");
        Node b = new Node(test1, new Point(120, 120), "B");
        _ = new Link(test1, a, b, 10);

        ValidateNetwork(test1, "test1");

    }

    private void network2_Click(object sender, RoutedEventArgs e)
    {
        Network test2 = new Network();
        Node a = new Node(test2, new Point(20, 20), "A");
        Node b = new Node(test2, new Point(120, 20), "B");
        Node c = new Node(test2, new Point(20, 120), "C");
        Node d = new Node(test2, new Point(120, 120), "D");

        _ = new Link(test2, test2.Nodes[0], test2.Nodes[1], 10);
        _ = new Link(test2, test2.Nodes[1], test2.Nodes[3], 15);
        _ = new Link(test2, test2.Nodes[0], test2.Nodes[2], 20);
        _ = new Link(test2, test2.Nodes[2], test2.Nodes[3], 25);

        ValidateNetwork(test2, "test3");
    }
    private void network3_Click(object sender, RoutedEventArgs e)
    {
        Network test3 = new Network();
        Node a = new Node(test3, new Point(20, 20), "A");
        Node b = new Node(test3, new Point(120, 20), "B");
        Node c = new Node(test3, new Point(20, 120), "C");
        Node d = new Node(test3, new Point(120, 120), "D");

        _ = new Link(test3, test3.Nodes[0], test3.Nodes[1], 10);
        _ = new Link(test3, test3.Nodes[1], test3.Nodes[3], 15);
        _ = new Link(test3, test3.Nodes[0], test3.Nodes[2], 20);
        _ = new Link(test3, test3.Nodes[2], test3.Nodes[3], 25);
        _ = new Link(test3, test3.Nodes[1], test3.Nodes[0], 11);
        _ = new Link(test3, test3.Nodes[3], test3.Nodes[1], 16);
        _ = new Link(test3, test3.Nodes[2], test3.Nodes[0], 21);
        _ = new Link(test3, test3.Nodes[3], test3.Nodes[2], 26);

        ValidateNetwork(test3, "test3");
    }

    private void ValidateNetwork(Network network, string fileName)
    {
        string path = GetPath(fileName);

        string serialisation1 = network.SaveIntoFile(path);
        network.ReadFromFile(path);
        string serialisation2 = network.Serialization();
        nodeText.Text = serialisation2;
        statusLabel.Content = serialisation1 == serialisation2 ? "OK" : "Serializations do not match";
    }



    private static string GetPath(string fileName) => Path.Combine(Environment.CurrentDirectory, $"{fileName}.net");
}