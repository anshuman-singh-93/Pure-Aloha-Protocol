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
using System.Windows.Threading;
namespace aloha
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //text_array represents textblock.. in this project we have created 3 textblock object as a packets
        // and we named it packet1 ,packet 2 and packet 3
        TextBlock[] text_array = new TextBlock[3];

        //if any packet got collide
        bool packet1_got_collided = false;
        bool packet2_got_collided = false;
        bool packet3_got_collided = false;

        //if packet reached at router
        bool is_packet1_reached = false;
        bool is_packet2_reached = false;
        bool is_packet3_reached = false;
        bool all_packets_reached = false;


        //initial value coordinate of all 3 packets.. 
        //once packet get collide packet will be resending  from their inital position after some random amount of time
        double packet1_left;
        double packet2_top;
        double packet2_left;

        double packet3_top;
        double packet3_left;
        Random rand = new Random();
        DateTime p1_backofftime;
        DateTime p2_backofftime;
        DateTime p3_backofftime;

        List<TextBlock> collided_packet_list = new List<TextBlock>(3);
        DispatcherTimer timer = new DispatcherTimer();

        double speed_of_p1 = 1.5;
        double speed_of_p2 = 0.5;
        double speed_of_p3 = 1.0;

        public void create_random_backoff_time()
        {

        }
        public void move(TextBlock[] text_array, double speed_of_p1 , double speed_of_p2, double speed_of_p3)
        {
           foreach(TextBlock r in text_array)
            {
                if (r.Equals(text1))
                {
                    //packet 1
                    if (!packet1_got_collided)
                    {
                        if (collided_packet_list.Contains(text1))
                            collided_packet_list.Remove(text1);
                        if (Canvas.GetLeft(text1) < Canvas.GetLeft(router_img) && !is_packet1_reached)
                            Canvas.SetLeft(text1, Canvas.GetLeft(text1) + speed_of_p1);
                        else if (!is_packet1_reached)
                        {
                            // MessageBox.Show("packet 1 reached at router");
                            log.Items.Add("packet 1 reached at router");

                            is_packet1_reached = true;
                            Canvas.SetLeft(text1, packet1_left);

                        }

                    }

                }
                else if (r.Equals(text2))
                {
                    if (!packet2_got_collided)
                    {
                        if (collided_packet_list.Contains(text2))
                            collided_packet_list.Remove(text2);

                        if (Canvas.GetTop(text2) < Canvas.GetTop(center_text) && !is_packet2_reached)
                        {
                            // packet reached at the middle
                            Canvas.SetTop(text2, Canvas.GetTop(text2) + speed_of_p2);
                        }
                        else if(!is_packet2_reached)
                        {
                            
                            Canvas.SetLeft(text2, Canvas.GetLeft(text2) + speed_of_p2);
                           if(Canvas.GetLeft(text2) > Canvas.GetLeft(router_img))
                            {
                                is_packet2_reached = true;
                                //MessageBox.Show("packet 2 reached at router ");
                                log.Items.Add("packet 2 reached at router");
                                Canvas.SetLeft(text2, packet2_left);
                                Canvas.SetTop(text2, packet2_top);



                            }


                        }
                    }

                }
                else if (r.Equals(text3))
                {
                    if (!packet3_got_collided)
                    {
                        if (collided_packet_list.Contains(text3))
                            collided_packet_list.Remove(text3);
                        if (Canvas.GetTop(text3) > Canvas.GetTop(center_text) && !is_packet3_reached)
                        {
                            // packet reached at the middle
                            Canvas.SetTop(text3, Canvas.GetTop(text3) - speed_of_p3);
                        }
                        else if (!is_packet3_reached)
                        {

                            Canvas.SetLeft(text3, Canvas.GetLeft(text3) + speed_of_p3);
                            if (Canvas.GetLeft(text3) > Canvas.GetLeft(router_img))
                            {
                                is_packet3_reached = true;
                                // MessageBox.Show("packet 3 reached at router ");
                                log.Items.Add("packet 3 reached at router");
                                Canvas.SetLeft(text3, packet3_left);
                                Canvas.SetTop(text3, packet3_top);



                            }


                        }
                    }

                }
                double p1_left_cd = Canvas.GetLeft(text1);
                double p2_left_cd = Canvas.GetLeft(text2);
                double p3_left_cd = Canvas.GetLeft(text3);

                double p1_top_cd = Canvas.GetTop(text1);
                double p2_top_cd = Canvas.GetTop(text2);
                double p3_top_cd = Canvas.GetTop(text3);
                //if p1 and p2 collides
                if(Math.Abs(p1_left_cd-p2_left_cd)<40 && Math.Abs(p1_top_cd - p2_top_cd) < 20 && !packet1_got_collided && !packet2_got_collided)
                    {
                   // MessageBox.Show("collided" +packet1_left+" "+p2_top_cd);
                 


                    packet1_got_collided = true;
                    packet2_got_collided = true;
                    collided_packet_list.Add(text1);
                    collided_packet_list.Add(text2);
                    int r1 = rand.Next(1, 5);
                    int r2 = rand.Next(5, 10);
                    p1_backofftime = DateTime.Now.AddSeconds(r1);
                    p2_backofftime = DateTime.Now.AddSeconds(r2);
                    log.Items.Add("packet 1 and packet 2 got colide.... ");
                    log.Items.Add("both are waiting for random amount of time ");
                    log.Items.Add("Packet 1 will be resend after"+r1+"sec.");
                    log.Items.Add("Packet 2 will be resend after" + r2 + "sec.");

                  //  MessageBox.Show("packet 1 and packet 2 got colide...." + "\n" + "both are waiting for random amount of time"
                //  + "Packet 1 will be resend after" + r1 + "sec." + "Packet 2 will be resend after" + r2 + "sec.");

                    Canvas.SetLeft(text1, packet1_left);
                    Canvas.SetTop(text2, packet2_top);
                    Canvas.SetLeft(text2, packet2_left);

                }


                //if p1 and p3 collides
                if (Math.Abs(p1_left_cd - p3_left_cd) < 40 && Math.Abs(p1_top_cd - p3_top_cd) < 20 && !packet1_got_collided && !packet3_got_collided)
                {
                   // MessageBox.Show("collided" + packet1_left + " " + p2_top_cd);
                 
                    packet3_got_collided = true;

                    packet1_got_collided = true;
                    collided_packet_list.Add(text1);
                    collided_packet_list.Add(text3);
                    int r1 = rand.Next(1, 5);
                    int r2 = rand.Next(5, 10);
                    p1_backofftime = DateTime.Now.AddSeconds(r1);
                    p3_backofftime = DateTime.Now.AddSeconds(r2);

                    log.Items.Add("packet 1 and packet 3 got colide.... ");
                    log.Items.Add("both are waiting for random amount of time ");
                    log.Items.Add("Packet 1 will be resend after" + r1 + "sec.");
                    log.Items.Add("Packet 3 will be resend after" + r2 + "sec.");

                  //  MessageBox.Show("packet 1 and packet 3 got colide...." + "\n" + "both are waiting for random amount of time"
                 // + "Packet 1 will be resend after" + r1 + "sec." + "Packet 3 will be resend after" + r2 + "sec.");
                    Canvas.SetLeft(text1, packet1_left);
                    Canvas.SetTop(text3, packet3_top);
                    Canvas.SetLeft(text3, packet3_left);

                }

                //if p2 and p3 collides
                if (Math.Abs(p2_left_cd - p3_left_cd) < 40 && Math.Abs(p2_top_cd - p3_top_cd) < 20 && !packet2_got_collided && !packet3_got_collided)
                {
                   // MessageBox.Show("collided" + packet1_left + " " + p2_top_cd);
              
                    packet2_got_collided = true;
                    packet3_got_collided = true;

                    collided_packet_list.Add(text2);
                    collided_packet_list.Add(text3);
                    int r1= rand.Next(1, 5);
                    int r2= rand.Next(5, 10);
                    p2_backofftime = DateTime.Now.AddSeconds(r1);
                    p3_backofftime = DateTime.Now.AddSeconds(r2);

                    log.Items.Add("packet 2 and packet 3 got colide.... ");
                    log.Items.Add("both are waiting for random amount of time ");
                    log.Items.Add("Packet 2 will be resend after" + r1 + "sec.");
                    log.Items.Add("Packet 3 will be resend after" + r2 + "sec.");

                   // MessageBox.Show("packet 2 and packet 3 got colide...." + "\n" + "both are waiting for random amount of time"
                   //     + "Packet 2 will be resend after" + r1 + "sec." + "Packet 3 will be resend after" + r2 + "sec.");

                    Canvas.SetTop(text2, packet2_top);
                    Canvas.SetLeft(text2, packet2_left);
                    Canvas.SetTop(text3, packet3_top);
                    Canvas.SetLeft(text3, packet3_left);

                }


                if(collided_packet_list.Count>0)
                {
                   foreach(TextBlock tc in collided_packet_list)
                    {
                        if(tc.Equals(text1))
                        {
                            if (p1_backofftime <DateTime.Now)
                            {
                                packet1_got_collided = false;
                               // collided_packet_list.Remove(text1);
                                
                            }
                        }

                        else if (tc.Equals(text2))
                        {
                            if (p2_backofftime < DateTime.Now)
                            {
                                packet2_got_collided = false;
                               // collided_packet_list.Remove(text2);
                            }
                        }

                        else if (tc.Equals(text3))
                        {
                            if (p3_backofftime < DateTime.Now)
                            {
                                packet3_got_collided = false;
                               // collided_packet_list.Remove(text3);
                            }
                        }

                    }
                }

                if(is_packet1_reached && is_packet2_reached && is_packet3_reached)
                {
                    MessageBox.Show("all packets are reached");
                    log.Items.Add("All packet are received by router");
                    reset_all_gui(0);
                }


            }
        }

      

        public MainWindow()
        {
            InitializeComponent();

            text_array[0] = text1;
            text_array[1] = text2;
            text_array[2] = text3;
            packet1_left = Canvas.GetLeft(text1);
            packet2_top = Canvas.GetTop(text2);
            packet2_left = Canvas.GetLeft(text2);
            packet3_top = Canvas.GetTop(text3);
            packet3_left = Canvas.GetLeft(text3);

            timer.Tick += new EventHandler(init);
            timer.Interval = new TimeSpan(0,0,0,0,20);
          //  MessageBox.Show(Canvas.GetTop(text3).ToString());

        }

        public void init(object sender, EventArgs e)
        {

            p1_speed_text_block.IsEnabled = false;
            p2_speed_text_block.IsEnabled = false;
            p3_speed_text_block.IsEnabled = false;

            move(text_array,speed_of_p1,speed_of_p2,speed_of_p3);

        }

        private void stop_btn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if(p1_speed_text_block.Text.Length>0)
            {
                try
                {
                    speed_of_p1 = Convert.ToDouble( p1_speed_text_block.Text);
                }
                catch
                {
                    MessageBox.Show("input should be decimal and less than 10");
                }
            }

            if (p2_speed_text_block.Text.Length > 0)
            {
                try
                {
                    speed_of_p2 = Convert.ToDouble(p2_speed_text_block.Text);
                }
                catch
                {
                    MessageBox.Show("input should be decimal and less than 10");

                }
            }


            if (p3_speed_text_block.Text.Length > 0)
            {
                try
                {
                    speed_of_p3 = Convert.ToDouble(p3_speed_text_block.Text);
                }
                catch
                {
                    MessageBox.Show("input should be decimal and less than 10");

                }
            }



            timer.Start();
        }

        private void reset_all_gui( int reset_btn_clicked=1)
        {
            timer.Stop();

            packet1_got_collided = false;
            packet2_got_collided = false;
            packet3_got_collided = false;

            is_packet1_reached = false;
            is_packet2_reached = false;
            is_packet3_reached = false;
            all_packets_reached = false;






            collided_packet_list.Clear();

            speed_of_p1 = 1.5;
            speed_of_p2 = 0.5;
            speed_of_p3 = 1.0;
            if (reset_btn_clicked == 1)
                 log.Items.Clear();

            Canvas.SetLeft(text1, packet1_left);
            Canvas.SetLeft(text2, packet2_left);
            Canvas.SetTop(text2, packet2_top);

            Canvas.SetLeft(text3, packet3_left);
            Canvas.SetTop(text3, packet3_top);
        }

        private void reset_btn_Click(object sender, RoutedEventArgs e)
        {
            reset_all_gui();

        }
    }
}
