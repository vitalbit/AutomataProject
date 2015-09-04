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
            string[] newGraphArray = new string[(test.States - 1) * (test.Values - 1)];
            bool?[] newFinalStates = new bool?[test.States];
            string[] newValuesArray = new string[test.Values];

            for (int i = 1; i != test.States; i++)
                newFinalStates[i] = Boolean.Parse(test.GraphArray[i][0]);

            for (int i = 1; i != test.Values; i++)
                newValuesArray[i] = test.GraphArray[0][i];

            for (int i = 1; i != test.States; i++)
            {
                for (int j = 1; j != test.Values; j++)
                    newGraphArray[(i - 1) * test.Values + (j - 1)] = test.GraphArray[i][j];
            }

            return new NewTestEntityModel()
            {
                FinalStates = newFinalStates,
                GraphArray = newGraphArray,
                Description = test.Description,
                Regex = test.Regex,
                States = test.States - 1,
                TestName = test.TestName,
                Values = test.Values - 1,
                ValuesArray = newValuesArray
            };
        }

        public static NewTestViewModel ToView(this NewTestEntityModel test)
        {
            string[][] newGraphArray = new string[test.States + 1][];

            for (int i = 0; i != test.States + 1; i++)
                newGraphArray[i] = new string[test.Values + 1];

            for (int i = 1; i != test.States + 1; i++)
                newGraphArray[i][0] = test.FinalStates[i - 1].ToString();

            for (int i = 1; i != test.Values + 1; i++)
                newGraphArray[0][i] = test.ValuesArray[i - 1];

            for (int i = 1; i != test.States + 1; i++)
                for (int j = 1; j != test.Values + 1; j++)
                    newGraphArray[i][j] = test.GraphArray[(i - 1) * test.Values + (j - 1)];

            return new NewTestViewModel()
            {
                GraphArray = newGraphArray,
                Description = test.Description,
                Regex = test.Regex,
                States = test.States,
                TestName = test.TestName,
                Values = test.Values,
            };
        }
    }
}
