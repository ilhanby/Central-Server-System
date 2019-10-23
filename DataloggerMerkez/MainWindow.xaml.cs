using DataloggerMerkez.Models;
using DataloggerMerkez.UserControls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;


namespace DataloggerMerkez
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int column_count;
        int system_count;
        int row_count;
        int second;
        int secondrepeat;
        public ServerUserControl serverUserControl1 { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            system_count = 6;
            column_count = 4;
            second = 180;
            secondrepeat = second;
            row_count = system_count / column_count;

            if (system_count % column_count != 0)
            {
                for (int i = 0; i < row_count + 1; i++)
                {
                    var row = new RowDefinition();
                    row.Height = new GridLength(1, GridUnitType.Star);
                    gridMain.RowDefinitions.Add(row);
                }
            }
            else
            {
                for (int i = 0; i < row_count; i++)
                {
                    var row = new RowDefinition();
                    row.Height = new GridLength(1, GridUnitType.Star);
                    gridMain.RowDefinitions.Add(row);
                }
            }

            for (int i = 0; i < column_count; i++)
            {
                var column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                gridMain.ColumnDefinitions.Add(column);
            }


            for (int i = 0; i <= system_count - 1; i++)
            {
                ServerUserControl serverUserControl1 = new ServerUserControl()
                {
                    SystemName = (i + 1).ToString() + ". SİSTEM",
                };

                SystemList.Items.Add(serverUserControl1.SystemName);
                Grid.SetRow(serverUserControl1, i / column_count);
                Grid.SetColumn(serverUserControl1, (i % column_count));
                gridMain.Children.Add(serverUserControl1);
            }
            MainWindow mainWindow = new MainWindow();
            gridMainScroll.Content = gridMain;
            mainWindow.Content = gridMainScroll;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Kapatmak İstiyor Musunuz", "FAYDAM",
                MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (message == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
                WindowGrid.Margin = new Thickness(5, 5, 5, 8);
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
                WindowGrid.Margin = new Thickness(0);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void gridMainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            gridMain.Children.Clear();
            SystemList.Items.Clear();
            for (int i = 0; i <= system_count - 1; i++)
            {
                ServerUserControl serverUserControl1 = new ServerUserControl()
                {
                    SystemName = (i + 1).ToString() + ". SİSTEM",
                };

                SystemList.Items.Add(serverUserControl1.SystemName);
                Grid.SetRow(serverUserControl1, i / column_count);
                Grid.SetColumn(serverUserControl1, (i % column_count));
                gridMain.Children.Add(serverUserControl1);
            }
            second = secondrepeat;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimerLabel.Content = second + "  SANİYE";
            if (second > 0)
            {
                second--;
            }
            else if (second == 0)
            {
                Image_MouseDown(this, null);
                second = secondrepeat;
            }

        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scroll = (ScrollViewer)sender;
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            PopUp.IsOpen = true;
            PopupText.Text = string.Empty;
        }

        private void Popup_Click(object sender, RoutedEventArgs e)
        {
            if (PopupText.Text.Length == 0 || Convert.ToInt64(PopupText.Text) == 0 || Convert.ToInt64(PopupText.Text) == column_count)
            {
                PopUp.IsOpen = false;
            }
            else
            {
                gridMain.RowDefinitions.Clear();
                gridMain.ColumnDefinitions.Clear();

                if (Convert.ToInt64(PopupText.Text) > system_count)
                {
                    column_count = system_count;
                }
                else
                {
                    column_count = Convert.ToInt32(PopupText.Text);
                }

                PopUp.IsOpen = false;
                row_count = system_count / column_count;

                if (system_count % column_count != 0 || system_count == column_count)
                {
                    for (int i = 0; i < row_count + 1; i++)
                    {
                        var row = new RowDefinition();
                        row.Height = new GridLength(1, GridUnitType.Star);
                        gridMain.RowDefinitions.Add(row);
                    }
                }
                else
                {
                    for (int i = 0; i < row_count; i++)
                    {
                        var row = new RowDefinition();
                        row.Height = new GridLength(1, GridUnitType.Star);
                        gridMain.RowDefinitions.Add(row);
                    }
                }


                for (int i = 0; i < column_count; i++)
                {
                    var column = new ColumnDefinition();
                    column.Width = new GridLength(1, GridUnitType.Star);
                    gridMain.ColumnDefinitions.Add(column);
                }

                Image_MouseDown(this, null);
            }
        }

        private void PopupText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }





}
