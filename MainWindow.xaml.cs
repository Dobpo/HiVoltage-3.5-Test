using System;
using System.Collections.Generic;
using System.Windows;
using SimpleTCP;

namespace HiVoltage_3._5_Test
{

    public partial class MainWindow : Window
    {
        private readonly SimpleTcpClient _client;

        public MainWindow()
        {
            InitializeComponent();
            _client = new SimpleTcpClient().Connect("192.168.0.222", 9670);
            _client.DataReceived += _client_DataReceived;
            Title = _client.TcpClient.Connected.ToString();
        }

        private void _client_DataReceived(object sender, Message e)
        {
            var hex = BitConverter.ToString(e.Data).Replace("-", " ");
            Dispatcher.Invoke(() => { AddLogInfo(e.Data, false); });
        }

        private void Client_DataReceived(object sender, Message e)
        {

        }

        private void AddLogInfo(byte[] array, bool isOutput)
        {
            var hex = BitConverter.ToString(array).Replace("-", " ") + "\n";
            if (isOutput) LogTextBox.AppendText(DateTime.Now.ToString("G") + "\tOUT\t->\t" + hex);
            else LogTextBox.AppendText(DateTime.Now.ToString("G") + "\tIN\t->\t" + hex + "\n");
            LogTextBox.ScrollToEnd();
        }


        private static byte XorChecksum(IReadOnlyList<byte> array, int len)
        {
            var tmpValue = array[0];
            for (var i = 2; i < len; i++) tmpValue = (byte)(tmpValue ^ array[i]);
            tmpValue += 0x80; return tmpValue;
        }

        private void OnButton_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x1F; buffer[2] = 0x01;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }

        private void OffButton_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x1F; buffer[2] = 0x00;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }

        private void OnTriacButton_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x2F; buffer[2] = 0x01;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }

        private void OffTriacButton_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x2F; buffer[2] = 0x00;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }


        private void Status_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x3F; buffer[2] = 0xFF;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }


        private void LoadSettings_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x4F; buffer[2] = 0xFF;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[50];
            buffer[0] = 0x5F;
            for (var i = 0; i < 24; i++) {
                buffer[i*2 + 2] = 0;
                buffer[i*2 + 3] = 0;
            }
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }

        private void Prepare_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x6F; buffer[2] = 0xFF;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }

        private void Xray_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x7F; buffer[2] = 0xFF;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new byte[3];
            buffer[0] = 0x8F; buffer[2] = 0xFF;
            buffer[1] = XorChecksum(buffer, buffer.Length);
            _client.Write(buffer);
            AddLogInfo(buffer, true);
        }


    }

}
