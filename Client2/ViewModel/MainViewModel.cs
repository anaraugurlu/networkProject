using networkProject.Command;
using networkProject.Network;
using networkProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
namespace Client2.ViewModel
{
    class MainViewModel:BaseViewModel
    {
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand VoiceCommand { get; set; }
        public RelayCommand PhotoCommand { get; set; }
        public RelayCommand VideoCommand { get; set; }
        public RelayCommand SendCommand { get; set; }
        public DispatcherTimer Timer { get; set; } = new DispatcherTimer();
        public MainViewModel(MainWindow mainwindow)
        {
            CloseCommand = new RelayCommand((s) =>
            {
                mainwindow.window.Close();
            });
            VideoCommand = new RelayCommand((s) =>
            {
            });
            PhotoCommand = new RelayCommand((s) =>
            {
            });
            VoiceCommand = new RelayCommand((s) =>
            {
            });
            SendCommand = new RelayCommand((s) =>
            {
                Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (!NetworkClient.ClientSocket.Connected)
                        {
                            NetworkClient.ConnectToServer();
                        }
                        byte[] buffer = Encoding.ASCII.GetBytes(mainwindow.messtxtb.Text);
                        NetworkClient.ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
                        mainwindow.list.Items.Add(mainwindow.messtxtb.Text);
                    });
                });
            });
        }
    }
}
