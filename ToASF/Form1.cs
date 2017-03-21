using System;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Drawing;

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

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                button2.Enabled = false;
                textBox1.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                textBox1.Enabled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            String inputString = GetTextFromClip();
            textBox1.Text = inputString;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (textBox2.Text != null)
            {
                Clipboard.SetDataObject(textBox2.Text);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            button2.Enabled = false;
        }

        /*---------------------------FUNCITON-----------------------------------*/

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

        /*----------------------TAB2-------------------------------*/

        private void button5_Click_1(object sender, EventArgs e)
        {
            string url = @"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=37AAF2CF58E058F22B2C7C0D2047E45C&steamids=" + textBox3.Text;
            ExtWebSource(GetWebSource(url));
        }

        /*----------------FUNCTION------------------*/

        private string GetWebSource(string url)
        {
            MessageBox.Show(url);
            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = stream.ReadToEnd();
            stream.Close();
            return responseText;
        }

        private void ExtWebSource(string jsonWebString)
        {
            JObject jObj = JObject.Parse(jsonWebString);

            string steamId = (string)jObj["response"]["players"][0]["steamid"];
            string personaName = (string)jObj["response"]["players"][0]["personaname"];
            string profileUrl = (string)jObj["response"]["players"][0]["profileurl"];
            string avatarUrl = (string)jObj["response"]["players"][0]["avatarmedium"];
            string createTime = (string)jObj["response"]["players"][0]["createTime"];
            string accountCountry = (string)jObj["response"]["players"][0]["loccountrycode"];

            WebClient wc = new WebClient();
            Image image = Image.FromStream(wc.OpenRead(avatarUrl));
            pictureBox1.Image = image;
            label4.Text = personaName;
        }
    }
}