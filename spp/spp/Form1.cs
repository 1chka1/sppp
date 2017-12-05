using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

namespace spp
{
    public partial class Form1 : Form
    {
        string URL = "https://api.coinmarketcap.com/v1/ticker/?limit=5";
        string json;

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

        public string GetData()
        {
            using (var WebClient = new System.Net.WebClient())
            {
                var jsonnew = WebClient.DownloadString(URL);
                return jsonnew;

            }
        }
        public void UpdateData(string jsonstr)
        {
            dynamic cryp = JsonConvert.DeserializeObject(jsonstr);

            labelName1.Text = cryp[0].id;
            labelName2.Text = cryp[1].id;
            labelName3.Text = cryp[2].id;
            labelName4.Text = cryp[3].id;
            labelName5.Text = cryp[4].id;

            txtBoxValueUSD1.Text = cryp[0].price_usd;
            txtBoxValueUSD2.Text = cryp[1].price_usd;
            txtBoxValueUSD3.Text = cryp[2].price_usd;
            txtBoxValueUSD4.Text = cryp[3].price_usd;
            txtBoxValueUSD5.Text = cryp[4].price_usd;

            labelSymb1.Text = cryp[0].symbol;
            labelSymb2.Text = cryp[1].symbol;
            labelSymb3.Text = cryp[2].symbol;
            labelSymb4.Text = cryp[3].symbol;
            labelSymb5.Text = cryp[4].symbol;

            labelChanges1h1.Text = cryp[0].percent_change_1h;
            labelChanges1h2.Text = cryp[1].percent_change_1h;
            labelChanges1h3.Text = cryp[2].percent_change_1h;
            labelChanges1h4.Text = cryp[3].percent_change_1h;
            labelChanges1h5.Text = cryp[4].percent_change_1h;

            labelChanges24h1.Text = cryp[0].percent_change_24h;
            labelChanges24h2.Text = cryp[1].percent_change_24h;
            labelChanges24h3.Text = cryp[2].percent_change_24h;
            labelChanges24h4.Text = cryp[3].percent_change_24h;
            labelChanges24h5.Text = cryp[4].percent_change_24h;
        }
        /* public async void DoAll(object state)
         {
             string newjson = await Task.Factory.StartNew<string>(() => GetData(), TaskCreationOptions.LongRunning);
             this.labelChanges1h.Text = "HELLO";
             if (newjson != json)
             {
                 UpdateData(newjson);
                 json = newjson;
             }
         }*/

        private void Form1_Load(object sender, EventArgs e)
        {

            //TimerCallback tm = new TimerCallback(DoAll);
            //System.Threading.Timer timer = new System.Threading.Timer(tm, null, 0, 2000);
            

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string newjson = await Task.Factory.StartNew<string> (() => GetData());
            if (newjson != json)
            {
                UpdateData(newjson);
                json = newjson;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
