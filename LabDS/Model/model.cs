using System;
using System.Collections.Generic;

namespace LabDS.Model
{
    public class Dados
    {
        //criar evento para informar o controller de um alarme de temperatura
        public event EventHandler OnAlarm = null;
        //criar evento para informar o controller de que a temperatura normalizou
        public event EventHandler OnNoAlarm = null;
        //criar evento para informar o controler de que a string foi processada
        public event EventHandler OnParsedString = null;
        //declaração de variáveis
        private string dataReceived;
        private string[] availableCOMS;
        private string selectedCOM;
        private string selectedBaudRate;
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
        //método para processar a string de dados
        public void ParseDados(string dadosRaw)
        {
            string[] dados = dadosRaw.Split(';');
            Temp = dados[1];
            Press = dados[2];
            //verifica se o novo valor de temperatura é superior ao setpoint
            ChkAlarm(Temp);
            //após processar a string lança evento para informar o controller que há novos valores
            //de temperatura e pressão
            OnParsedString?.Invoke(this, EventArgs.Empty);
        }
        private void ChkAlarm(string Temp)
        {
            //se o valor de temperatura é superior ao setpoint lançar evento Alarm, 
            //se não lançar evento NoAlarm
            if (Convert.ToDouble(Temp) > Convert.ToDouble(SPoint))
            {
                OnAlarm?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnNoAlarm?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
 