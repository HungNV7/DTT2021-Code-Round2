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
using System.Windows.Shapes;

namespace DTT2021_Round2.Window
{
    /// <summary>
    /// Interaction logic for Round2.xaml
    /// </summary>
    public partial class Round2 : System.Windows.Window
    {
        private MainWindow mainWindow;

        public Round2()
        {
            InitializeComponent();
        }

        public Round2(MainWindow mainWindow):this()
        {
            this.mainWindow = mainWindow;
            InitView();
        }

        private void InitView()
        {
            Image imageClock = new Image();
            MediaAct.Instance.Upload(imageClock, "BoxTime.png");
            imageClock.SetValue(Grid.ColumnProperty, 1);
            grdClock.Children.Add(imageClock);
            Viewbox viewbox = new Viewbox();
            Label timeTick = new Label { Content = "40:00", Foreground = Brushes.White };

            timeTick.MouseDoubleClick += TimeTick_MouseDoubleClick;

            viewbox.Child = timeTick;
            viewbox.SetValue(Grid.ColumnProperty, 1);
            grdClock.Children.Add(viewbox);
        }

        private void TimeTick_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label timeTick = sender as Label;
            Thread thread = new Thread(
                () =>
                {
                    int minute = 40;
                    int second = 0;

                    while (minute != 0 || second != 0)
                    {
                        Thread.Sleep(1000);
                        if(second == 0)
                        {
                            second = 59;
                            minute -= 1;
                        }
                        else
                        {
                            second -= 1;
                        }
                        this.Dispatcher.Invoke(() =>
                        {
                            timeTick.Content = minute.ToString("D2") + ":" + second.ToString("D2");
                            MediaAct.Instance.Upload(tickSound, "TickSound.mp3");
                            tickSound.Play();
                        });
                    }
                    this.Dispatcher.Invoke(() =>
                    {
                        this.Visibility = Visibility.Hidden;
                        mainWindow.Visibility = Visibility.Visible;
                    });
                }

                );
            thread.Start();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.mainWindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;  
        }
    }
}
