using System.Windows;

namespace Milestone7;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly SortedBinaryNode<int> Sentinel;
    public MainWindow()
    {
        InitializeComponent();
        Sentinel = new SortedBinaryNode<int>(-1);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        BuildTestTree();
        DrawTree();
    }

    private void addButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(ValueTextBox.Text, out int value))
        {
            try
            {
                Sentinel.AddNode(value);
                ReDrawTree();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Value is already present.");
            }
        }
    }

    private void findButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(ValueTextBox.Text, out int value))
        {
            SortedBinaryNode<int>? found = Sentinel.FindNode(value);
            MessageBox.Show($"{value}: {(found is null ? "not found" : "found")}");
        }
    }

    private void removeButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(ValueTextBox.Text, out int value))
        {
            if(value == -1)
            {
                MessageBox.Show("Value cannot be removed.");
            }

            Sentinel.RemoveNode(value);
            ReDrawTree();
        }
    }

    private void resetButton_Click(object sender, RoutedEventArgs e)
    {
        BuildTestTree();
        ReDrawTree();
    }

    private void BuildTestTree()
    {
        Sentinel.DeleteAllChildren();        
        var elements = new int[] { 60, 35, 76, 21, 42, 71, 89, 17, 24, 74, 11, 23, 72, 75 };

        foreach (var i in elements)
        {
            Sentinel.AddNode(i);
        }
    }

    private void DrawTree() => Sentinel.ArrangeAndDrawSubtree(mainCanvas, 10, 10);

    private void ReDrawTree()
    {
        mainCanvas.Children.Clear();
        DrawTree();
    }
}