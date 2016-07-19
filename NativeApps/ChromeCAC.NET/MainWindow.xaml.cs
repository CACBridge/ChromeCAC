using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ChromeCAC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Just hide the main window
            InitializeComponent();
            Hide();

            // Handle all communication and signing logic
            HandleNativeMessages();

            // Close the application
            Close();
        }

        void HandleNativeMessages()
        {
            SignatureRequest r;
            while (true)
            {
                NativeMessage m = NativeMessage.Read();
                r = m.Get<SignatureRequest>();
                if (r != null)
                    break;
                Thread.Sleep(25);
            }

            string data = r.data;

            //MessageBox.Show("[Debug] " + data);

            var result = MessageBox.Show("A website is attempting to sign data with your private key. Do you sure you want to continue?", "CACBridge", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var sig = CAC.Sign(data);

                    NativeMessage nativeResponse = new NativeMessage(sig);
                    nativeResponse.Send();

                    if (!string.IsNullOrEmpty(sig.signature))
                    {
                        MessageBox.Show("Data was successfully signed", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong, failed to sign data.", "Error");
                    }

                    return;
                }
                catch
                {
                    MessageBox.Show("An error occurred. Data was not signed.", "Error");
                }
            }
        }
    }
}
