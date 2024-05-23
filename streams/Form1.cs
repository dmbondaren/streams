using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace streams
{
    public partial class Form1 : Form
    {
        private Thread tX, tY, tCheckCursor;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread sequentialThread = new Thread(() =>
            {
                MyShowX();
                MyShowY();
            });
            sequentialThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tX = new Thread(MyShowX)
            {
                Priority = ThreadPriority.Lowest
            };
            tX.Start();

            tY = new Thread(MyShowY)
            {
                Priority = ThreadPriority.Lowest
            };
            tY.Start();

            tCheckCursor = new Thread(CheckCursor)
            {
                Priority = ThreadPriority.Lowest
            };
            tCheckCursor.Start();
        }

        private void MyShowX()
        {
            int x = Cursor.Position.X;
            int x2 = x;

            while (x > 100)
            {
                x2 = Cursor.Position.X;
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        label1.Text = "x = " + x.ToString();
                        this.Refresh();
                    });
                }
                else
                {
                    label1.Text = "x = " + x.ToString();
                    this.Refresh();
                }
                x = x2;
                Thread.Sleep(50); // Sleep to avoid high CPU usage

            }

        }

        private void MyShowY()
        {
            int y = Cursor.Position.Y;
            int y2 = y;

            while (y > 100)
            {
                y2 = Cursor.Position.Y;
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        label2.Text = "y = " + y.ToString();
                        this.Refresh();
                    });
                }
                else
                {
                    label2.Text = "y = " + y.ToString();
                    this.Refresh();
                }
                y = y2;
                Thread.Sleep(50); // Sleep to avoid high CPU usage
            }
        }

        private void CheckCursor()
        {
            while (true)
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        Point cursorPosition = Cursor.Position;
                        Point button1Location = PointToScreen(button1.Location);
                        Point button2Location = PointToScreen(button2.Location);

                        bool isCursorOverButton1 = cursorPosition.X >= button1Location.X &&
                                                   cursorPosition.X <= button1Location.X + button1.Width &&
                                                   cursorPosition.Y >= button1Location.Y &&
                                                   cursorPosition.Y <= button1Location.Y + button1.Height;

                        bool isCursorOverButton2 = cursorPosition.X >= button2Location.X &&
                                                   cursorPosition.X <= button2Location.X + button2.Width &&
                                                   cursorPosition.Y >= button2Location.Y &&
                                                   cursorPosition.Y <= button2Location.Y + button2.Height;

                        if (isCursorOverButton1)
                        {
                            label3.Text = "Cursor over button1";
                        }
                        else if (isCursorOverButton2)
                        {
                            label3.Text = "Cursor over button2";
                        }
                        else
                        {
                            label3.Text = string.Empty;
                        }
                    });
                }

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}
