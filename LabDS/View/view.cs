using System;
using System.Drawing;
using System.Windows.Forms;
using LabDS.Model;
using ZedGraph;
using LabDS.View;

namespace LabDS.View
{
    public partial class Janela: Form
    {
        //instancia objetos das classes da API Zedgraph
        GraphPane TempGraph = new GraphPane();
        GraphPane PressGraph = new GraphPane();
        PointPairList listPointsTemp = new PointPairList();
        PointPairList listPointsPress = new PointPairList();
        LineItem myLineTemp;
        LineItem myLinePress;

        //criar evento para informar os subscritores que houve um click no botão iniciar
        public event EventHandler OnIniciar = null;

        //criar evento para informar os subscritores que houve um click no botão terminar
        public event EventHandler OnTerminarDAQ = null;

        //criar evento para informar os subscritores que houve uma seleção de COM
        public event EventHandler OnSelectCOM = null;

        //criar evento para informar os subscritores que houve uma seleção de COM
        public event EventHandler OnSelectBaudRate = null;

        //criar evento para informar os subscritores que houve uma seleção de COM
        public event EventHandler OnIniciarDAQ = null;

        //criar evento para informar os subscritores que houve uma alteração no setpoint
        public event EventHandler OnSetPoint = null;

        //criar evento para informar os sbscritores que houve um click no botão SAIR
        public event EventHandler OnSair = null;

        //criar evento para informar os subscritores que houve um click no botão Não
        //da caixa de díalogo após exceção (não deseja continuar);
        public event EventHandler OnNoButton = null;

        //iniciar a consola
        public Janela()
        {
            InitializeComponent();
            CreateTempGraph();
            CreatePressGraph();
        }

        //método invocado na criação da consola para desenhar o gráfico de temperatura
        public void CreateTempGraph()
        {
            TempGraph = tempGrph.GraphPane;
            TempGraph.Title.Text = "Temperatura (t)";
            TempGraph.XAxis.Title.Text = "Tempo (s)";
            TempGraph.YAxis.Title.Text = "Graus (C)";
            TempGraph.YAxis.Scale.Min = 0;
            TempGraph.YAxis.Scale.Max = 40;
            //TempGraph.Chart.Fill = new Fill(Color.White, Color.LightBlue, 45.0f);
            myLineTemp = TempGraph.AddCurve(null, listPointsTemp, Color.Red, SymbolType.Square);
            myLineTemp.Line.Width = 1;
        }

        //método invocado na criação da consola para desenhar o gráfico de pressão
        public void CreatePressGraph()
        {
            PressGraph = pressGrph.GraphPane;
            PressGraph.Title.Text = "Pressão Atmosférica (t)";
            PressGraph.XAxis.Title.Text = "Tempo (s)";
            PressGraph.YAxis.Title.Text = "hPa";
            PressGraph.YAxis.Scale.Min = 800;
            PressGraph.YAxis.Scale.Max = 1200;
            //PressGraph.Chart.Fill = new Fill(Color.White, Color.LightBlue, 45.0f);
            myLinePress = PressGraph.AddCurve(null, listPointsPress, Color.Red, SymbolType.Square);
            myLinePress.Line.Width = 1;
        }

        //método invocado pelo Controller para atualizar o gráfico de temperatura
        public void UpdateGraphTemp(double time, string temp)
        {
            listPointsTemp.Add(new PointPair(time, Convert.ToDouble(temp)));
            TempGraph.XAxis.Scale.Max = time;
            TempGraph.AxisChange();
        }

        //método invocado pelo Controller para atualizar o gráfico de pressão
        public void UpdateGraphPress(double time, string press)
        {
            listPointsPress.Add(new PointPair(time, Convert.ToDouble(press)));
            PressGraph.XAxis.Scale.Max = time;
            PressGraph.AxisChange();
        }

        //método que lança o evento da View de click no botão Iniciar
        private void Iniciar_Click(object sender, EventArgs e)
        {
            try
            {
                OnIniciar?.Invoke(sender, e);
            }
            catch (ViewException)
            {
                ShowException("Não há portas COM disponíveis. \n Tentar de novo?\n");
            } 
        }

        //método que lança o evento da View de click no botão iniciar receção de dados
        private void IniciarDAQ_Click(object sender, EventArgs e)
        {
            OnIniciarDAQ?.Invoke(sender, e);
        }

        //método que lança o evento da View de click no botão terminar
        private void TerminarDAQ_Click(object sender, EventArgs e)
        {
            OnTerminarDAQ?.Invoke(sender, e);
        }

        //método que lança o evento da View de seleção do baud rate
        private void OnSelectBaudRateEvent(object sender, EventArgs e)
        {
            OnSelectBaudRate?.Invoke(sender, e);
        }

