using MvcAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters
{
    public interface ITestConverter
    {
        NewTestViewModel getFromBytes(byte[] bytes);
        byte[] getFromNewTest(NewTestViewModel model);
    }
}
