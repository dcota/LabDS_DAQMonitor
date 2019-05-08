using System;
using System.Windows.Forms;
using LabDS.View;
using LabDS.Model;
using System.IO.Ports;


namespace LabDS
{
    public class Program
    {
        static Process process;
        static Janela monitor;
        static SerialPort port;
        //static Data data;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            process = new Process();
            //data = new Data();
            monitor = new Janela();
            port = new SerialPort();

            //customException = new CustomExceptionView();
            //subscrever evento da View de clicar no botão iniciar
            monitor.OnIniciar += Start;

            //subscrever evento da View de selecionar iniciar DAQ
            monitor.OnIniciarDAQ += StartDAQ;

            //subscrever evento da View de clicar no botão terminar
            monitor.OnTerminarDAQ += CloseDAQ;

            //subscrever evento da View de selecionar porta COM
            monitor.OnSelectCOM += SelectCOM;

            //subscrever evento da View de selecionar Baud Rate
            monitor.OnSelectBaudRate += SelectBaudRate;

            //subscrever evento da View de alteração de setpoint
            monitor.OnSetPoint += ChangeSetPoint;

            //subscrever evento da View de clicar no botão iniciar
            monitor.OnSair += Sair;

            //subscrever evento do Controller de input na porta Serial
            port.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);

            //subscrever evento do Model se deve haver alarme, em nome da View
            process.OnAlarm += monitor.Alarm;

            //subscrever evento do Model se a temperatura é normal, em nome da View 
            process.OnNoAlarm += monitor.NoAlarm;

            //subscrever evento do Model quando nova string é processada, em nome da View 
            process.StringParsed += monitor.UpdateView;

            //lançar a aplicação (consola da View)
            Application.Run(monitor);
        }

        //método invocado na subscrição do evento botão 
        //iniciar clicado, gerado pela View - obtem as COM disponíveis, mostra na View e guarda no Model
        static void Start(object sender, EventArgs e)
        {
            string[] avPorts = SerialPort.GetPortNames();
            if (avPorts.Length == 0)
            {
                throw new ViewException();
            }
            else
            {
                process.AvailableCOMs = avPorts;
                monitor.com_box.Items.AddRange(process.AvailableCOMs);
            }
        }

        //método invocado na subscrição do evento botão
        //terminar clicado, gerado pela View - encerra comunicações
        static void CloseDAQ(object sender, EventArgs e)
        {
            if (port.IsOpen)
            {
                port.Close();
                monitor.iniciarDAQ.Enabled = true;
                monitor.terminarDAQ.Enabled = false;
                monitor.reportBox.Text += "Porta COM fechada" + Environment.NewLine;
            }
        }

        //método invocado na subscrição do evento botão SAIR
        //terminar clicado, gerado pela View - sai da aplicação 
        static void Sair(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //método invocado na subscrição do evento porta COM selecionada
        //, gerado pela View - atualiza a View com o valor selecionado e guarda no Model
        static void SelectCOM(object sender, EventArgs e)
        {
            monitor.com_box.SelectedIndex = 0;
            process.SelectedCOM = monitor.com_box.Text;
            port.PortName = process.SelectedCOM;
            monitor.reportBox.Text += "Porta selecionada: " + process.SelectedCOM + Environment.NewLine;
        }

        //método invocado na subscrição do evento Baud Rate selecionada
        //, gerado pela View - guarda no Model o valor selecionado na View
        static void SelectBaudRate(object sender, EventArgs e)
        {
            process.SelectedBaudRate = monitor.baud_box.Text;
            monitor.reportBox.Text += "Baud Rate: " + process.SelectedBaudRate + Environment.NewLine;
        }

        //método invocado na subscrição do evento botão 
        //iniciar DAQ clicado, gerado pela View - inicia as comunicações 
        static void StartDAQ(object sender, EventArgs e)
        {
           //flag controla a saída do método que ocorre se a porta COM for aberta ou
           //se o utilizador escolher tentar de novo
           bool flag = false;
           do
           {
                try
                {
                    if (!port.IsOpen)
                    {
                        port.Open();
                        monitor.iniciarDAQ.Enabled = false;
                        monitor.terminarDAQ.Enabled = true;
                        monitor.reportBox.Text += "Porta COM aberta" + Environment.NewLine;
                        monitor.reportBox.Text += "A receber dados..." + Environment.NewLine;
                        flag = true;
                    }
                }
                catch (System.IO.IOException)
                {
                    //executar se o utilizador escolhe Não na caixa de diálogo -> terminar execução
                    if (!monitor.ShowException("Não foi possível aceder à porta. \n Selecione porta COM. \nTentar novamente?"))
                    {
                        Application.Exit();
                    }
                    //executar se o utilizador escolher Sim na caixa de diálogo -> sair do método e ir para a View tentar de novo
                    else
                    {
                        flag = true;
                    }
                }
           } while (flag == false);
        }

        //método invocado na subscrição do evento caixa numérica up/down
        //gerado pela View - atualiza o valor de setpoint e guarda no Model
        static void ChangeSetPoint(object sender, EventArgs e)
        {
            process.SPoint = monitor.setpoint.Value;
        }

        //método invocado quando há um evento de input na Porta Serial
        //recebe os dados e passa para o Model processar
        private static void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //flag controla a saída do método que ocorre se o parse da string tiver sucesso ou
            //se o utilizador escolher tentar de novo
            bool flag = false;
            do
            {
                try
                {
                   string newString = port.ReadLine();
                   process.ParseString(newString);
                   flag = true;
                }
                catch (ModelException)
                {
                    //executar se o utilizador escolhe Não na caixa de diálogo -> terminar execução
                    if (!monitor.ShowException("Ocorreu um erro! \n Tentar de novo? \n"))
                    {
                        Application.Exit();
                    }
                    //executar se o utilizador escolher Sim na caixa de diálogo -> sair do método e tentar novo parse
                    else
                    {
                        flag = true;
                    }
                }
            } while (flag == false);
        }
    }
    //classe que notifica a View das exceções apanhadas pelo Controller
    public class ControllerException : Exception
    {
        public ControllerException(string message)
        {
            //construtor
        }
    }
}