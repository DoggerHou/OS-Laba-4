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
        myArray massiv;

        public Form1()
        {
            InitializeComponent();
            massiv = new myArray();
            massiv.observers += new System.EventHandler(this.UpdateFromModel);
            massiv.observers.Invoke(this, null);
        }


        private void UpdateFromModel(object sender, EventArgs e)
        {
            label1.Text = Array_to_String(massiv.arr);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            flag = checkBox1.Checked;
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Thread dec = new Thread(DescendeSort);
            Thread inc = new Thread(IncreaseSort);
            dec.Start();
        }

        public void DescendeSort()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (massiv.arr[i] > massiv.arr[j])
                    {
                        (massiv.arr[i], massiv.arr[j]) = (massiv.arr[j], massiv.arr[i]);
                    }
            massiv.observers.Invoke(this, null);
        }


        public void IncreaseSort()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (massiv.arr[i] < massiv.arr[j])
                    {
                        (massiv.arr[i], massiv.arr[j]) = (massiv.arr[j], massiv.arr[i]);
                    }
            massiv.observers.Invoke(this, null);
        }


        private string Array_to_String(int[] arr)
        {
            string s = "";
            foreach (int i in arr)
                s += i.ToString() + "  ";
            return s;
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



    public class myArray
    {
        public int[] arr;
        public System.EventHandler observers;

        public myArray()
        {
            arr = new int[10];
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
                arr[i] = rnd.Next(0, 99);
        }


        public void DescendeSort1()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (arr[i] > arr[j])
                    {
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
            observers.Invoke(this, null);
        }


        public void IncreaseSort1()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (arr[i] < arr[j])
                    {
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                    }
            observers.Invoke(this, null);
        }
    }
}