        //método que lança o evento da View de seleção da porta COM
        private void Com_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectCOM?.Invoke(sender, e);
        }

        //método que lança o evento da View de alteração do setpoint
        private void Setpoint_ValueChanged(object sender, EventArgs e)
        {
            OnSetPoint?.Invoke(sender, e);
        }

        //método que lança o evento da View de click no botão SAIR
        private void Terminated_Click(object sender, EventArgs e)
        {
            OnSair?.Invoke(sender, e);
        }

        //método invocado quando há um evento lançado pelo Model (novo par de pontos)
        public void UpdateView(object o, ParsedStringEventArgs e)
        {
            ShowTemp(e.NewStringParsed[0]); 
            ShowPress(e.NewStringParsed[1]);
            TempPlot(e.NewStringParsed[2], e.NewStringParsed[0]); //atualiza o gráfico na View
            PressPlot(e.NewStringParsed[2], e.NewStringParsed[1]); //atualiza o gráfico na View
        }

        //método chamado aquando do evento do Model de alarme (temperatura>setpoint)
        public void Alarm(object sender, EventArgs e)
        {
            UpdateAlarm();
        }

        //método chamado aquando do evento do Model de fim de alarme
        public void NoAlarm(object sender, EventArgs e)
        {
            UpdateNoAlarm();
        }

        //delegado e método de callback para assinalar temperatura normal
        delegate void ShowNorm();
        private void UpdateNoAlarm()
        {
            if (alarmBox.InvokeRequired)
            {
                ShowNorm n = new ShowNorm(UpdateNoAlarm);
                alarmBox.Invoke(n, new object[] { });
            }
            else
            {
                alarmBox.BackColor = Color.White;
                alarmBox.Text = "";
            }
        }

        //delegado e método de callback para assinalar informação de ALARME de temperatura
        delegate void ShowAlarm();
        private void UpdateAlarm()
        {
            if (alarmBox.InvokeRequired)
            {
                ShowAlarm a = new ShowAlarm(UpdateAlarm);
                alarmBox.Invoke(a, new object[] { });
            }
            else
            {
                alarmBox.BackColor = Color.Red;
                alarmBox.ForeColor = Color.White;
                alarmBox.Font = new Font("Georgia", 16, FontStyle.Bold);
                alarmBox.Text = "ALARME! TEMPERATURA EXCESSIVA";
            }
        }

        //delegado e método de callback para atualizar a temperatura na View
        delegate void UpdateTempBox(string temp);
        private void ShowTemp(string temp)
        {
            if (textBoxTemp.InvokeRequired)
            {
                UpdateTempBox t = new UpdateTempBox(ShowTemp);
                textBoxTemp.Invoke(t, new Object[] { temp});
            }
            else
            {
                textBoxTemp.Text = temp;
            }
        }

        //delegado e método de callback para atualizar a pressão na View
        delegate void UpdatePressBox(string press);
        private void ShowPress(string press)
        {
            if (textBoxPress.InvokeRequired)
            {
                UpdatePressBox p = new UpdatePressBox(ShowPress);
                textBoxPress.Invoke(p, new Object[] { press });
            }
            else
                textBoxPress.Text = press;
        }

        //delegado e método de callback para atualizar o gráfico da temperatura na View
        delegate void UpdateTempGraph(string x, string temp);
        private void TempPlot(string x, string temp)
        {
            if (tempGrph.InvokeRequired)
            {
                UpdateTempGraph t = new UpdateTempGraph(TempPlot);
                tempGrph.Invoke(t, new Object[] {x,temp});
            }
            else
            {
                UpdateGraphTemp(Convert.ToDouble(x), temp); //envia novo ponto para a View  atualizar o gráfico
                tempGrph.Refresh();
            }
        }

        //delegado e método de callback para atualizar o gráfico da pressão na View
        delegate void UpdatePressGraph(string x, string press);
        private void PressPlot(string x, string press)
        {
            if (pressGrph.InvokeRequired)
            {
                UpdatePressGraph p = new UpdatePressGraph(PressPlot);
                pressGrph.Invoke(p, new Object[] {x,press}); 
            }
            else
            {
                UpdateGraphPress(Convert.ToDouble(x), press); //envia novo ponto para a View atualizar o gráfico
                pressGrph.Refresh();
            }
        }

        public void ShowException(string message)
        {
            string title = "";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No)
            {
                NoButtonEvent(null, EventArgs.Empty);
            }
        }

        public void NoButtonEvent(object sender, EventArgs e)
        {
            OnNoButton?.Invoke(sender, e);
        }
    }

    //classe que lida com as exceções apanhadas pelo Controller
    public class ViewException : Exception
    {
        public ViewException()
        {
            //construtor
        }
    }
}
