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
        public bool IsTimerComplete { get; set; }
        public bool IsTimerActive { get; set; }
        public bool isBreakActive { get; set; }
        public int SessionCounter { get; set; }

        CancellationTokenSource cancelToken = null;

        Functions.Timer timer = new Functions.Timer();
        FrmSettings frmSettings = new FrmSettings();

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
        
        private async void btnStart_Click(object sender, MouseButtonEventArgs e)
        {
            cancelToken = new CancellationTokenSource();
            var token = cancelToken.Token;
            await Task.Factory.StartNew(() => TimerStart(token));
        }

        private void btnStop_Click(object sender, MouseButtonEventArgs e)
        {
            IsTimerActive = false;
            MessageBox.Show("Stop");
        }

        private void btnPause_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Pause");
        }

        private void btnSettings_Click(object sender, MouseButtonEventArgs e)
        {
            frmSettings.ShowDialog();
        }

        private void UpdateTimer()
        {
            // Adding this check so that output shown will be in 00:00 format
            // without it, timer will appear as 10:3 instead of 10:03
            var timerResult = timer.CountDown(timer.Minutes, timer.Seconds);
            if (timerResult.Seconds < 10)
            {
                //lblTimer.Content = $"{timerResult.Minutes}:0{timerResult.Seconds}";

                lblTimer.Dispatcher.BeginInvoke(new Action(() => { lblTimer.Content = $"{timerResult.Minutes}:0{timerResult.Seconds}"; }));
            }
            else
            {
                //TODO: Find a way to end the invoke method
                //lblTimer.Content = $"{timerResult.Minutes}:{timerResult.Seconds}";
                lblTimer.Dispatcher.BeginInvoke(new Action(() => { lblTimer.Content = $"{timerResult.Minutes}:{timerResult.Seconds}"; }));
            }
              
            Thread.Sleep(1000);
        }

        private void TimerStart(CancellationToken token)
        {
            IsTimerActive = true;

            if(token.IsCancellationRequested)
            {
                return;
            }

            UpdateTimer();
            

            if(timer.Minutes == 0 && timer.Seconds == 0) 
            {
                SessionCounter++;
                IsTimerActive = false;
                return;
                
                if(frmSettings.IsAutoStartBreakEnabled) 
                {
                    
                }
            }
            else
            {
               TimerStart(token);
            }
        }

        private void btnExit_Click(object sender, MouseButtonEventArgs e)
        {
            cancelToken.Dispose();
            this.Close();
        }
    }
}
