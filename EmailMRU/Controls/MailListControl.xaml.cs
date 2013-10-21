using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Resources;


namespace EmailMRU
{
    /// <summary>
    /// Interaction logic for MailList.xaml
    /// </summary>
    public partial class MailListControl : UserControl
    {
        Action<Email> sc, dc;

        public MailListControl(Action<Email> singleClick, Action<Email> doubleClick)
        {
            InitializeComponent();
            sc = singleClick;
            dc = doubleClick;
        }

        private void lb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dc(lb.SelectedItem as Email);
        }

        private void lb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            sc(lb.SelectedItem as Email);
        }
    }
}
