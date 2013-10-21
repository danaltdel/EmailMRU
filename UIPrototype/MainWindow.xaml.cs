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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmailList emailList = new EmailList();

        public MainWindow()
        {
            InitializeComponent();


            emailList.Add(new Email() { ID = "1", from = "daniel altin", subject = "This is the subject of my emaiThis is the subject of my emailThis is the subject of my emailThis is the subject of my emailThis is the subject of my emailThis is the subject of my emailThis is the subject of my emailThis is the subject of my emailThis is the subject of my emaill", dateSent = DateTime.Now, hasAttachment = true });
            emailList.Add(new Email() { ID = "2", from = "daniel altin", subject = "This is the subject of my email", dateSent = DateTime.Now, hasAttachment = true });
            emailList.Add(new Email() { ID = "3", from = "daniel altin", subject = "This is the subject of my email", dateSent = DateTime.Now, hasAttachment = true });


            lb.DataContext = emailList;
        }
    }
}
