using DTT2021_Round2.sample.dtos;
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
using System.Windows.Shapes;
namespace DTT2021_Round2
{
    /// <summary>
    /// Interaction logic for Round1.xaml
    /// </summary>
    public partial class Round1 : System.Windows.Window
    {
        private List<Question> listQuestion = null;
        private List<Question> listBUQuestion = null;
        private List<Question> currentListQuestion = null;
        private List<Label> listLabelQuestion = null;
        private List<Image> listImage = null;
        private List<List<Image>> listAnswerImage = null;
        private Boolean isBackup = false;
        private int currentQuestion = 1;
        private MainWindow mainWindow = null;

        public Round1(List<Question> listQuestion, List<Question> listBUQuestion, MainWindow mainWindow)
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            this.WindowState = WindowState.Maximized;
            this.listQuestion = listQuestion;
            this.listBUQuestion = listBUQuestion;
            this.mainWindow = mainWindow;
            currentListQuestion = listQuestion;
            HiddenAllGrid();
            grdRound1.Visibility = Visibility.Visible;
            grdButton.Visibility = Visibility.Visible;

            DivideGrid();
            DivideColumn(listQuestion);
            InitNoQuestion();
            InitDigitAnswer(listQuestion);
        }

        //divide row
        private void DivideGrid()
        {
            for (int i = 0; i < 6; i++)
            {
                RowDefinition rowDefinition = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                grdNo.RowDefinitions.Add(rowDefinition); 
            }

            for (int i = 0; i < 6; i++)
            {
                RowDefinition rowDefinition = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
                grdDigit.RowDefinitions.Add(rowDefinition);
            }
        }

        private void DivideColumn(List<Question> list)
        {
            int maxDigit = FindMax(list) > 10?FindMax(list):10;
            for (int i = 0; i < maxDigit; i++)
            {
                ColumnDefinition colDefinition = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                grdDigit.ColumnDefinitions.Add(colDefinition);
            }

        }

        //set image into screen
        private void InitNoQuestion()
        {
            this.listLabelQuestion = new List<Label>();
            this.listImage = new List<Image>();
            for (int i = 0; i < 6; i++)
            {
                Image image = new Image();
                Viewbox viewBox = new Viewbox();
                Label label = new Label() { Content = "Câu " + (i + 1), Foreground = Brushes.White };
                label.MouseDoubleClick += Label_MouseDoubleClick;
                this.listLabelQuestion.Add(label);
                this.listImage.Add(image);
                if (i == 5)
                {
                    label.Content = "Từ khóa";
                }  
                viewBox.Child = label;
                MediaAct.Instance.Upload(image, "Obstacles_BoxNumberImage.png");
                image.SetValue(Grid.RowProperty, i);
                image.SetValue(Grid.ColumnProperty, 1);
                viewBox.SetValue(Grid.RowProperty, i);
                viewBox.SetValue(Grid.ColumnProperty, 1);
                grdNo.Children.Add(image);
                grdNo.Children.Add(viewBox);
            }
        }

        private void InitDigitAnswer(List<Question> list)
        {
            this.listAnswerImage = new List<List<Image>>();
            foreach (Question question in list)
            {
                List<Image> tmp = new List<Image>();
                int count = question.NumberOfDigits;
                for (int i = 0; i < count; i++)
                {
                    Image image = new Image();
                    MediaAct.Instance.Upload(image, "Obstacles_ObstacleImage.png");
                    image.SetValue(Grid.RowProperty, question.ID - 1);
                    image.SetValue(Grid.ColumnProperty, i);
                    tmp.Add(image);
                    grdDigit.Children.Add(image);
                }
                this.listAnswerImage.Add(tmp);
            }
        }

        private void SetQuestionDetail_Round1(Question question)
        {
            HiddenAllGrid();
            grdDetailQuestionRound1.Visibility = Visibility.Visible;

            if(question.ID != 6)
            {
                tbQuestionDetail.Text = "Câu " + question.ID + ": " + question.Detail;
            }
            else
            {
                tbQuestionDetail.Text = "Từ khóa";
            }
            
            
            int maxDigit = FindMax(currentListQuestion) > 10 ? FindMax(currentListQuestion) : 10;
            if(question.NumberOfDigits % 2 == 1)
            {
                maxDigit += 1;
            }
            grdRound1_DigitDetail.ColumnDefinitions.Clear();
            grdRound1_DigitDetail.Children.Clear();
            for (int i = 0; i < maxDigit; i++)
            {
                ColumnDefinition colDefinition = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
                grdRound1_DigitDetail.ColumnDefinitions.Add(colDefinition);
            }

            int count = question.NumberOfDigits;
            int gap = Convert.ToInt32(Math.Ceiling((double)(maxDigit - count) / 2));
            for (int i = 0; i < count; i++)
            {
                Image image = new Image();
                MediaAct.Instance.Upload(image, "Obstacles_ObstacleChosenImage.png");
                //image.SetValue(Grid.RowProperty, 0);
                image.SetValue(Grid.ColumnProperty, i + gap);
                grdRound1_DigitDetail.Children.Add(image);
            }

            Image imageClock = new Image();
            MediaAct.Instance.Upload(imageClock, "BoxTime.png");
            imageClock.SetValue(Grid.ColumnProperty, 1);
            grdTimeRound1.Children.Add(imageClock);
            Viewbox viewbox = new Viewbox();
            Label timeTick = new Label { Content = "00:15", Foreground=Brushes.White};
            if(question.ID == 6)
            {
                timeTick.Content = "00:10";
            }
            timeTick.MouseDoubleClick += TimeTick_MouseDoubleClick;

            viewbox.Child = timeTick;
            viewbox.SetValue(Grid.ColumnProperty, 1);
            grdTimeRound1.Children.Add(viewbox);
        }

