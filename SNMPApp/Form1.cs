using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;
using SNMPApp.Properties;
using SnmpSharpNet;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;

namespace SNMPApp
{
    public partial class Form1 : Form
    {
        //список валидных хостов в подсети
        private List<string> _ipAddresses = new List<string>();
        //флаг выполнения
        private bool _process;

        public Form1()
        {
            InitializeComponent();
            //получаем список сетевых интерфейсов
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var iface in interfaces) {  
               //если это не туннель или петля, то добавляем интерфейс для использования
               if ((iface.NetworkInterfaceType!= NetworkInterfaceType.Tunnel)&&(iface.NetworkInterfaceType!= NetworkInterfaceType.Loopback)) 
                   adapterComboBox.Items.Add(iface.Description);
            }
        }

        private void AskButtonClick(object sender, EventArgs e)
        {
            //к кому будем обращаться
            string host = String.Empty;
            if (hostTextBox.Enabled)
                host = hostTextBox.Text;
            if (addressesListBox.SelectedIndex != -1 && addressesListBox.Enabled)
                    host = (string) addressesListBox.Items[addressesListBox.SelectedIndex];
            //если не все поля заполнены, то говорим, что было б неплохо заполнить
            if (host == String.Empty || communityTextBox.Text.Length == 0)
            {
                MessageBox.Show("Host or community strings are empty. Enter some values", "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            //очищаем область ответа
            responseTextBox.Text = "";
            string community = communityTextBox.Text;
            //создаем запрос
            var snmp = new SimpleSnmp(host, community);
            //если запрос не валиден, то пишем об этом
            if (!snmp.Valid)
            {
                responseTextBox.Text += Resources.Not_Valid_SNMP_HOST;
                return;
            }
            //формируем тело запроса и отсылаем его
            Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2,
                                                       new[]
                                                           {
                                                               ".1.3.6.1.2.1"/*,
                                                               ".1.3.6.1.2.1.1.1.0", // sysDescr
                                                               ".1.3.6.1.2.1.1.5.0", // sysName
                                                               ".1.3.6.1.2.1.1.3.0", // upTime
                                                               ".1.3.6.1.2.1.2.1.0", // available interfaces count
                                                               ".1.3.6.1.2.1.1.4.0", // sysContact,
                                                               ".1.3.6.1.2.1.1.6.0"  // sysLocation)
                                                               */
                                                           });

            //если нет ответа, то так и скажем
            if (result == null)
            {
                responseTextBox.Text += Resources.NoResults;
                return;
            }
            //счетчик для форматирования
            int i = 0;
            foreach (var kvp in result)
            {
                //если есть ответ, то выводим
                switch (i)
                {
                    case 1: responseTextBox.Text += "System Name:";
                        break;
                    case 2: responseTextBox.Text += "Uptime:";
                        break;
                    case 3: responseTextBox.Text += "Interfaces available:";
                        break;
                    case 4: responseTextBox.Text += "Contact:";
                        break;
                    case 5: responseTextBox.Text += "Location:";
                        break;
                };
                responseTextBox.Text += kvp.Value;
                responseTextBox.Text += "\r\n";
                i++;
                responseTextBox.Text += "===========================\r\n";
            }
            
        }


        //если выбали другой адаптер, то надо просканированть подсеть
        private void ComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            //начальная инициализация
            stopButton.Enabled = true;
            addressesListBox.Items.Clear();
            _process = true;
            adapterComboBox.Enabled = false;
            string adapterIP="";
            string adapterMask = "";
            //получаем все интерфейсы
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var iface in interfaces)
            {
                //находим наш интерфейс
                if (iface.Description == adapterComboBox.Text)
                {
                    //получаем свойства IP 
                    var properties = iface.GetIPProperties();
                    //получаем адреса
                    var uniCast = properties.UnicastAddresses;
                    if (uniCast!=null)
                    {
                        //и, наконец, вытаскиваем эти адреса
                        foreach (UnicastIPAddressInformation uni in uniCast)
                        {
                            //вытаскиваем IP
                            adapterIP = uni.Address.ToString();
                            //вытаскиваем маску подсети
                            if (uni.IPv4Mask == null)
                            {
                                adapterComboBox.Enabled = true;
                                stopButton.Enabled = false;
                                progressBar.Value = 0;
                                return;
                            }
                            adapterMask = uni.IPv4Mask.ToString();
                        }
                    }
                }
            }
            //эти вспомогательные переменные нужны, чтобы определить IP адреса, принадлежащие подсети
            var IP = new byte[4];
            var IPMask = new byte[4];
            //разбиваем IP на октеты
            //первый октет
            string tmp = adapterIP;
            tmp = tmp.Remove(tmp.IndexOf('.'));
            IP[0] = Convert.ToByte(tmp);
            //второй октет
            tmp = adapterIP;
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(tmp.IndexOf('.'));
            IP[1] = Convert.ToByte(tmp);
            //третий октет
            tmp = adapterIP;
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(tmp.IndexOf('.'));
            IP[2] = Convert.ToByte(tmp);
            //четвертый октет
            tmp = adapterIP;
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            IP[3] = Convert.ToByte(tmp);
            //разбиваем маску на октеты
            //первый октет
            tmp = adapterMask;
            tmp = tmp.Remove(tmp.IndexOf('.'));
            IPMask[0] = Convert.ToByte(tmp);
            //второй октет
            tmp = adapterMask;
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(tmp.IndexOf('.'));
            IPMask[1] = Convert.ToByte(tmp);
            //третий октет
            tmp = adapterMask;
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(tmp.IndexOf('.'));
            IPMask[2] = Convert.ToByte(tmp);
            //четвертый октет
            tmp = adapterMask;
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            tmp = tmp.Remove(0, tmp.IndexOf('.') + 1);
            IPMask[3] = Convert.ToByte(tmp);
            //представляем IP в виде long числа
            long lIP = ((IP[0] << 24) + (IP[1] << 16) + (IP[2] << 8) + IP[3]);
            //точно так же маску
            long lMask = ((IPMask[0] << 24) + (IPMask[1] << 16) + (IPMask[2] << 8) + IPMask[3]);
            //вычисляем номер подсети
            long lSubnetNum = (lIP & lMask);
            //нижний предел подсети
            long lStartIP = (lSubnetNum & lMask) + 1;
            //верхний предел подсети
            long lStopIP = (lSubnetNum | ((~lMask) - 1));
            //текущий адес
            var currIP = new byte[4];
            //выставляем погресс бар
            progressBar.Maximum = Convert.ToInt32(lStopIP - lStartIP);
            //ищем IP адреса в сети
            for (long i = lStartIP; i <= lStopIP; i++)
            {
                //если флаг выполнения сброшен, то завершаем процедуру
                if (!_process) break;
                //увеличиваем прогресс бар
                progressBar.Value = Convert.ToInt32(i - lStartIP);
                //пингуем хосты в подсети
                var ping = new Ping();
                PingOptions options = new PingOptions();
                //запрещам фрагметированный пинг
                options.DontFragment = true;
                //информация в пинге(32байта)
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                //Создаем буфер данных 
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //таймаут
                int timeout = 120;
                //текущий IP
                currIP = BitConverter.GetBytes(i);
                Application.DoEvents();
                //Собираем ИП в правильном порядке байт(не в двоично-десятичной нотации)
                IPAddress remote = new IPAddress(new byte[4]{currIP[3],currIP[2],currIP[1],currIP[0]});
                try
                {
                    //пингуем
                    PingReply reply = ping.Send(remote, timeout, buffer, options);
                    //если ответ есть, то хост в сети
                    if (reply.Status == IPStatus.Success)
                    {
                        //добавляем хост в список
                        string sIP = currIP[3].ToString() + "." + currIP[2].ToString() + "." + currIP[1].ToString() + "." + currIP[0].ToString();
                        _ipAddresses.Add(sIP);
                        addressesListBox.Items.Add(sIP);
                        addressesListBox.Invalidate();
                    }
                   
                }
                catch (Exception) { }
            }
            adapterComboBox.Enabled = true;
            stopButton.Enabled = false;
            progressBar.Value = progressBar.Maximum;
        }

        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            adapterLabel.Enabled = !checkBox.Checked;
            addressesLabel.Enabled = !checkBox.Checked;
            adapterComboBox.Enabled = !checkBox.Checked;
            addressesListBox.Enabled = !checkBox.Checked;
            hostLabel.Enabled = checkBox.Checked;
            hostTextBox.Enabled = checkBox.Checked;
        }

        private void HostTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 )
            {
                AskButtonClick(this, EventArgs.Empty);
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            //сбрасываем флаг выполнения
            _process = false;
        }

        private void addressesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string re = addressesListBox.SelectedItem.ToString();
            hostTextBox.Text = re;
        }
    }
}

