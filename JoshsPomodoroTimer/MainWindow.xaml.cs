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
using System.Windows.Threading;

namespace JoshsPomodoroTimer
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


        }

        // This allows the window to be moved around when WindowStyle=None
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            try
            {
                this.DragMove();
            }
            catch
            {
                return;
            }
        }

        private void btnStart_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Start");
        }

        private void btnStop_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Stop");
        }

        private void btnPause_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Pause");
        }

        private void btnSettings_Click(object sender, MouseButtonEventArgs e)
        {
            FrmSettings frmSettings= new FrmSettings();
            frmSettings.ShowDialog();
        }

        private void btnExit_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
