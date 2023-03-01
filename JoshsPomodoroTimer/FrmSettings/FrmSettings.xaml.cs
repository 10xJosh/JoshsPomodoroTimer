using JoshsPomodoroTimer.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace JoshsPomodoroTimer
{
    public partial class FrmSettings : Window
    {
        public double Volume { get; set; } = 90;
        public int LongBreakInterval { get; set; }
        public static bool IsAutoStartBreakEnabled { get; set; } = true;
        public string AlarmSound { get; set; }
        public static int BreakDuration { get; set; } = 5;
        public int Minutes { get; set; } = 25;
        public int Seconds { get; set; }


        public delegate void OnSettingsChanged(Settings settings);
        public static event OnSettingsChanged settingsChanged;

        public FrmSettings()
        {
            InitializeComponent();
            InitializeComboBox();

        }

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

        private void btnSave_Click(object sender, MouseButtonEventArgs e)
        {
            
            try
            {
                Settings settings = new Settings(
                Volume = sldrVolume.Value,
                LongBreakInterval = Int32.Parse(cmboLongBreakInterval.Text),
                IsAutoStartBreakEnabled = chkboxAutoStartBreaks.IsChecked.Value,
                AlarmSound = AlarmSound,
                BreakDuration = Int32.Parse(cmboBoxBreakDuration.Text.Replace(" minutes", "")),
                Minutes = Int32.Parse(txtMinutes.Text),
                Seconds = Int32.Parse(txtSeconds.Text)
                );

                settingsChanged(settings);
                this.Close();
            }
            catch
            {
                MessageBox.Show("There was an error with your minute/second input. Please try again.");
            }
        }

        private void btnExit_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void sldrVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void cmboBoxBreakDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void InitializeComboBox()
        {
            for (int i = 5; i < 35; i+=5)
            {
                cmboBoxBreakDuration.Items.Add($"{i} minutes");
            }
            cmboBoxBreakDuration.SelectedIndex = 0;

            for (int i = 1; i < 8; i++)
            {
                cmboLongBreakInterval.Items.Add(i);
            }
            // 4 intervals is the default recommendation for pomodoro
            cmboLongBreakInterval.SelectedIndex = 3; 
        }

        private void txtMinutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Int32.TryParse(txtMinutes.Text, out int minutes))
            {
                if(minutes > 61)
                {
                    txtMinutes.Text = "60";
                } 
                else if (minutes < 0)
                {
                    txtMinutes.Text = "0";
                }
            }
            else if(txtMinutes.Text == string.Empty)
            {
                txtMinutes.Text = "00";
                return;
            }
            else
            {
                txtMinutes.Text = "25";
            }
        }

        private void txtSeconds_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Int32.TryParse(txtSeconds.Text, out int seconds))
            {
                if (seconds >= 60)
                {
                    txtSeconds.Text = "59";
                }
                else if (seconds < 0)
                {
                    txtSeconds.Text = "0";
                }
            }
            else if (txtMinutes.Text == string.Empty)
            {
                return;
            }
            else
            {
                txtSeconds.Text = "0";
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
