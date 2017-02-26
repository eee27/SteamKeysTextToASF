using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace ToASF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            button2.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                String inputString = textBox1.Text;
                toAsfString(inputString);
            }
            else
            {
                String inputString = GetTextFromClip();
                toAsfString(inputString);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String inputString = GetTextFromClip();
            textBox1.Text = inputString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (textBox2.Text != null)
            {
                Clipboard.SetDataObject(textBox2.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            button2.Enabled = false;
        }

        /*--------------------------------------------------------------*/

        private string GetTextFromClip()
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                string inputString = (string)iData.GetData(DataFormats.Text);
                return inputString;
            }
            else
            {
                MessageBox.Show("Wrong Input Format!");
                return null;
            }
        }

        private void toAsfString(String input)
        {
            int keyNums = 0;
            string outputString = null;

            Regex reg = new Regex(@"[a-zA-Z0-9]{5}-[a-zA-Z0-9]{5}-[a-zA-Z0-9]{5}");

            if (reg.IsMatch(input))
            {
                var keyString = reg.Matches(input, 0);
                foreach (var item in keyString)
                {
                    outputString += (item + ",");
                    keyNums += 1;
                }
                outputString = outputString.Substring(0, outputString.Length - 1);
            }
            else
            {
                keyNums = 0;
            }
            label3.Text = "Total: " + keyNums + " Keys";
            textBox2.Text = outputString;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("有Bug请联系我!\n" + "SteamId:  eee27\n" + "http://steamcommunity.com/id/lb-eee27/");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}