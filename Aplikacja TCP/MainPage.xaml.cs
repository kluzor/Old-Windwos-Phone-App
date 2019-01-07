using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_TCP
{
    public sealed partial class MainPage : Page
    {
        public StreamSocket clientSocket;
        public HostName serverHost;
        public DataReader reader;
        public bool connected = false;

        public int L1;
        public int R1;
        public int L2;
        public int R2;
        public int L3;
        public int R3;
        public int L4;
        public int R4;
        public int L5;
        public int R5;
    
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// CONNECT TO SERVER
        /// </summary>
        /// <param name="serverHost">Host name/IP address</param>
        /// <param name="serverPort">Port number</param>
        /// <returns>Response from server</returns>
        /// 
        public async void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (connected)
            {
                OutputView.Text = "Already connected";
            }
            else
            {
                try
                {
                    OutputView.Text = "Trying to connect ...";
                    clientSocket = new StreamSocket();
                    clientSocket.Control.NoDelay = true;
                    serverHost = new HostName(ServerHostname.Text);
                    await clientSocket.ConnectAsync(serverHost, ServerPort.Text);
                    connected = true;
                    OutputView.Text = "Connection established";
                    //await this.read();
                }
                catch (Exception exception)
                {

                    if (SocketError.GetStatus(exception.HResult) == SocketErrorStatus.Unknown)
                    {
                        throw;
                    }
                    OutputView.Text = "Connect failed with error: " + exception.Message;
                    clientSocket.Dispose();
                    clientSocket = null;
                    connected = false;
                }
            }
        }

        /// <summary>
        /// SEND DATA
        /// </summary>
        /// <param name="message">Message to server</param>
        /// <returns>void</returns>
        /// 
        public async Task send(string message)
        {
            if (!connected)
            {
                OutputView.Text = "Must be connected to send!";
                return;
            }
            else
            {
                DataWriter writer;

                using (writer = new DataWriter(clientSocket.OutputStream))
                {
                    writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    writer.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;
                    writer.WriteString(message);
                    try
                    {
                        await writer.StoreAsync();
                        OutputView.Text = "Data was sent: " + message ;
                    }
                    catch (Exception exception)
                    {
                        switch (SocketError.GetStatus(exception.HResult))
                        {
                            case SocketErrorStatus.HostNotFound:
                                throw;
                            default:
                                throw;
                        }
                    }
                    await writer.FlushAsync();
                    writer.DetachStream();
                }
            }
        }

        /// <summary>
        /// READ RESPONSE
        /// </summary>
        /// <returns>Response from server</returns>
        /// 
        public async Task <String> read()
        {
            StringBuilder strBuilder;

            using (reader = new DataReader(clientSocket.InputStream))
            {
                strBuilder = new StringBuilder();
                reader.InputStreamOptions = Windows.Storage.Streams.InputStreamOptions.Partial;
                reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                reader.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;
                await reader.LoadAsync(1024);
                while (reader.UnconsumedBufferLength > 0)
                {
                    strBuilder.Append(reader.ReadString(reader.UnconsumedBufferLength));
                    await reader.LoadAsync(1024);
                }
                reader.DetachStream();
                OutputView.Text = "Receive data: " + strBuilder.ToString();

                //To analyze:
                //connected = false;
                //string host = ServerHostname.Text;
                //string port = ServerPort.Text;
                //await connect(host, port);

                return strBuilder.ToString();         
            }
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            string sendText = SendText.Text;
            await send(sendText);
        }

        private void SendText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ServerHostname_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ServerPort_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OutputView_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void button_Auto_Click(object sender, RoutedEventArgs e)
        {
            string sendText = "A";
            await send(sendText);
        }

        private async void button_Up_Click(object sender, RoutedEventArgs e)
        {
            string sendText = "!";
            await send(sendText);
        }

        private async void button_Down_Click(object sender, RoutedEventArgs e)
        {
            string sendText = "#";
            await send(sendText);
        }

        private async void button_Left_Click(object sender, RoutedEventArgs e)
        {
            string sendText = "$";
            await send(sendText);
        }

        private async void button_Right_Click(object sender, RoutedEventArgs e)
        {
            string sendText = "%";
            await send(sendText);
        }

        private async void button_Start_Click(object sender, RoutedEventArgs e)
        {
            string sendText = "&";
            await send(sendText);
        }

        private async void button_L1_Click(object sender, RoutedEventArgs e)
        {
            L1 = 0;
            R1 = 1;
            while (L1 == 0)
            {
                string sendText = "(";
                await send(sendText);
                await Task.Delay(20);
            }
        }

        private async void button_L2_Click(object sender, RoutedEventArgs e)
        {
            L2 = 0;
            R2 = 1;
            while (L2 == 0)
            {
                string sendText = ")";
                await send(sendText);
                await Task.Delay(20);
            }   
        }

        private async void button_L3_Click(object sender, RoutedEventArgs e)
        {
            L3 = 0;
            R3 = 1;
            while (L3 == 0)
            {
                string sendText = "*";
                await send(sendText);
                await Task.Delay(20);
            } 
        }

        private async void button_L4_Click(object sender, RoutedEventArgs e)
        {
            L4 = 0;
            R4 = 1;
            while (L4 == 0)
            {
                string sendText = "-";
                await send(sendText);
                await Task.Delay(20);
            }
        }

        private async void button_L5_Click(object sender, RoutedEventArgs e)
        {
            L5 = 0;
            R5 = 1;
            while (L5 == 0)
            {
                string sendText = "/";
                await send(sendText);
                await Task.Delay(20);
            }
        }

        private async void button_S5_Click(object sender, RoutedEventArgs e)
        {
            L5 = 1;
            R5 = 1;
            string sendText = "<";
            await send(sendText);
        }

        private async void button_S4_Click(object sender, RoutedEventArgs e)
        {
            L4 = 1;
            R4 = 1;
            string sendText = "=";
            await send(sendText);
        }

        private async void button_S3_Click(object sender, RoutedEventArgs e)
        {
            L3 = 1;
            R3 = 1;
            string sendText = ">";
            await send(sendText);
        }

        private async void button_S2_Click(object sender, RoutedEventArgs e)
        {
            L2 = 1;
            R2 = 1;
            string sendText = "?";
            await send(sendText);
        }

        private async void button_S1_Click(object sender, RoutedEventArgs e)
        {
            L1 = 1;
            R1 = 1;
            string sendText = "@";
            await send(sendText);
        }

        private async void button_R1_Click(object sender, RoutedEventArgs e)
        {
            R1 = 0;
            L1 = 1;
            while (R1 == 0)
            {
                string sendText = "[";
                await send(sendText);
                await Task.Delay(20);
            }
        }

        private async void button_R2_Click(object sender, RoutedEventArgs e)
        {
            R2 = 0;
            L2 = 1;
            while (R2 == 0)
            {
                string sendText = "]";
                await send(sendText);
                await Task.Delay(20);
            }
        }

        private async void button_R3_Click(object sender, RoutedEventArgs e)
        {
            R3 = 0;
            L3 = 1;
            while (R3 == 0)
            {
                string sendText = "^";
                await send(sendText);
                await Task.Delay(20);
            }
        }

        private async void button_R4_Click(object sender, RoutedEventArgs e)
        {
            R4 = 0;
            L4 = 1;
            while (R4 == 0)
            {
                string sendText = "{";
                await send(sendText);
                await Task.Delay(20);
            }
        }

        private async void button_R5_Click(object sender, RoutedEventArgs e)
        {
            R5 = 0;
            L5 = 1;
            while (R5 == 0)
            {
                string sendText = "}";
                await send(sendText);
                await Task.Delay(20);
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            if (connected)
            {
                clientSocket.Dispose();
                clientSocket = null;
                connected = false;
                OutputView.Text = "Socket TCP disconnected";
            }
            else
            {
                OutputView.Text = "Already disconnected";
            }
        }

        private void button_L1_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_L2_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_L3_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_L4_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_L5_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_S1_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_S2_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_S3_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_S4_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_S5_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_R1_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_R2_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_R3_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_R4_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void button_R5_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}