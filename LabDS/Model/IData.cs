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
    }
}