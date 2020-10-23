using DTT2021_Round2.sample.dtos;
using DTT2021_Round2.Window;
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
namespace DTT2021_Round2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private List<Question> listQuestion = null;
        private List<Question> listBUQuestion = null;

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            ImageBrush background = new ImageBrush();
            MediaAct.Instance.Upload(background, "background.png");
            this.Background = background;
            LoadData();
        }

        private void LoadData()
        {
            ReadExcel.Instance.FileName = ReadExcel.Instance.GetPath();
            if (ReadExcel.Instance.FileName != String.Empty)
            {
                //0 stands for offical question, 1 stands for backup question
                listQuestion = ReadExcel.Instance.GetQuestion(0);
                listBUQuestion = ReadExcel.Instance.GetQuestion(1);
            }
            else
            {
                this.Close();
            }
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Round1 round1 = new Round1(listQuestion, listBUQuestion, this);
            round1.Visibility = Visibility.Visible;
        }

        private void lbRound2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Round2 round2 = new Round2(this);
            round2.Visibility = Visibility.Visible;

        }
    }
}
