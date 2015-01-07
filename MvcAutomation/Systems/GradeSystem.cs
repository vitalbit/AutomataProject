using MvcAutomation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAutomation.Systems
{
    public static class GradeSystem
    {
        public static double GradeTest(NewTestViewModel user_answer, NewTestViewModel right_answer)
        {
            int same = 0;
            int right_val = 0;
            foreach(var val in right_answer.ValuesArray)
            {
                int user_val = user_answer.ValuesArray.ToList().IndexOf(val);
                if (user_val >= 0)
                {
                    for (int i = 0; i != Math.Min(user_answer.States, right_answer.States); i++)
                        if (user_answer.GraphArray[i * user_answer.Values + user_val] ==
                            right_answer.GraphArray[i * right_answer.Values + right_val])
                            ++same;
                }
                ++right_val;
            }
            for (int i = 0; i != Math.Min(user_answer.States, right_answer.States); i++)
                if (user_answer.FinalStates[i] == right_answer.FinalStates[i])
                    ++same;
            return (double)same / (right_answer.States * right_answer.Values + right_answer.States) * 10;
        }
    }
}