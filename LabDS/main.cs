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
            //subscrever evento da view de clicar no botão iniciar
            monitor.OnIniciar += Started;
            //subscrever evento da view de selecionar iniciar DAQ
            monitor.OnIniciarDAQ += StartedDAQ;
            //subscrever evento da view de clicar no botão terminar
            monitor.OnTerminarDAQ += Closed;
            //subscrever evento da view de selecionar porta COM
            monitor.OnSelectCOM += SelectedCOM;
            //subscrever evento da view de selecionar Baud Rate
            monitor.OnSelectBaudRate += SelectedBaudRate;
            //subscrever evento da view de alteração de setpoint
            monitor.OnSetPoint += ChangedSetPoint;
            //subscrever evento da view de clicar no botão iniciar
            monitor.OnSair += Sair;
            //subscrever evento do controller de input na porta Serial
            port.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
            //subscrever evento do model de temperatura acima do setpoint
            dados.OnAlarm += Alarm;
            //subscrever evento do model de temperatura normal
            dados.OnNoAlarm += NoAlarm;
            //lançar a aplicação (consola da view)
            dados.OnParsedString += UpdateView;
            //lançar a aplicação (consola da view)
            Application.Run(monitor);
        }

        //método invocado na subscrição do evento botão 
        //iniciar clicado, gerado pela view - obtem as COM disponíveis, mostra na view e guarda no model
        static void Started(object sender, EventArgs e)
        {
           dados.AvailableCOMS = SerialPort.GetPortNames();
           monitor.com_box.Items.AddRange(dados.AvailableCOMS);
           
        }
        //método invocado na subscrição do evento botão
        //terminar clicado, gerado pela view - sai da aplicação manda o model guardar 
        //a lista de dados num ficheiro
        static void Closed(object sender, EventArgs e)
        {
            port.Close();
            monitor.reportBox.Text += "Porta COM fechada" + Environment.NewLine;
        }
        //método invocado na subscrição do evento porta COM selecionada
        //, gerado pela view - atualiza a view com o valor selecionado e guarda no model
        static void SelectedCOM(object sender, EventArgs e)
        {
            monitor.com_box.SelectedIndex = 0;
            dados.SelectedCOM = monitor.com_box.Text;
            port.PortName = dados.SelectedCOM;
            monitor.reportBox.Text += "Porta selecionada: " + dados.SelectedCOM + Environment.NewLine;
        }
        //método invocado na subscrição do evento Baud Rate selecionada
        //, gerado pela view - guarda no model o valor selecionado na view
        static void SelectedBaudRate(object sender, EventArgs e)
        {
            dados.SelectedBaudRate = monitor.baud_box.Text;
            monitor.reportBox.Text += "Baud Rate: " + dados.SelectedBaudRate + Environment.NewLine;
        }
        //método invocado na subscrição do evento botão 
        //iniciar DAQ clicado, gerado pela view - inicia as comunicações 
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
        //gerado pela view - atualiza o valor de setpoint e guarda no model
        static void ChangedSetPoint(object sender, EventArgs e)
        {
            dados.SPoint = monitor.setpoint.Value;
        }
        static void Sair(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //método invocado quando há um evento de input na Porta Serial
        //recebe os dados e passa para o model processar
        private static void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string dadosIn = port.ReadLine(); //recebe a string que chega via Serial Port
            dados.ParseDados(dadosIn); //envia para o model fazer parse da string
        }
        //método invocado quando há um evento lançado pelo model (novo par de pontos)
        static void UpdateView(object sender, EventArgs e)
        {
            ShowTemp(dados.Temp); //envia a temperatura atualizada para a view
            ShowPress(dados.Press); //envia a pressão atualizada para a view
            TempPlot(); //atualiza o gráfico na view
            PressPlot(); //atualiza o gráfico na view
            dados.X += 0.05;//incrementa X para o próximo ponto
        }

        //delegado e método de callback invocado pelo controller para atualizar a temperatura na view
        delegate void UpdateTempBox(string temp);
            static void ShowTemp(string temp)
            {
                if (monitor.textBoxTemp.InvokeRequired)
                {
                    UpdateTempBox t = new UpdateTempBox(ShowTemp);
                    monitor.textBoxTemp.Invoke(t, new Object[] { temp });
                }
                else
                {
                    monitor.textBoxTemp.Text = temp;
                }
            }
        //delegado e método de callback invocado pelo controller para atualizar a pressão na view
        delegate void UpdatePressBox(string press);
        static void ShowPress(string press)
        {
            if (monitor.textBoxPress.InvokeRequired)
            {
                UpdatePressBox p = new UpdatePressBox(ShowPress);
                monitor.textBoxPress.Invoke(p, new Object[] { press });
            }
            else
                monitor.textBoxPress.Text = press;
        }
        //delegado e método de callback invocado pelo controller para atualizar o gráfico da temperatura na view
        delegate void UpdateTempGraph();
        static void TempPlot()
        {
            if (monitor.tempGrph.InvokeRequired)
            {
                UpdateTempGraph t = new UpdateTempGraph(TempPlot);
                monitor.tempGrph.Invoke(t, new Object[] { });
            }
            else
            {
                monitor.UpdateGraphTemp(dados.X, dados.Temp); //envia novo ponto para a view  atualizar o gráfico
                //dados.X += 0.05;//incrementa X para o próximo ponto
                monitor.tempGrph.Refresh();
            }
        }
        //delegado e método de callback invocado pelo controller para atualizar o gráfico da pressão na view
        delegate void UpdatePressGraph();
        static void PressPlot()
        {
            if (monitor.pressGrph.InvokeRequired)
            {
                UpdatePressGraph p = new UpdatePressGraph(PressPlot);
                monitor.pressGrph.Invoke(p, new Object[] { });
            }
            else
            {
                monitor.UpdateGraphPress(dados.X, dados.Press); //envia novo ponto para a view atualizar o gráfico
                //dados.X += 0.05;//incrementa X para o próximo ponto
                monitor.pressGrph.Refresh();
            }
        }
        //método chamado aquando do evento de alarme (temperatura>setpoint)
        static void Alarm(object sender, EventArgs e)
        {
            UpdateAlarm();
        }
        //método chamado aquando do evento de fim de alarme
        static void NoAlarm(object sender, EventArgs e)
        {
            UpdateNoAlarm();
        }
        //delegado e método de callback do controller para assinalar temperatura normal
        delegate void ShowNorm();
        static void UpdateNoAlarm()
        {
            if (monitor.alarmBox.InvokeRequired)
            {
                ShowNorm n = new ShowNorm(UpdateNoAlarm);
                monitor.alarmBox.Invoke(n, new object[] { });
            }
            else
            {
                monitor.alarmBox.BackColor = Color.White;
                monitor.alarmBox.Text = "";
            }
        }
        //delegado e método de callback do controller para assinalar informação de ALARME de temperatura
        delegate void ShowAlarm();
        static void UpdateAlarm()
        {
            if (monitor.alarmBox.InvokeRequired)
            {
                ShowAlarm a = new ShowAlarm(UpdateAlarm);
                monitor.alarmBox.Invoke(a, new object[] { });
            }
            else
            {
                monitor.alarmBox.BackColor = Color.Red;
                monitor.alarmBox.ForeColor = Color.White;
                monitor.alarmBox.Font = new Font("Georgia", 16, FontStyle.Bold);
                monitor.alarmBox.Text = "ALARME! TEMPERATURA EXCESSIVA";
            }
        }
    }
 }