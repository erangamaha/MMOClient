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
using System.Windows.Shapes;

namespace MMOClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        IMMOPortalController m_Portal;

        public Login()
        {
            try
            {
                InitializeComponent();

                ChannelFactory<IMMOPortalController> IMMOFactory;

                var netTcpBinding = new NetTcpBinding();
                string URL = "net.tcp://localhost:50002/MMOPortal";
                netTcpBinding.MaxReceivedMessageSize = Int32.MaxValue;
                netTcpBinding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                IMMOFactory = new ChannelFactory<IMMOPortalController>(netTcpBinding, URL);

                m_Portal = IMMOFactory.CreateChannel();
                m_Portal.InitDB();
            }
            //catch(Exception LoginFualt)
            //{
            //    Close();
            //    MessageBox.Show("Check wether server is running");
            //}
            catch (EndpointNotFoundException)
            {
                //Close();
                MessageBox.Show("Check wether server is running");
            }
            catch (ChannelTerminatedException)
            {
                Close();
                MessageBox.Show("Please Re-run the application");
            }
            catch (CommunicationException)
            {
                //Close();
                MessageBox.Show("Check wether server is running");
            }
            catch (TimeoutException)
            {
                Close();
                MessageBox.Show("Timeout! Re-run the application");
            }
        }

        private void btnCloase_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m_Portal.UserChecking(txtBoxUsername.Text, pwdBoxPwd.Password))
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password");
                    txtBoxUsername.Clear();
                    pwdBoxPwd.Clear();
                }
            }
            catch (CommunicationException)
            {
                MessageBox.Show("Check wether server is running");
                txtBoxUsername.Clear();
                pwdBoxPwd.Clear();
                Close();
            }
        }
    }
}
