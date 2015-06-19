using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.Threading;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace guess
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string songdata;
        song []sk;
        int isstart = 0;
        int px;
        int right = 0;
        string right_word = "";
        string wrong_word = "";
        int right_num=0;
        int wrong_num=0;
        int font_soze = 32;
        int song_number;
        int []song_weight;

        private DispatcherTimer timer;
        DateTime startTime;
        DateTime currentTime;
        double timed;
        double gametime;

        public MainPage()
        {
            this.InitializeComponent();
            Loaded += new RoutedEventHandler(Window1_Loaded);
            ProjectFile();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void g_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (isstart == 2 && right ==0)
            {
                double x = e.GetCurrentPoint(null).Position.X;
                px = (int)x;
                //string xxx = "";
                if (px < 683)
                {
                    right = 1;
                    right_word =right_word + textb1.Text + "\n";
                    right_num++;
                    //xxx = "right";
                }
                else
                {
                    right = 2;
                    wrong_word = wrong_word + textb1.Text + "\n";
                    wrong_num++;
                    //xxx = "error";
                }
                Random rd = new Random();
                int i = rd.Next(1, song_number);

                int pi, ni;
                pi = i - 1;
                ni = i + 1;
                if (pi < 0) pi = 0;
                if (ni > song_number) ni = song_number;
                if (song_weight[pi] > song_weight[ni]) pi = ni;
                if (song_weight[pi] < song_weight[i]) i = pi;
                song_weight[i]++;

                textb1.Text = sk[i].songname;
                textsinger.Text = sk[i].singer;
                textlyric.Text = sk[i].songlyric;
                //right = 0;
            }
        }

        struct song
        {
            public int id;
            public string songname;
            public string singer;
            public string songlyric;
        }

        //文件读取-异步
        private async void ProjectFile()
        {

            // settings
            var _Path = @"Assets\data\lyric.txt"; ;
            var _Folder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            // acquire file
            var _File = await _Folder.GetFileAsync(_Path);

            // read content
            var _ReadThis = await Windows.Storage.FileIO.ReadTextAsync(_File);

            songdata = _ReadThis;

            string[] ss = songdata.Split('\n');

            int num = System.Int32.Parse(ss[0]) + 1;

            sk = new song[num];
            song_weight = new int[num];
            song_number = ss.Length - 1;

            for (int n = 1; n < song_number; n++)
            {
                song_weight[n] = 0;
                int p = 0;
                string[] sss = ss[n].Split('#');
                sk[n].id = n;
                sk[n].singer = sss[p++];
                sk[n].songname = sss[p++];
                sk[n].songlyric = sss[p++];
                /*sk[n].songname = "";
				for(; p<sss.Length;p++){
					sk[n].songname = sk[n].songname + " " +sss[p];
				}*/
            }


        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {

            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(0.4);

            timer.Tick += new EventHandler<object>(timer1_Tick);

            timer.Start();
        }

        private void show_result()
        {
            rightword.Height = (font_soze+8) * right_num ;
            wrongword.Height = (font_soze+8) * wrong_num ;
            rightword.Text = right_word;
            wrongword.Text = wrong_word;
            //scr.Background = new SolidColorBrush(Windows.UI.Colors.Black);
            
        }

        private void show()
        {
            int m, s;
            m = Convert.ToInt32(timed) / 60;
            s = Convert.ToInt32(timed) % 60;
            if (s > 9) { time1.Text = m + ":" + s; }
            else { time1.Text = m + ":0" + s; }
            if (right == 0)
            {
                //scr.Background = new SolidColorBrush(Windows.UI.Colors.Black);
                rightarea.Visibility = Visibility.Collapsed;
                wrongarea.Visibility = Visibility.Collapsed;
            }else
            {
                // Brush bru;
                //if(right==1)scr.Background = new SolidColorBrush(Windows.UI.Colors.Green);
                //else scr.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                if (right == 1) rightarea.Visibility = Visibility.Visible;
                else wrongarea.Visibility = Visibility.Visible;
                right = 0;
            }
        }

        void timer1_Tick(object sender, object e)
        {
            if (isstart == 1)
            {
                currentTime = DateTime.Now;
                timed = (currentTime - startTime).TotalSeconds;
                timed = gametime - timed;
                int x = (int)(timed + 3 - gametime);
                if (x < 0)
                {
                    isstart = 2;
                    ctime.Visibility = Visibility.Collapsed;
                    time2.Visibility = Visibility.Collapsed;
                    cav.Visibility = Visibility.Visible;
                }
                else
                {
                    time2.Text = (x+1).ToString();
                }
            }
            else if(isstart == 2)
            {
                currentTime = DateTime.Now;
                timed = (currentTime - startTime).TotalSeconds;
                timed = gametime - timed;
                if (timed > 0)
                {
                    /*int m, s;
                    m = Convert.ToInt32(timed) / 60;
                    s = Convert.ToInt32(timed) % 60;
                    time1.Text = m + ":" + s;*/
                    show();
                }
                else
                {
                    wrong_word = wrong_word + textb1.Text + "\n";
                    wrong_num++;
                    isstart = 0;
                    result.Visibility = Visibility.Visible;
                    ctime.Visibility = Visibility.Collapsed;
                    time2.Visibility = Visibility.Collapsed;
                    cav.Visibility = Visibility.Collapsed;
                    show_result();
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (isstart == 0)
            {
                if (xianshigeci.IsChecked == true)
                {
                    textlyric.Visibility = Visibility.Visible;
                }
                else
                {
                    textlyric.Visibility = Visibility.Collapsed;
                }
                isstart = 1;
                startTime = DateTime.Now;
                time1.Text = System.Int32.Parse(timebox.Text) / 60 + ":" + System.Int32.Parse(timebox.Text) % 60;
                gametime = (double)(System.Int32.Parse(timebox.Text) + 5);
                ctime.Visibility = Visibility.Collapsed;
                time2.Visibility = Visibility.Visible;
                cav.Visibility = Visibility.Collapsed;
                currentTime = DateTime.Now;

                Random rd = new Random();
                int i = rd.Next(1, sk.Length);
                textb1.Text = sk[i].songname;
                textsinger.Text = sk[i].singer;
                textlyric.Text = sk[i].songlyric;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            result.Visibility = Visibility.Collapsed;
            ctime.Visibility = Visibility.Visible;
            time2.Text = "倒计时";
            right_word = "";
            wrong_word = "";
            right_num = 0;
            wrong_num = 0;
        }

    }
}
