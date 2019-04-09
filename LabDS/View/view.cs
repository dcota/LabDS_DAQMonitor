using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using ZedGraph;

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
            TempGraph.Chart.Fill = new Fill(Color.White, Color.LightBlue, 45.0f);
            myLineTemp = TempGraph.AddCurve(null, listPointsTemp, Color.Red, SymbolType.Square);
            myLineTemp.Line.Width = 1;
            //myLineTemp.Line.Fill = new Fill(Color.White, Color.Red, 45F);
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
            PressGraph.Chart.Fill = new Fill(Color.White, Color.LightBlue, 45.0f);
            myLinePress = PressGraph.AddCurve(null, listPointsPress, Color.Red, SymbolType.Square);
            myLinePress.Line.Width = 1;
            //myLinePress.Line.Fill = new Fill(Color.White, Color.Red, 45F);
        }

        //método invocado pelo controller para atualizar o gráfico de temperatura
        public void UpdateGraphTemp(double time, string temp)
        {
            listPointsTemp.Add(new PointPair(time, Convert.ToDouble(temp)));
            TempGraph.XAxis.Scale.Max = time;
            TempGraph.AxisChange();
        }

        //método invocado pelo controller para atualizar o gráfico de pressão
        public void UpdateGraphPress(double time, string press)
        {
            listPointsPress.Add(new PointPair(time, Convert.ToDouble(press)));
            PressGraph.XAxis.Scale.Max = time;
            PressGraph.AxisChange();
        }

        //método que lança o evento da view de click no botão Iniciar
        private void Iniciar_Click(object sender, EventArgs e)
        {
            iniciar.Enabled = false;
            terminarDAQ.Enabled = true;
            OnIniciar?.Invoke(sender, e);
        }

        //método que lança o evento da view de click no botão iniciar receção de dados
        private void IniciarDAQ_Click(object sender, EventArgs e)
        {
            iniciarDAQ.Enabled = false;
            terminarDAQ.Enabled = true;
            OnIniciarDAQ?.Invoke(sender, e);
        }

        //método que lança o evento da view de click no botão terminar
        private void TerminarDAQ_Click(object sender, EventArgs e)
        {
            iniciarDAQ.Enabled = true;
            terminarDAQ.Enabled = false;
            OnTerminarDAQ?.Invoke(sender, e);
        }

        //método que lança o evento da view de seleção do baud rate
        private void OnSelectBaudRateEvent(object sender, EventArgs e)
        {
            OnSelectBaudRate?.Invoke(sender, e);
        }

        //método que lança o evento da view de seleção da porta COM
        private void Com_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectCOM?.Invoke(sender, e);
        }

        //método que lança o evento da view de alteração do setpoint
        private void Setpoint_ValueChanged(object sender, EventArgs e)
        {
            OnSetPoint?.Invoke(sender, e);
        }
        //método que lança o evento da view de click no botão SAIR
        private void Terminated_Click(object sender, EventArgs e)
        {
            OnSair?.Invoke(sender, e);
        }
    }
}
