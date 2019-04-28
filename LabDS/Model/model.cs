using System;

namespace LabDS.Model   
{
    public interface IData
    {
        string Temp { get; set; }
        string Press { get; set; }
        double Time { get; set; }
        void ParseString(string RawString);
        event EventHandler<ParsedStringEventArgs> StringParsed;
    }

    public class Data : IData
    {
        //evento lançado sempre que nova string é processada
        public event EventHandler<ParsedStringEventArgs> StringParsed = null;

        //campos de dados locais
        private string temp;
        private string press;
        private double time = -0.05;

        //método get/set para atualização da temperatura
        public string Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        //método get/set para atualização da pressão
        public string Press
        {
            get { return press; }
            set { press = value; }
        }

        //método get/set para atualização do tempo nos gráficos (eixo x)
        public double Time
        {
            get { return time; }
            set { time = value; }
        }

        public void ParseString(string RawString)
        {
            try
            {
                string[] dados = RawString.Split(';');
                Temp  = dados[1];
                Press = dados[2];
                Time = Time + 0.05;
                /*após processar cada string lançar evento para informar a View, através do
                Controller, de que há novos valores de temperatura e pressão para um novo instante de tempo x*/
                OnNewStringParsed(Temp, Press, Convert.ToString(Time));
                //verificar o estado do alarme e informar a View, através do Controller
            }
            catch (IndexOutOfRangeException)
            {
                //apanha exceção no Model e alertar o Controller que vai ativar caixa de dialogo na View)
                throw new ModelException();
            }
        }

        public virtual void OnNewStringParsed(string temp, string press, string x)
        {
            StringParsed?.Invoke(this, new ParsedStringEventArgs(temp, press, x));
        }
    }

    //classe que define os parâmetros a passar ao Controller em caso de evento de nova string processada
    public class ParsedStringEventArgs : EventArgs
    {
        public string[] NewStringParsed { get; set; }
        public ParsedStringEventArgs(params string[] args)
        {
            NewStringParsed = args;
        }
    }

    //classe com campos e métodos de parametrização da ligação ao DAQ e alarmes
    public class Setup
    {
        //criar evento para informar a View, através do Controller de um alarme de temperatura
        public event EventHandler OnAlarm = null;

        //criar evento para informar a View, através do Controller de que a temperatura normalizou
        public event EventHandler OnNoAlarm = null;

        //declaração de variáveis
        private string[] availableCOMs;
        private string selectedCOM;
        private string selectedBaudRate = " ";
        private decimal spoint = 25;

        //método get/set para atualização das COM disponíveis
        public string[] AvailableCOMs
        {
            get { return availableCOMs; }
            set { availableCOMs = value; }
        }

        //método get/set para atualização da COM selecionada
        public string SelectedCOM
        {
            get { return selectedCOM; }
            set { selectedCOM = value; }
        }

        //método get/set para atualização do Baud Rate
        public string SelectedBaudRate
        {
            get { return selectedBaudRate; }
            set { selectedBaudRate = value; }
        }

        //método get/set para atualização do setpoint
        public decimal SPoint
        {
            get { return spoint; }
            set { spoint = value; }
        }

        //método para verificar de temperatura > setpoint
        public void ChkAlarm(string temp)
        {
            /*se o valor de temperatura é superior ao setpoint lançar evento Alarm, 
            se não lançar evento NoAlarm*/
            if (Convert.ToDouble(temp) > Convert.ToDouble(SPoint))
            {
                OnAlarm?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnNoAlarm?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    //classe que notifica o Controller das exceções apanhadas pelo Model
    public class ModelException : Exception
    {
        public ModelException()
        {
            //construtor
        }
    } 
}
 