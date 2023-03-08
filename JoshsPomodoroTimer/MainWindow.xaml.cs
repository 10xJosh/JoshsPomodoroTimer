﻿using JoshsPomodoroTimer.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
        public (int Minutes, int Seconds) TimeSelectedStorage { get; set; } = (25, 0); 
        public int LongBreakInterval { get; set; } = 4;
        public int BreakDuration { get; set; } = 5;

        CancellationTokenSource cancelToken = null;
        Settings settingsStorage = new Settings();
        Functions.Timer timer = new Functions.Timer();

        public delegate void OnBreakReqiurementMet();
        public static event OnBreakReqiurementMet BreakRequirementMet;

        public MainWindow()
        {
            InitializeComponent();
            InitializeSettings();

            FrmSettings.settingsChanged += settingsChanged;
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
            IsTimerActive = false;

            if(cancelToken != null) 
                cancelToken.Cancel();

            if (TimeSelectedStorage.Seconds < 10)
                lblTimer.Content = $"{TimeSelectedStorage.Minutes}:0{TimeSelectedStorage.Seconds}";
            else
                lblTimer.Content = $"{TimeSelectedStorage.Minutes}:{TimeSelectedStorage.Seconds}";

            timer.Minutes = TimeSelectedStorage.Minutes;
            timer.Seconds = TimeSelectedStorage.Seconds;
        }

        private void btnPause_Click(object sender, MouseButtonEventArgs e)
        {
            IsTimerActive = false;
            cancelToken.Cancel();
        }

        private void btnSettings_Click(object sender, MouseButtonEventArgs e)
        {
            if(settingsStorage != null)
            {
                FrmSettings frmSettings = new FrmSettings(settingsStorage);
                frmSettings.ShowDialog();
            } else
            {
                FrmSettings frmSettings = new FrmSettings();
                frmSettings.ShowDialog();
            }
            
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
                StartBreakInitilization();
                return;
            }
            else
            {
               TimerStart(token);
            }
        }


        public void StartBreakInitilization()
        {
            cancelToken = new CancellationTokenSource();
            var token = cancelToken.Token;

            lblHeader.Dispatcher.BeginInvoke(new Action(() => { lblHeader.Content = $"Break Time! Good Work!"; }));

            if (SessionCounter == FrmSettings.LongBreakInterval)
            {
                timer.Minutes = FrmSettings.LongBreakMinutes;
                timer.Seconds = 0;

                Task.Factory.StartNew(() => BreakStart(token));
            }
            else
                timer.Minutes = FrmSettings.BreakDuration;
            Task.Factory.StartNew(() => BreakStart(token));
        }

        private void BreakStart(CancellationToken token)
        {
            IsTimerActive = true;

            if (token.IsCancellationRequested)
            {
                btnStart.Dispatcher.BeginInvoke(new Action(() => { btnStart.IsEnabled = true; }));
                return;
            }

            UpdateTimer(token);

            if (timer.Minutes == 0 && timer.Seconds == 0 && FrmSettings.IsAutoStartBreakEnabled)
            {
                cancelToken = new CancellationTokenSource();
                var tokenCancel = cancelToken.Token;

                lblPomodoroCount.Dispatcher.BeginInvoke(
                        new Action(() => {
                            lblHeader.Content = $"Nothing to it but to do it!";
                        }));

                timer.Minutes = TimeSelectedStorage.Minutes;
                timer.Seconds = TimeSelectedStorage.Seconds;
                
                TimerStart(tokenCancel);
            }
            else if (timer.Minutes == 0 && timer.Seconds == 0)
            {
                lblPomodoroCount.Dispatcher.BeginInvoke(
                        new Action(() => {
                            lblHeader.Content = $"Nothing to it but to do it!";
                        }));
                timer.Minutes = TimeSelectedStorage.Minutes;
                timer.Seconds = TimeSelectedStorage.Seconds;
                btnStart.Dispatcher.BeginInvoke( new Action(() => {btnStart.IsEnabled = true; }));
            }
            else
                TimerStart(token);
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
            BreakDuration = settings.BreakDuration;

            if (IsTimerActive)
            {
                return;
            }
            else
            {
                InitializeSettings();
            }
        }
        private void InitializeSettings()
        {
            if (timer.Seconds < 10)
                lblTimer.Content = $"{timer.Minutes}:0{timer.Seconds}";
            else
                lblTimer.Content = $"{timer.Minutes}:{timer.Seconds}";
        }
    }
}
