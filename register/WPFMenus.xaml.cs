using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace register
{
    public class ExitKey : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }

    public class WrapKey : ICommand
    {
        public object UIElement { get; set; }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (UIElement is TextBox textBox)
            {
               // MessageBox.Show("Wrap Shortcut key pressed");
                textBox.TextWrapping = textBox.TextWrapping == TextWrapping.NoWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
            }
            else
            {
                MessageBox.Show("Invalid UIElement.");
            }
        }
    }

    public class ExitCommandContext
    {
        public object ObjForWrapKey { get; set; }
        public ICommand ExitCommand => new ExitKey();

        public ICommand WrapCommand => new WrapKey
        {
            UIElement = ObjForWrapKey
        };
    }

    public partial class WPFMenus : Window
    {
        public WPFMenus()
        {
            InitializeComponent();
            DataContext = new ExitCommandContext
            {
                ObjForWrapKey = this.txtBox
            };
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CommandBindingNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBindingNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New menuItem Clicked!");
        }

        private void CommandBindingOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBindingOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Open MenuItem Clicked!");
        }
    }
}
