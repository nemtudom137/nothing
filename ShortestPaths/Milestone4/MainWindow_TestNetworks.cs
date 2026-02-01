using System.IO;
using System.Windows;

namespace Milestone4;

public partial class MainWindow : Window
{
    private const double MARGIN = 20;
    private readonly Random rnd = new Random();

    private Network BuildGridNetwork(string filename, double width, double height, int numRows, int numCols)
    {
        ArgumentException.ThrowIfNullOrEmpty(filename);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 4 * MARGIN);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 4 * MARGIN);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(numRows, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(numCols, 0);

        Network network = new Network();

        double dx = numCols == 1 ? 0 : (width - 2 * MARGIN) / (numCols - 1);
        double dy = numRows == 1 ? 0 : (height - 2 * MARGIN) / (numRows - 1);
        Node[,] nodes = new Node[numRows, numCols];

        //nodes
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                double x = MARGIN + j * dx;
                double y = MARGIN + i * dy;
                nodes[i, j] = new Node(network, new Point(x, y), $"{i * numCols + j + 1}");                
            }
        }

        // horizontal links
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols - 1; j++)
            {
                MakeRandomizedLinks(network, nodes[i, j], nodes[i, j + 1]);
            }
        }

        // vertical links
        for (int i = 0; i < numRows - 1; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                MakeRandomizedLinks(network, nodes[i, j], nodes[i + 1, j]);
            }
        }

        network.SaveIntoFile(GetPath(filename));
        return network;
    }

    private void MakeRandomizedLinks(Network network, Node n1, Node n2)
    {
        double lenght = Distance(n1.Center, n2.Center);
        double cost1 = Math.Round(lenght * (1 + 0.2 * rnd.NextDouble()));
        double cost2 = Math.Round(lenght * (1 + 0.2 * rnd.NextDouble()));
        _ = new Link(network, n1, n2, cost1);
        _ = new Link(network, n2, n1, cost2);

    }

    private static double Distance(Point p1, Point p2)
    {
        return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }

    private static string GetPath(string fileName) => Path.Combine(Environment.CurrentDirectory, $"{fileName}.net");
}
