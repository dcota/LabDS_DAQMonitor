using System;
using System.Drawing;
using System.Windows.Forms;
using LabDS.View;
using LabDS.Model;
using System.IO.Ports;


namespace LabDS
{
    public class Program
    {
        static Dados dados;
        static Janela monitor;
        static SerialPort port;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            dados = new Dados();
            monitor = new Janela();
            port = new SerialPort();
            //subscrever evento da View de clicar no botão iniciar
            monitor.OnIniciar += Started;

            //subscrever evento da View de selecionar iniciar DAQ
            monitor.OnIniciarDAQ += StartedDAQ;

            //subscrever evento da View de clicar no botão terminar
            monitor.OnTerminarDAQ += Closed;

            //subscrever evento da View de selecionar porta COM
            monitor.OnSelectCOM += SelectedCOM;

            //subscrever evento da View de selecionar Baud Rate
            monitor.OnSelectBaudRate += SelectedBaudRate;

            //subscrever evento da View de alteração de setpoint
            monitor.OnSetPoint += ChangedSetPoint;

            //subscrever evento da View de clicar no botão iniciar
            monitor.OnSair += Sair;

            //subscrever evento do Controller de input na porta Serial
            port.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);

            //subscrever evento do Model se deve haver alarme, em nome da View
            dados.OnAlarm += monitor.Alarm;

            //subscrever evento do Model se a temperatura é normal, em nome da View 
            dados.OnNoAlarm += monitor.NoAlarm;

            //subscrever evento do Model quando nova string é processada, em nome da View 
            dados.StringParsed += monitor.UpdateView;

            //lançar a aplicação (consola da View)
            Application.Run(monitor);
        }

        //método invocado na subscrição do evento botão 
        //iniciar clicado, gerado pela View - obtem as COM disponíveis, mostra na View e guarda no Model
        static void Started(object sender, EventArgs e)
        {
           dados.AvailableCOMS = SerialPort.GetPortNames();
           monitor.com_box.Items.AddRange(dados.AvailableCOMS);
        }

        //método invocado na subscrição do evento botão
        //terminar clicado, gerado pela View - encerra comunicações
        static void Closed(object sender, EventArgs e)
        {
            port.Close();
            monitor.reportBox.Text += "Porta COM fechada" + Environment.NewLine;
        }

        //método invocado na subscrição do evento porta COM selecionada
        //, gerado pela View - atualiza a View com o valor selecionado e guarda no Model
        static void SelectedCOM(object sender, EventArgs e)
        {
            monitor.com_box.SelectedIndex = 0;
            dados.SelectedCOM = monitor.com_box.Text;
            port.PortName = dados.SelectedCOM;
            monitor.reportBox.Text += "Porta selecionada: " + dados.SelectedCOM + Environment.NewLine;
        }

        //método invocado na subscrição do evento Baud Rate selecionada
        //, gerado pela View - guarda no Model o valor selecionado na View
        static void SelectedBaudRate(object sender, EventArgs e)
        {
            dados.SelectedBaudRate = monitor.baud_box.Text;
            monitor.reportBox.Text += "Baud Rate: " + dados.SelectedBaudRate + Environment.NewLine;
        }

        //método invocado na subscrição do evento botão 
        //iniciar DAQ clicado, gerado pela View - inicia as comunicações 
        static void StartedDAQ(object sender, EventArgs e)
        {
            if (!port.IsOpen)
            {
                port.Open();
                monitor.reportBox.Text += "Porta COM aberta" + Environment.NewLine;
                monitor.reportBox.Text += "A receber dados..." + Environment.NewLine;
            }
        }

        //método invocado na subscrição do evento caixa numérica up/down
        //gerado pela View - atualiza o valor de setpoint e guarda no Model
        static void ChangedSetPoint(object sender, EventArgs e)
        {
            dados.SPoint = monitor.setpoint.Value;
        }

        //método invocado na subscrição do evento botão SAIR
        //terminar clicado, gerado pela View - sai da aplicação 
        static void Sair(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //método invocado quando há um evento de input na Porta Serial
        //recebe os dados e passa para o Model processar
        private static void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string dadosIn = port.ReadLine(); //recebe a string que chega via Serial Port
            dados.ParseDados(dadosIn); //envia para o Model fazer parse da string
        }
    }
 }