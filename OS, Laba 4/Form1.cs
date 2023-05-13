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
        int n;
        int[] array;
        int[] incarray;
        int[] decarray;
        bool f;

        Thread inc;
        Thread dec;



        public Form1()
        {
            InitializeComponent();

            Random rnd = new Random();
            n = rnd.Next(10, 21);
            array = new int[n];
            incarray = new int[n];
            decarray = new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(0, 100);
                incarray[i] = array[i];
                decarray[i] = array[i];
            }
            Array.Sort(incarray);
            decarray = array.OrderByDescending(x => x).ToArray();

            label1.Text = Array_to_String(array);

            inc = new Thread(IncreaseSort);
            dec = new Thread(DescendeSort);

        }


        public delegate void InvokeDelegate();

        private void Invoke_Click(object sender, EventArgs e)
        {
            label1.BeginInvoke(new InvokeDelegate(InvokeMethod));
        }

        public void InvokeMethod()
        {
            if (f == true)
                label1.BackColor = Color.Green;
            else
                label1.BackColor = Color.Yellow;
            label1.Text = Array_to_String(array);
        }



        private void Form1_Shown(object sender, EventArgs e)
        {
            inc.Start();
            dec.Start();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            flag = checkBox1.Checked;
            MessageBox.Show(inc.ThreadState.ToString());
            MessageBox.Show(dec.ThreadState.ToString());
        }





        private string Array_to_String(int[] arr)
        {
            string s = "";
            foreach (int i in arr)
                s += i.ToString() + "  ";
            return s;
        }



        private async void DescendeSort()
        {
            while (!Enumerable.SequenceEqual(array,decarray))
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (array[i] > array[j])
                        {
                            (array[i], array[j]) = (array[j], array[i]);
                            wait(300);
                            f = false;
                            Invoke_Click(this,null);
                        }
        }


        private async void IncreaseSort()
        {

            while(!Enumerable.SequenceEqual(array, incarray))
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (array[i] < array[j])
                    {
                        (array[i], array[j]) = (array[j], array[i]);
                        wait(300);
                        f = true;
                        Invoke_Click(this, null);
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

    }
}