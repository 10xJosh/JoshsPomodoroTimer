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
        public bool IsPauseEffectBreakEnabled { get; set; }
        public bool IsAutoStartBreakEnabled { get; set; }
        public string AlarmSound { get; set; }
        public string BreakDuration { get; set; }

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
            MessageBox.Show(Convert.ToDouble(sldrVolume.Value).ToString());
            this.Close();
        }

        private void btnExit_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void sldrVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Volume = sldrVolume.Value;
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
            
        }
    }
}
