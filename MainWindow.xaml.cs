using MMOPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
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

namespace MMOClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IMMOPortalController m_Portal;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { 
            ChannelFactory<IMMOPortalController> IMMOFactory;

            var netTcpBinding = new NetTcpBinding();
            string URL = "net.tcp://localhost:50002/MMOPortal";
            netTcpBinding.MaxReceivedMessageSize = Int32.MaxValue;
            netTcpBinding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
            IMMOFactory = new ChannelFactory<IMMOPortalController>(netTcpBinding, URL);

            m_Portal = IMMOFactory.CreateChannel();
            m_Portal.InitDB();
            //MessageBox.Show(m_Portal.GetNumBosses().ToString());
            listServer.Items.Add("50002");
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Check wether server is running");
            }
            catch (ChannelTerminatedException)
            {
                MessageBox.Show("Please Re-run the application");
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Check wether server is running");
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Timeout! Re-run the application");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}
