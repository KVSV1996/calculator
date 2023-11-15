
using System.Text.RegularExpressions;

namespace Calculator
{
    class ConsoleCalculator : Calculator
    {        
        protected override void RegexValidation(string expression)
        {
            string paternForConsole = @"[\/*+]{2,}|^[\/*+]|[\/*+-]$|[-]{3,}|[\.,()=]{1,}|[a-z]|\-[\/*+]|[\/+*][\-]{2,}";
            Regex regex = new Regex(paternForConsole);

            Match m = regex.Match(expression);
            if (m.Success)
            {
                throw new FormatException();
            }            
        }        
    }
}
