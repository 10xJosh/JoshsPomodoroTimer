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

        public bool isTimerComplete { get; set; }
        public bool isTimerActive { get; set; }

        Functions.Timer timer = new Functions.Timer();

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
            isTimerActive = true;
            
            // Adding this check so that output shown will be in 00:00 format
            // without it, timer will appear as 10:3 instead of 10:03

            var timerResult = timer.CountDown(timer.Minutes, timer.Seconds);
            if (timerResult.Seconds != 0 && timerResult.Seconds < 10)
            {
                lblTimer.Content = $"{timerResult.Minutes}:0{timerResult.Seconds}";
            }
            else
                lblTimer.Content = $"{timerResult.Minutes}:{timerResult.Seconds}";
        }

        private void btnStop_Click(object sender, MouseButtonEventArgs e)
        {
            isTimerActive = false;
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
