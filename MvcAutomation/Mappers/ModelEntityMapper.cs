using ModelEntityLayer;
using MvcAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcAutomation.Mappers
{
    public static class ModelEntityMapper
    {
        public static NewTestEntityModel ToEntity(this NewTestViewModel test)
        {
            return new NewTestEntityModel()
            {
                FinalStates = test.FinalStates,
                GraphArray = test.GraphArray,
                Description = test.Description,
                Regex = test.Regex,
                States = test.States,
                TestName = test.TestName,
                Values = test.Values,
                ValuesArray = test.ValuesArray
            };
        }

        public static NewTestViewModel ToView(this NewTestEntityModel test)
        {
            return new NewTestViewModel()
            {
                FinalStates = test.FinalStates,
                GraphArray = test.GraphArray,
                Description = test.Description,
                Regex = test.Regex,
                States = test.States,
                TestName = test.TestName,
                Values = test.Values,
                ValuesArray = test.ValuesArray
            };
        }
    }
}
