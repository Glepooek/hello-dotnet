using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmitSimpleAOPSamples
{
    // 1. 目标接口
    public interface ICalculator
    {
        int Add(int a, int b);
        int Subtract(int a, int b);
    }
}
