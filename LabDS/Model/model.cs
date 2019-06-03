/********************************************************
 * UC 21179 - Laboratório de Desenvolvimento de Software 
 * Ano letivo: 2018/2019                                 
 * Ficheiro: model.cs
 * Autor: Duarte Cota
 * Descrição: Componente MODEL da aplicação DAQ Monitor
 *******************************************************/

using System;

namespace LabDS.Model   
{
    public class Data : IData
    {
        //campos de dados locais
        private string temp;
        private string press;
        private double time = 0;
        private double avTemp = 0;
        private double avPress = 0;
        private double sumTemp = 0;
        private double sumPress = 0;

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

        //método get/set para atualização da soma de temperaturas
        public double AddTempValues
        {
            get { return sumTemp; }
            set { sumTemp = value; }
        }

        //método get/set para atualização da soma de pressões
        public double AddPressValues
        {
            get { return sumPress; }
            set { sumPress = value; }
        }

        //método get/set para atualização da média das temperaturas
        public double AvTemp
        {
            get { return avTemp; }
            set { avTemp = value; }
        }

        //método get/set para atualização da média das temperaturas
        public double AvPress
        {
            get { return avPress; }
            set { avPress = value; }
        }

        //método para atualização da média da temperatura
        public void UpdateAverageTemp(double temp, int counter)
        {
            AddTempValues += temp;
            AvTemp = AddTempValues / counter;
        }

        //método para atualização da média da pressão
        public void UpdateAveragePress(double press, int counter)
        {
            AddPressValues += press;
            AvPress = AddPressValues / counter;
        }
    }
    
    //classe com campos e métodos de parametrização da ligação ao DAQ e alarmes
    public class Process
    {
        //criar evento para informar a View, através do Controller de um alarme de temperatura
        public event EventHandler OnAlarm = null;

        //criar evento para informar a View, através do Controller de que a temperatura normalizou
        public event EventHandler OnNoAlarm = null;

        //evento lançado sempre que nova string é processada
        public event EventHandler<ParsedStringEventArgs> OnNewStringParsed = null;

        //declaração de variáveis
        private string[] availableCOMs;
        private string selectedCOM;
        private string selectedBaudRate = " ";
        private decimal spoint = 25;
        int counter = 0;

        //criar objeto da classe Data que implementa IData
        Data data = new Data();

        //método que retorna o objeto de IData para o Controller à View
        public IData GetIDataObject()
        {
            return data;
        }

        //método para processar cada string recebida do DAQ
        public void ParseString(string RawString)
        {
            try
            {
                string[] dataSplited = RawString.Split(';');
                data.Temp = dataSplited[1];
                data.Press = dataSplited[2];
                /*após processar cada string lançar evento para informar a View, através do
                Controller, de que há novos valores de temperatura e pressão para um novo instante de tempo x */
                StringParsed(data);
                // set do próximo instante de tempo
                data.Time = data.Time + 0.05;
                //incrementar o contador de valores para efeito do calculo das médias
                counter++;
                //atualizar a média dos valores
                data.UpdateAverageTemp(Math.Round(Convert.ToDouble(data.Temp), 2, MidpointRounding.AwayFromZero), counter);
                data.UpdateAveragePress(Math.Round(Convert.ToDouble(data.Press), 2, MidpointRounding.AwayFromZero), counter);
                //verificar o estado do alarme e informar a View, através do Controller
                ChkAlarm(data.Temp);
            }
            catch (IndexOutOfRangeException)
            {
                //apanhar exceção no Model e alertar o Controller que vai ativar caixa de dialogo na View)
                throw new ModelException();
            }
        }
        
        //método invocado sempre que uma nova string é processada
        public virtual void StringParsed(IData data)
        {
            OnNewStringParsed?.Invoke(this, new ParsedStringEventArgs(data));
        }


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

        //método para verificar de temperatura >= setpoint
        public void ChkAlarm(string temp)
        {
            /*se o valor de temperatura é superior ao setpoint lançar evento Alarm, 
            se não lançar evento NoAlarm*/
            if (Convert.ToDouble(temp) >= Convert.ToDouble(SPoint))
            {
                OnAlarm?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnNoAlarm?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    //classe que define os parâmetros a passar em caso de evento de nova string processada
    public class ParsedStringEventArgs : EventArgs
    {
        public IData NewStringParsed { get; set; }
        public ParsedStringEventArgs(IData args)
        {
            NewStringParsed = args;
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