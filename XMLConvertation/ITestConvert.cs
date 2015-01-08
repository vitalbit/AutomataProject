using ModelEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLConvertation
{
    public interface ITestConvert
    {
        NewTestEntityModel getFromBytes(byte[] bytes);
        byte[] getFromNewTest(NewTestEntityModel model);
    }
}
