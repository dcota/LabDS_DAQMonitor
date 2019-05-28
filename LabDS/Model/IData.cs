using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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