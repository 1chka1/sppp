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
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;

namespace spp
{
    public partial class FormCurr : Form
    {
        string URL = "https://api.coinmarketcap.com/v1/ticker/?limit=5";
        string json;

        public FormCurr()
        {
            InitializeComponent();
        }

        public string GetData()
        {
            try
            {
                using (var WebClient = new System.Net.WebClient())
                {
                    var jsonnew = WebClient.DownloadString(URL);
                    return jsonnew;
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return json;
            }
        }
        public async void UpdateData(string jsonstr)
        {
            dynamic cryp = JsonConvert.DeserializeObject(jsonstr);

            labelName1.Text = await Task.Factory.StartNew<string>(() => cryp[0].id);
            labelName2.Text = await Task.Factory.StartNew<string>(() => cryp[1].id);
            labelName3.Text = await Task.Factory.StartNew<string>(() => cryp[2].id);
            labelName4.Text = await Task.Factory.StartNew<string>(() => cryp[3].id);
            labelName5.Text = await Task.Factory.StartNew<string>(() => cryp[4].id);

            txtBoxValueUSD1.Text = await Task.Factory.StartNew<string>(() => cryp[0].price_usd);
            txtBoxValueUSD2.Text = await Task.Factory.StartNew<string>(() => cryp[1].price_usd);
            txtBoxValueUSD3.Text = await Task.Factory.StartNew<string>(() => cryp[2].price_usd);
            txtBoxValueUSD4.Text = await Task.Factory.StartNew<string>(() => cryp[3].price_usd);
            txtBoxValueUSD5.Text = await Task.Factory.StartNew<string>(() => cryp[4].price_usd);

            labelSymb1.Text = await Task.Factory.StartNew<string>(() => cryp[0].symbol);
            labelSymb2.Text = await Task.Factory.StartNew<string>(() => cryp[1].symbol);
            labelSymb3.Text = await Task.Factory.StartNew<string>(() => cryp[2].symbol);
            labelSymb4.Text = await Task.Factory.StartNew<string>(() => cryp[3].symbol);
            labelSymb5.Text = await Task.Factory.StartNew<string>(() => cryp[4].symbol);

            labelChanges1h1.Text = await Task.Factory.StartNew<string>(() => cryp[0].percent_change_1h);
            labelChanges1h2.Text = await Task.Factory.StartNew<string>(() => cryp[1].percent_change_1h);
            labelChanges1h3.Text = await Task.Factory.StartNew<string>(() => cryp[2].percent_change_1h);
            labelChanges1h4.Text = await Task.Factory.StartNew<string>(() => cryp[3].percent_change_1h);
            labelChanges1h5.Text = await Task.Factory.StartNew<string>(() => cryp[4].percent_change_1h);

            labelChanges24h1.Text = await Task.Factory.StartNew<string>(() => cryp[0].percent_change_24h);
            labelChanges24h2.Text = await Task.Factory.StartNew<string>(() => cryp[1].percent_change_24h);
            labelChanges24h3.Text = await Task.Factory.StartNew<string>(() => cryp[2].percent_change_24h);
            labelChanges24h4.Text = await Task.Factory.StartNew<string>(() => cryp[3].percent_change_24h);
            labelChanges24h5.Text = await Task.Factory.StartNew<string>(() => cryp[4].percent_change_24h); 

            labelLastUpdate.Text = "Last update:\n"+DateTime.Now.ToString();

            MessageBox.Show("Succesfuly updated", "Nice", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void Form1_Load(object sender, EventArgs e)
        {                
                string newjson = GetData();
                json = newjson;
                UpdateData(newjson);                 
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string newjson = await Task.Factory.StartNew<string>(() => GetData());
            if (newjson != json)
            {
                UpdateData(newjson);
                json = newjson;
            }
            else
            {
                labelLastUpdate.Text = "Last update:\n" + DateTime.Now.ToString();
                MessageBox.Show("You have the latest information", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                dynamic cryp = JsonConvert.DeserializeObject(json);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("chaoproduction228@gmail.com", "ChKA");
                mail.To.Add(new MailAddress(txtBoxEmailTo.Text));
                mail.Subject = "Курс криптовалют";
                string message = cryp[0].symbol + " - " + cryp[0].price_usd + "$ \n"
                    + cryp[1].symbol + " - " + cryp[1].price_usd + "$ \n"
                    + cryp[2].symbol + " - " + cryp[2].price_usd + "$ \n"
                    + cryp[3].symbol + " - " + cryp[3].price_usd + "$ \n"
                    + cryp[4].symbol + " - " + cryp[4].price_usd + "$ \n";
                mail.Body = message;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("chaoproduction228@gmail.com", "asdasdasd1");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch 
            {
                MessageBox.Show("Something went wrong :(", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(this, EventArgs.Empty);
        }
    }
}
