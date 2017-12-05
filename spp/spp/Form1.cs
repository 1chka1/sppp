using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace spp
{
    public partial class Form1 : Form
    {
        string URL = "https://api.coinmarketcap.com/v1/ticker/?limit=5";
        public class CryptoCurrency
        {
            public string id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string rank { get; set; }
            public string price_usd { get; set; }
            public string price_btc { get; set; }
            public string volume_usd_24h { get; set; }
            public string market_cap_usd { get; set; }
            public string available_supply { get; set; }
            public string total_supply { get; set; }
            public string max_supply { get; set; }
            public string percent_change_1h { get; set; }
            public string percent_change_24h { get; set; }
            public string percent_change_7d { get; set; }
            public string last_updated { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        public void UpdateData()
        {
            using (var WebClient = new System.Net.WebClient())
            {
                var json = WebClient.DownloadString(URL);
                dynamic cryp = JsonConvert.DeserializeObject(json);

                label1.Text = cryp[0].id;
                label2.Text = cryp[1].id;
                label3.Text = cryp[2].id;
                label4.Text = cryp[3].id;
                label5.Text = cryp[4].id;

                textBox1.Text = cryp[0].price_usd;
                textBox2.Text = cryp[1].price_usd;
                textBox3.Text = cryp[2].price_usd;
                textBox4.Text = cryp[3].price_usd;
                textBox5.Text = cryp[4].price_usd;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}
