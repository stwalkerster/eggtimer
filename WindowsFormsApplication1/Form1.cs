using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int TIMER_END = 500;
        private const int TIMER_TICK = 100;

        private DateTime _endTime;
        private TimeSpan _countdownTime =new TimeSpan(0, 0, 15, 0);
        private TimeSpan _lastSetCountdownTime = new TimeSpan(0, 0, 15, 0);

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (timer1.Interval == TIMER_END)
            {
                label1.Text = _countdownTime.Hours.ToString("00") + ":" + _countdownTime.Minutes.ToString("00") + ":" +
                                  _countdownTime.Seconds.ToString("00") + "." + _countdownTime.Milliseconds.ToString("000");

                this.BackColor = this.BackColor == Color.Red ? Color.Black : Color.Red;
                if(_countdownTime .TotalMilliseconds > 0 && this.BackColor == Color.Black)
                {
                    panel1.BackColor = panel1.BackColor == Color.Green ? Color.Black : Color.Green;
                }
                
            }
            else
            {
                if (_endTime < DateTime.Now)
                {
                    timer1.Interval = TIMER_END;
                    label1.Text = "00:00:00.000";
                    _countdownTime = new TimeSpan();
                    this.BackColor = this.BackColor == Color.Red ? Color.Black : Color.Red;
                    panel1.BackColor = Color.Black;
                    this.TopMost = true;
                    if (this.WindowState == FormWindowState.Minimized)
                        this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    TimeSpan rem = (_endTime - DateTime.Now);
                    label1.Text = rem.Hours.ToString("00") + ":" + rem.Minutes.ToString("00") + ":" +
                                  rem.Seconds.ToString("00") + "." + rem.Milliseconds.ToString("000");
                }
            }
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (timer1.Interval != TIMER_END)
                    {
                        if (timer1.Enabled)
                        {
                            _countdownTime = (_endTime - DateTime.Now);
                            timer1.Interval = TIMER_END;
                            this.BackColor = this.BackColor == Color.Red ? Color.Black : Color.Red;
                            panel1.BackColor = Color.Black;

                            label1.Text = _countdownTime.Hours.ToString("00") + ":" + _countdownTime.Minutes.ToString("00") + ":" +
                  _countdownTime.Seconds.ToString("00") + "." + _countdownTime.Milliseconds.ToString("000");

                        }


                        return;
                    }
                    break;
                case MouseButtons.Right:
                    if(timer1.Interval==TIMER_TICK) return;
                    Form2 t = new Form2
                                  {
                                      numericUpDown1 = {Value = _countdownTime.Hours},
                                      numericUpDown2 = {Value = _countdownTime.Minutes},
                                      numericUpDown3 = {Value = _countdownTime.Seconds}
                                  };
                    t.ShowDialog();
                    this.Text = t.textBox1.Text;
                    _lastSetCountdownTime = _countdownTime = new TimeSpan(0, (int)t.numericUpDown1.Value, (int)t.numericUpDown2.Value, (int)t.numericUpDown3.Value);
                    return;
            }
            timer1.Interval = TIMER_TICK;
            this.BackColor = Color.Black;
            panel1.BackColor = Color.Green;
            if (_countdownTime.TotalMilliseconds == 0)
                _countdownTime = _lastSetCountdownTime;
            _endTime = DateTime.Now + _countdownTime;
            this.TopMost = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
