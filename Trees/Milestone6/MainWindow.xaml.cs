using System.Windows;
using System.Windows.Media;

namespace Milestone6;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DrawTestNaryTree(new Point(10, 10));
    }

    private void DrawTestNaryTree(Point leftUpperPoint)
    {
        NaryNode<string> node_a = new NaryNode<string>("GeneriGloop");
        NaryNode<string> node_b = new NaryNode<string>("R & D");
        NaryNode<string> node_c = new NaryNode<string>("Sales");
        NaryNode<string> node_d = new NaryNode<string>("Professional Services");
        NaryNode<string> node_e = new NaryNode<string>("HR");
        NaryNode<string> node_f = new NaryNode<string>("Accounting");
        NaryNode<string> node_g = new NaryNode<string>("Legal");

        node_a.AddChild(node_b);
        node_a.AddChild(node_c);
        node_a.AddChild(node_d);
        node_d.AddChild(node_e);
        node_d.AddChild(node_f);
        node_d.AddChild(node_g);


        foreach (var item in new string[] { "some", "thing", "something", "blabla", "blaaaablaa" })
        {
            node_b.AddChild(new NaryNode<string>(item));
            node_c.AddChild(new NaryNode<string>(item));
            node_e.AddChild(new NaryNode<string>(item));
            node_f.AddChild(new NaryNode<string>(item));
            node_g.AddChild(new NaryNode<string>(item));
        }

        // Draw the tree.
        var dpiInfo = VisualTreeHelper.GetDpi(this);
        var dip = dpiInfo.PixelsPerDip;

        node_a.ArrangeAndDrawSubtree(mainCanvas, leftUpperPoint.X, leftUpperPoint.Y, dip);
    }
}