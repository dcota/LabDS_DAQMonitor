using System;
using System.Collections.Generic;

namespace LabDS.Model   
{
    public class ParsedStringEventArgs : EventArgs
    {
        public string[] NewStringParsed { get; set; }
        public ParsedStringEventArgs(params string[] args)
        {
            NewStringParsed = args;
        }
    }
    public class Dados
    {
        //criar evento para informar a View, através do Controller de um alarme de temperatura
        public event EventHandler OnAlarm = null;

        //criar evento para informar a View, através do Controller de que a temperatura normalizou
        public event EventHandler OnNoAlarm = null;

        //criar evento para informar a View, através do Controller de que a string foi processada
        public event EventHandler<ParsedStringEventArgs> StringParsed = null;

        //declaração de variáveis
        private string dataReceived;
        private string[] availableCOMS;
        private string selectedCOM;
        private string selectedBaudRate = " ";
        private int index;
        private string temp;
        private string press;
        private decimal spoint = 25;
        double x = 0;

        //método get;set para atualização da string de dados
        public string DataReceived
        {
            get { return dataReceived; }
            set { dataReceived = value; }
        }

        //método get/set para atualização das COM disponíveis
        public string[] AvailableCOMS
        {
            get { return availableCOMS; }
            set { availableCOMS = value; }
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

        //método get/set para atualização do índice da lista
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

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

        //método get/set para atualização do setpoint
        public decimal SPoint
        {
            get { return spoint; }
            set { spoint = value; }
        }

        //método get/set para atualização do valor de X (tempo) nos gráficos
        public double X
        {
            get { return x; }
            set { x = value; }
        }

        //método para verificar se o vetor de portas COM está vazio
        public void ChkAvailableCOMs(string[] avcoms)
        {
            if (avcoms.Length != 0)
            {
                AvailableCOMS = avcoms;
            }
        }

        //método para processar a string de dados
        public void ParseDados(string dadosRaw)
        {
            try
            {
                string[] dados = dadosRaw.Split(';');
                Temp = dados[1];
                Press = dados[2];
                //após processar cada string lança evento para informar a View, através do
                //Controller, de que há novos valores de temperatura e pressão para um novo instante de tempo x
                OnNewStringParsed(Temp, Press, Convert.ToString(X));
                //atualizar próximo valor de x (tempo)
                X += 0.05;
                //verificar o estado do alarme e informar a View, através do Controller
                ChkAlarm(Temp);
            }
            catch (IndexOutOfRangeException)
            {
                //apanha exceção no Model e alertar o Controller que vai ativar caixa de dialogo na View)
                throw new ModelException();
            }
        }

        //método que lança evento de nova string 
        public virtual void OnNewStringParsed(string temp, string press, string x)
        {
            StringParsed?.Invoke(this, new ParsedStringEventArgs(temp, press, x));
        }

        //método para verificar de temperatura > setpoint
        private void ChkAlarm(string temp)
        {
            //se o valor de temperatura é superior ao setpoint lançar evento Alarm, 
            //se não lançar evento NoAlarm
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
 