using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Milestone5
{
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
            var bottomRightPoint1 = DrawTestBinaryTree(new Point(10, 10));
            var _ = DrawTestNaryTree(new Point(10, bottomRightPoint1.Y + 10));
        }

        private Point DrawTestBinaryTree(Point leftUpperPoint)
        {
            // Build a test tree.
            //         A
            //        / \
            //       /   \
            //      /     \
            //     B       C
            //    / \     / \
            //   D   E   F   G
            //      / \     /
            //     H   I   J
            //            / \
            //           K   L
            BinaryNode<string> node_a = new BinaryNode<string>("A");
            BinaryNode<string> node_b = new BinaryNode<string>("B");
            BinaryNode<string> node_c = new BinaryNode<string>("C");
            BinaryNode<string> node_d = new BinaryNode<string>("D");
            BinaryNode<string> node_e = new BinaryNode<string>("E");
            BinaryNode<string> node_f = new BinaryNode<string>("F");
            BinaryNode<string> node_g = new BinaryNode<string>("G");
            BinaryNode<string> node_h = new BinaryNode<string>("H");
            BinaryNode<string> node_i = new BinaryNode<string>("I");
            BinaryNode<string> node_j = new BinaryNode<string>("J");
            BinaryNode<string> node_k = new BinaryNode<string>("K");
            BinaryNode<string> node_l = new BinaryNode<string>("L");

            node_a.AddLeft(node_b);
            node_a.AddRight(node_c);
            node_b.AddLeft(node_d);
            node_b.AddRight(node_e);
            node_c.AddLeft(node_f);
            node_c.AddRight(node_g);
            node_e.AddLeft(node_h);
            node_e.AddRight(node_i);
            node_g.AddLeft(node_j);
            node_j.AddLeft(node_k);
            node_j.AddRight(node_l);

            // Arrange and draw the tree.

            node_a.ArrangeAndDrawSubtree(mainCanvas, leftUpperPoint.X, leftUpperPoint.Y);
            return node_a.SubtreeBounds.BottomRight;
        }

        private Point DrawTestNaryTree(Point leftUpperPoint)
        {
            // Build a test tree.
            // A
            //         |
            //     +---+---+
            // B   C   D
            //     |       |
            //    +-+      +
            // E F      G
            //    |        |
            //    +      +-+-+
            // H      I J K
            NaryNode<string> node_a = new NaryNode<string>("A");
            NaryNode<string> node_b = new NaryNode<string>("B");
            NaryNode<string> node_c = new NaryNode<string>("C");
            NaryNode<string> node_d = new NaryNode<string>("D");
            NaryNode<string> node_e = new NaryNode<string>("E");
            NaryNode<string> node_f = new NaryNode<string>("F");
            NaryNode<string> node_g = new NaryNode<string>("G");
            NaryNode<string> node_h = new NaryNode<string>("H");
            NaryNode<string> node_i = new NaryNode<string>("I");
            NaryNode<string> node_j = new NaryNode<string>("J");
            NaryNode<string> node_k = new NaryNode<string>("K");

            node_a.AddChild(node_b);
            node_a.AddChild(node_c);
            node_a.AddChild(node_d);
            node_b.AddChild(node_e);
            node_b.AddChild(node_f);
            node_d.AddChild(node_g);
            node_e.AddChild(node_h);
            node_g.AddChild(node_i);
            node_g.AddChild(node_j);
            node_g.AddChild(node_k);

            // Draw the tree.

            node_a.ArrangeAndDrawSubtree(mainCanvas, leftUpperPoint.X, leftUpperPoint.Y);
            return node_a.SubtreeBounds.BottomRight;
        }
    }
}