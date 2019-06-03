/********************************************************
 * UC 21179 - Laboratório de Desenvolvimento de Software 
 * Ano letivo: 2018/2019                                 
 * Ficheiro: IData.cs
 * Autor: Duarte Cota
 * Descrição: Definição da Interface IData
 *******************************************************/


namespace LabDS.Model
{
    public interface IData
    {
        string Temp { get; set; }
        string Press { get; set; }
        double Time { get; set; }
        double AvTemp { get; set; }
        double AvPress { get; set; }
        double AddTempValues { get; set; }
        double AddPressValues { get; set; }
        void UpdateAverageTemp(double temp, int counter);
        void UpdateAveragePress(double press, int counter);
    }
}