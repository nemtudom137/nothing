using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace Milestone3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{   
    public MainWindow()
    {
        InitializeComponent();
    }

    private Network displayedNetwork = new Network();

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
                // Open the result.
                displayedNetwork = new Network(dialog.FileName);
            }
    }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            displayedNetwork = new Network();
        }

        // Display the result.
        DrawNetwork();
    }

    private void DrawNetwork()
    {
        mainCanvas.Children.Clear();
        displayedNetwork.Draw(mainCanvas);
        SizeToContent = SizeToContent.WidthAndHeight;
    }

    private void MakeTestNetworksCommand_Executed(object sender, RoutedEventArgs e)
    {
        _ = BuildGridNetwork("test1", 800, 600, 6, 10);
        _ = BuildGridNetwork("test2", 800, 600, 10, 15);
    }

    private void ExitCommand_Executed(object sender, RoutedEventArgs e)
    {
        Close();
    }    
}