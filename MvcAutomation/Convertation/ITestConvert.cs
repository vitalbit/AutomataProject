using MvcAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAutomation.Convertation
{
    public interface ITestConvert
    {
        NewTestViewModel getFromBytes(byte[] bytes);
        byte[] getFromNewTest(NewTestViewModel model);
    }
}
