using System.IO;
using System.Windows;

namespace Milestone3;

public partial class MainWindow : Window
{
    private const double MARGIN = 40;

    private static Network BuildGridNetwork(string filename, double width, double height, int numRows, int numCols)
    {
        ArgumentException.ThrowIfNullOrEmpty(filename);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 4 * MARGIN);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 4 * MARGIN);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(numRows, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(numCols, 0);

        Network result = new Network();

        double x = MARGIN;
        double y = MARGIN;
        double dx = (width - 2 * MARGIN) / (numCols - 1);
        double dy = (height - 2 * MARGIN) / (numRows - 1);

        //nodes
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                _ = new Node(result, new Point(x, y), $"{i * numCols + j + 1}");
                x += dx;
            }

            x = MARGIN;
            y += dy;
        }

        //links in the first row
        for (int i = 1; i < numCols; i++)
        {
            MakeRandomizedLinks(result, result.Nodes[i], result.Nodes[i - 1]);
        }

        //other links
        for (int i = 1; i < numRows; i++)
        {
            MakeRandomizedLinks(result, result.Nodes[i * numCols], result.Nodes[(i - 1) * numCols]);

            for (int j = 1; j < numCols; j++)
            {
                int current = i * numCols + j;
                MakeRandomizedLinks(result, result.Nodes[current], result.Nodes[current - 1]);
                MakeRandomizedLinks(result, result.Nodes[current], result.Nodes[current - numCols]);

            }
        }

        result.SaveIntoFile(GetPath(filename));
        return result;
    }

    private static void MakeRandomizedLinks(Network network, Node n1, Node n2)
    {
        Random rnd = new Random();
        double lenght = Distance(n1.Center, n2.Center);
        double x = 1 + 0.2 * rnd.NextDouble();
        _ = new Link(network, n1, n2, Math.Round(lenght * (1 + 0.2 * rnd.NextDouble()), 0));
        _ = new Link(network, n2, n1, Math.Round(lenght * (1 + 0.2 * rnd.NextDouble()), 0));

    }

    private static double Distance(Point p1, Point p2)
    {
        return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }

    private static string GetPath(string fileName) => Path.Combine(Environment.CurrentDirectory, $"{fileName}.net");
}
