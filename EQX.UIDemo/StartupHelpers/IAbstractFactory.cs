using EQX.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UIDemo.StartupHelpers
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}
