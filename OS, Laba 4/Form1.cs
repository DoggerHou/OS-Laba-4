using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OS__Laba_4
{



    public partial class Form1 : Form
    {
        bool flag = true;
        int[] array = new int[10];

        public System.EventHandler obs;


        public Form1()
        {
            InitializeComponent();

            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
                array[i] = rnd.Next(0, 99);

            label1.Text = Array_to_String(array);
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            flag = checkBox1.Checked;
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Thread inc = new Thread(IncreaseSort);
            Thread dec = new Thread(DescendeSort);

            //inc.Start();
            dec.Start();
            label1.Text = Array_to_String(array);
        }



        private string Array_to_String(int[] arr)
        {
            string s = "";
            foreach (int i in arr)
                s += i.ToString() + "  ";
            return s;
        }



        private void DescendeSort()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (array[i] > array[j])
                    {
                        (array[i], array[j]) = (array[j], array[i]);
                        wait(200);
                    }
            //label1.Text = Array_to_String(array);
        }


        private void IncreaseSort()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (array[i] < array[j])
                    {
                        (array[i], array[j]) = (array[j], array[i]);
                        wait(200);
                    }
        }


        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        private void UpdateFromModel(object sender, EventArgs e)
        {
            label1.Text = Array_to_String(array);
        }
    }
}
