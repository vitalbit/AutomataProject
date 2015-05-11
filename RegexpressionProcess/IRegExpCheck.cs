using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexpressionProcess
{
    public interface IRegExpCheck
    {
        bool isMatchesToDescription(string description, string regexp);
    }
}
