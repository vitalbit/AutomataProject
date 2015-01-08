using ModelEntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems
{
    public interface IGradeSystem
    {
        double GradeTest(NewTestEntityModel user_answer, NewTestEntityModel right_answer);
    }
}
