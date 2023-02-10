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
    /// <summary>
    /// Interaction logic for FrmSettings.xaml
    /// </summary>
    public partial class FrmSettings : Window
    {
        public FrmSettings()
        {
            InitializeComponent();
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
            MessageBox.Show("Saved");
            this.Close();
        }

        private void btnExit_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
