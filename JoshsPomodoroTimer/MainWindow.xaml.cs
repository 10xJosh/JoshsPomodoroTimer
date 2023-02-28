using JoshsPomodoroTimer.Functions;
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
using System.Windows.Media.Animation;
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
        public (int Minutes, int Seconds) TimeSelectedStorage { get; set;}

        public int LongBreakInterval { get; set; } = 4;

        CancellationTokenSource cancelToken = null;
        Settings settings = new Settings();
        Functions.Timer timer = new Functions.Timer();

        public delegate void OnBreakReqiurementMet();
        public static event OnBreakReqiurementMet BreakRequirementMet;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSettings();

            FrmSettings.settingsChanged += settingsChanged;
        }

        public void StartBreak()
        {
            cancelToken = new CancellationTokenSource();
            var token = cancelToken.Token;

            lblHeader.Dispatcher.BeginInvoke(
                new Action(() => {
                        lblTimer.Content = $"Break Time!";
                }));

            if (SessionCounter == LongBreakInterval)
            {
                timer.Minutes = 5;
                timer.Seconds = 0;
                
                UpdateTimer(token);
            } 
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
            cancelToken = new CancellationTokenSource();
            var token = cancelToken.Token;

            Task.Factory.StartNew(() => TimerStart(token));
            btnStart.IsEnabled = false;
        }

        private void btnStop_Click(object sender, MouseButtonEventArgs e)
        {
            lblTimer.Content = $"{TimeSelectedStorage.Minutes}:{TimeSelectedStorage.Seconds}";
            IsTimerActive = false;
            cancelToken.Cancel();
        }

        private void btnPause_Click(object sender, MouseButtonEventArgs e)
        {
            IsTimerActive = false;
            cancelToken.Cancel();
        }

        private void btnSettings_Click(object sender, MouseButtonEventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
        }

        private void UpdateTimer(CancellationToken token)
        {
            var tokenCancel = cancelToken.Token;

            // Adding this check so that output shown will be in 00:00 format
            // without it, timer will appear as 10:3 instead of 10:03

            var timerResult = timer.CountDown(timer.Minutes, timer.Seconds);
            if (timerResult.Seconds < 10)
            {
                lblTimer.Dispatcher.BeginInvoke(
                new Action(() => {
                    if (cancelToken.IsCancellationRequested)
                        return;
                    else
                        lblTimer.Content = $"{timerResult.Minutes}:0{timerResult.Seconds}";
                }));
            }
            else
            {

                lblTimer.Dispatcher.BeginInvoke(
                new Action(() => {
                    if (cancelToken.IsCancellationRequested)
                        return;
                    else
                        lblTimer.Content = $"{timerResult.Minutes}:{timerResult.Seconds}";
                }));
            }

            Thread.Sleep(1000);
        }

        private void TimerStart(CancellationToken token)
        {
            IsTimerActive = true;

            if(token.IsCancellationRequested)
            {
                btnStart.Dispatcher.BeginInvoke(new Action(() => { btnStart.IsEnabled = true; }));
                return;
            }

            UpdateTimer(token);
            

            if(timer.Minutes == 0 && timer.Seconds == 0) 
            {
                SessionCounter++;

                lblPomodoroCount.Dispatcher.BeginInvoke(
                    new Action(() => {
                        lblPomodoroCount.Content = SessionCounter; 
                    }));
                IsTimerActive = false;
                StartBreak();
                return;
            }
            else
            {
               TimerStart(token);
            }
        }

        private void btnExit_Click(object sender, MouseButtonEventArgs e)
        {
            if(cancelToken != null)
            {
                cancelToken.Cancel();
            }
   
            this.Close();
        }

        private void settingsChanged(Settings settings)
        {
            timer.Minutes = settings.Minutes;
            timer.Seconds = settings.Seconds;
            TimeSelectedStorage = (settings.Minutes, settings.Seconds);
            LongBreakInterval = settings.LongBreakInterval;
            MessageBox.Show("LongBreak interval = ", settings.LongBreakInterval.ToString());
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            if (timer.Seconds < 10)
            {
                lblTimer.Content = $"{timer.Minutes}:0{timer.Seconds}";
            }
            else
            {

                lblTimer.Content = $"{timer.Minutes}:{timer.Seconds}";
            }
        }
    }
}