        private void TimeTick_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label timeTick = sender as Label;
            Thread thread = new Thread(
                () =>
                {
                    int count = 15;
                    if(currentQuestion == 5)
                    {
                        count = 10;
                    }

                    while (count != 0)
                    {
                        Thread.Sleep(1000);
                        count--;
                        this.Dispatcher.Invoke(() =>
                        {
                            timeTick.Content = "00:" + count.ToString("D2");
                            MediaAct.Instance.Upload(tickSound, "TickSound.mp3");
                            tickSound.Play();
                        });
                    }
                    this.Dispatcher.Invoke(() => {
                        HiddenAllGrid();
                        grdRound1.Visibility = Visibility.Visible;
                        grdButton.Visibility = Visibility.Visible;
                        if (currentQuestion != 5)
                            {
                                ShadowImage(currentQuestion);
                            }                      
                    });
                    if(currentQuestion != 5)
                    {
                        VisibleImage(currentQuestion);
                    }
                }
                   
                );
            thread.Start();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            Label label = sender as Label;
            int index = listLabelQuestion.IndexOf(label);
            currentQuestion = index;

            Question question = currentListQuestion[index];
            SetQuestionDetail_Round1(question);
        }

        private void ShadowImage(int index)
        {
            DoubleAnimation shadow = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
            listLabelQuestion[index].BeginAnimation(Label.OpacityProperty, shadow);
            listImage[index].BeginAnimation(Image.OpacityProperty, shadow);
            foreach (Image image in listAnswerImage[index])
            {
                image.BeginAnimation(Image.OpacityProperty, shadow);
            }
        }

        private void VisibleImage(int index)
        {
            Thread thread = new Thread(
                () =>
                {
                    Thread.Sleep(2000);
                    this.Dispatcher.Invoke(() => {
                        DoubleAnimation show = new DoubleAnimation(1, TimeSpan.FromSeconds(0));
                        Question question = null;
                        if (isBackup)
                        {
                            question = listBUQuestion[index];
                        }
                        else
                        {
                            question = listQuestion[index];
                        }
                        
                        for (int i = 0; i < question.NumberOfDigits; i++)
                        {
                            Image image = new Image();
                            MediaAct.Instance.Upload(image, "Obstacles_ObstacleChosenImage.png");
                            image.SetValue(Grid.RowProperty, index);
                            image.SetValue(Grid.ColumnProperty, i);
                            grdDigit.Children.Add(image);
                            listAnswerImage[index].Add(image);
                        }

                        listLabelQuestion[index].BeginAnimation(Label.OpacityProperty, show);
                        listImage[index].BeginAnimation(Image.OpacityProperty, show);
                        grdButton.Visibility = Visibility.Visible;
                    });
                }
                );
            thread.Start();
        }

        private int FindMax(List<Question> list)
        {
            int max = 0;
            foreach (Question question in list)
            {
                if(question.NumberOfDigits > max)
                {
                    max = question.NumberOfDigits;
                }
            }
            return max;
        }

        private void HiddenAllGrid()
        {
            grdRound1.Visibility = Visibility.Hidden;
            grdDetailQuestionRound1.Visibility = Visibility.Hidden;
            grdButton.Visibility = Visibility.Hidden;
        }

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            isBackup = true;
            HiddenAllGrid();
            grdDigit.Children.Clear();
            grdDigit.ColumnDefinitions.RemoveRange(0, FindMax(listQuestion) > 10 ? FindMax(listQuestion) : 10);
            DivideColumn(listBUQuestion);
            InitDigitAnswer(listBUQuestion);
            currentListQuestion = listBUQuestion;
            grdRound1.Visibility = Visibility.Visible;
            grdButton.Visibility = Visibility.Visible;
        }

        private void btnTimeAnswer_Click(object sender, RoutedEventArgs e)
        {
            currentQuestion = 5;
            SetQuestionDetail_Round1(currentListQuestion[5]);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            mainWindow.Visibility = Visibility.Visible;
        }
    }
}
