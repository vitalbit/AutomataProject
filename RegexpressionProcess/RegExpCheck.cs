using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexpressionProcess
{
    public class RegExpCheck : IRegExpCheck
    {
        public bool isMatchesToDescription(string description, string regexp)
        {
            StringBuilder regexpFormation = new StringBuilder("(\\(|\\)|\\*|\\||\\^|{|}|\\[|\\]| |!");
            foreach (string s in description.Split('\n'))
            {
                if (s[0] == '\'' && s[2] == '\'')
                    regexpFormation.Append("|'" + s[1] + "'");
            }
            regexpFormation.Append(")+");
            Regex rgx = new Regex(regexpFormation.ToString());
            Match match = rgx.Match(regexp);
            if (match.Success && match.Value == regexp)
                return true;
            return false;
        }
    }
}
