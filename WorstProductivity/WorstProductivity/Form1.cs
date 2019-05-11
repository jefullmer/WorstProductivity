using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using WorstProductivity.Properties;

namespace WorstProductivity
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        //private static List<string> schedule;
        private Schedule schedule;

        public Form1()
        {
            InitializeComponent();
            //schedule = new List<string>();
            //timer1.Start();
            //timer2.Start();
            schedule = new Schedule();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Point pt = new Point();
            GetCursorPos(ref pt);
            label1.Text = "X: " + pt.X + " Y: " + pt.Y;
        }
        
        /// <summary>
        /// Timer for YouTube checking
        /// </summary>
        private void timer2_Tick(object sender, EventArgs e)
        {
            //Bitmap screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Bitmap screenCapture = new Bitmap(305, 131);
            Graphics g = Graphics.FromImage(screenCapture);

            g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, screenCapture.Size, CopyPixelOperation.SourceCopy);
            //g.CopyFromScreen(305, 131, 0, 0, screenCapture.Size, CopyPixelOperation.SourceCopy);
            Bitmap pic = Resources.YoutubeLogo;

            bool isInScreen = IsInCapture(pic, screenCapture);
            label2.Text = isInScreen.ToString();
        }

        private bool IsInCapture(Bitmap searchFor, Bitmap searchIn)
        {
            for (int x = 0; x < searchIn.Width; x++)
            {
                for (int y = 0; y < searchIn.Height; y++)
                {
                    bool invalid = false;
                    int k = x, l = y;
                    for (int a = 0; a < searchFor.Width; a++)
                    {
                        l = y;
                        for (int b = 0; b < searchFor.Height; b++)
                        {
                            if (searchFor.GetPixel(a, b) != searchIn.GetPixel(k, l))
                            {
                                invalid = true;
                                break;
                            }
                            else
                                l++;
                        }
                        if (invalid)
                            break;
                        else
                            k++;
                    }
                    if (!invalid)
                        return true;
                }
            }
            return false;
        }


        //Boring schedule buttons and such
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < dateTimePicker2.Value)
            {
                schedule.AddTask(textBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
                richTextBox1.Text = schedule.GetString();
            }
            else
                MessageBox.Show("Please enter a valid time.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("Please enter a valid time.");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter a title");
            }
            else
            {
                schedule.EditTaskStart(textBox1.Text, dateTimePicker1.Value);
                schedule.EditTaskEnd(textBox1.Text, dateTimePicker2.Value);
                richTextBox1.Text = schedule.GetString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please enter a title");
            }
            else
            {
                schedule.RemoveTask(textBox1.Text);
                richTextBox1.Text = schedule.GetString();
            }
        }
    }
}
