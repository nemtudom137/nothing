using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Milestone2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private Network MyNetwork = new Network();

    private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = true;
    }

    private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        try
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".net";
            dialog.Filter = "Network Files|*.net|All Files|*.*";

            // Display the dialog.
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                // Open the network.

                }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            MyNetwork = new Network();
        }

        // Display the network.
        DrawNetwork();
    }

    private void DrawNetwork()
    {
        // Remove any previous drawing.

            // Make the network draw itself.
        }

    private void ExitCommand_Executed(object sender, RoutedEventArgs e)
    {
        Close();
    }
}