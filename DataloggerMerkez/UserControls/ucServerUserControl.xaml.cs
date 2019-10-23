using DataloggerMerkez.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
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
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;



namespace DataloggerMerkez.UserControls
{
    /// <summary>
    /// Interaction logic for ServerUserControl.xaml
    /// </summary>
    public partial class ServerUserControl : UserControl
    {

        public string SystemName;
        public bool IsServerActive;
        public bool IsSmsActive;
        public bool IsCallActive;
        public int OfflineGatewayCount;
        public int OnlineGatewayCount;
        public int TotalGatewayCount;
        public double TotalChart { get; set; }
        public double OnlineChart { get; set; }
        public List<Gateway> GatewayList;
        public string GatewayName;
        public bool GatewayIsActive;
        public int GatewayActiveCount;
        public int GatewayPassiveCount;



        public ServerUserControl()
        {
            InitializeComponent();
            GatewayList = new List<Gateway>();
            GatewayList.Add(new Gateway() { GatewayName = "ADIYAMAN BKM", GatewayActiveCount = 13, GatewayPassiveCount = 0 });
            GatewayList.Add(new Gateway() { GatewayName = "BURSA BKM", GatewayActiveCount = 8, GatewayPassiveCount = 0 });
            GatewayList.Add(new Gateway() { GatewayName = "ESKİŞEHİR BKM", GatewayActiveCount = 11, GatewayPassiveCount = 0 });
            GatewayList.Add(new Gateway() { GatewayName = "ANKARA BKM", GatewayActiveCount = 12, GatewayPassiveCount = 0 });
            GatewayList.Add(new Gateway() { GatewayName = "BALIKESİR BKM", GatewayActiveCount = 22, GatewayPassiveCount = 4 });
            GatewayList.Add(new Gateway() { GatewayName = "KIRŞEHİR BKM", GatewayActiveCount = 14, GatewayPassiveCount = 2 });
            GatewayList.Add(new Gateway() { GatewayName = "ESKİŞEHİR BKM", GatewayActiveCount = 5, GatewayPassiveCount = 0 });
            GatewayList.Add(new Gateway() { GatewayName = "ESKİŞEHİR BKM", GatewayActiveCount = 9, GatewayPassiveCount = 0 });
            GatewayList.Add(new Gateway() { GatewayName = "ESKİŞEHİR BKM", GatewayActiveCount = 19, GatewayPassiveCount = 0 });

            IsServerActive = true;
            IsSmsActive = false;
            IsCallActive = false;

            for (int i = 0; i < GatewayList.Count; i++)     //Listenin elemanlarının aldığı pasif değere göre Renk değiştirmesi
            {
                if (GatewayList[i].GatewayPassiveCount >= 1) 
                {
                    GatewayList[i].GatewayIsActive = false;
                    OfflineGatewayCount++;
                }
                else { GatewayList[i].GatewayIsActive = true; }
            }

            TotalGatewayCount = GatewayList.Count;         
            OnlineGatewayCount = TotalGatewayCount - OfflineGatewayCount;
            TotalChart = (double)TotalGatewayCount / 100;
            TotalChart = Math.Round(TotalChart, 2);       
            OnlineChart = OnlineGatewayCount / TotalChart;
            OnlineChart = Math.Round(OnlineChart, 2);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            systemnamelabel.Content = this.SystemName;
            onlinegatewaycountlabel.Content = this.OnlineGatewayCount;
            offlinegatewaycountlabel.Content = this.OfflineGatewayCount;
            livechart.Value = this.OnlineChart;
            ListView1.ItemsSource = this.GatewayList;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListView1.ItemsSource); //Liste Sıralaması 
            view.SortDescriptions.Add(new SortDescription("GatewayIsActive", ListSortDirection.Ascending)); // Kırmızı renkli olanlar en üstte gözükmesi için
            view.SortDescriptions.Add(new SortDescription("GatewayName", ListSortDirection.Ascending));     // Ondan sonra Alfabetik sıra

            if (livechart.Value < 100)       // Chartın Pasif (Kırmızı) yüzdelik deger göstermesi
            {
                livechart.GaugeBackground = Brushes.Red;
            }

            if (IsServerActive == true)
            {
                txtServer.Background = Brushes.Lime;
                txtServerText.Foreground = Brushes.MidnightBlue;
            }
            else
            {
                txtServer.Background = Brushes.Red;
                txtServerText.Foreground = Brushes.Navy;
            }

            if (IsSmsActive == true)
            {
                txtSms.Background = Brushes.Lime;
                txtSmsText.Foreground = Brushes.MidnightBlue;
            }
            else
            {
                txtSms.Background = Brushes.Red;
                txtSmsText.Foreground = Brushes.Navy;
            }

            if (IsCallActive == true)
            {
                txtCall.Background = Brushes.Lime;
                txtCallText.Foreground = Brushes.MidnightBlue;
            }
            else
            {
                txtCall.Background = Brushes.Red;
                txtCallText.Foreground = Brushes.Navy;
            }


        }

        private void ListView1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listview = sender as ListView;         // width="*"
            GridView gridview = listview.View as GridView;
            var actualwidth = listview.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            for (int i = 1; i < gridview.Columns.Count; i++)
            {
                actualwidth = actualwidth - gridview.Columns[i].ActualWidth;
            }
            gridview.Columns[0].Width = actualwidth;


        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scroll = (ScrollViewer)sender;                
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
            e.Handled = true;
        }

    }

}







