
using System.Text.RegularExpressions;

namespace Calculator
{
    class BracketsCalculator : Calculator
    {        
        protected override void RegexValidation(string expression)
        {
            int leftBrackets = new Regex(@"\(").Matches(expression).Count;
            int rightsBrackets = new Regex(@"\)").Matches(expression).Count;

            string paternForFile = @"[\/*+]{2,}|^[\/*+]|[\/*+-]$|[-]{3,}|[\.,=]{1,}|[a-z]|\-[\/*+]|[\/+*][\-]{2,}";            

            Regex regex = new Regex(paternForFile);
            Match m = regex.Match(expression);

            if (m.Success|| leftBrackets != rightsBrackets)
            {
                throw new FormatException();
            }            
        }

        protected override void CalculationInsideBrekets(string input)
        {          

            while(true)
            {
                int fb = input.LastIndexOf('(');
                if (fb == -1)
                {
                    break;
                }
                int lb = input.IndexOf(')', fb); 
                string brecetsIside = input.Substring(fb + 1, lb - fb - 1);
                SimpleCalculations(brecetsIside);
                input = input.Remove(fb, lb - fb + 1);
                input = input.Insert(fb, Numbers[0].ToString());

            }
            Input = input;
        }        
    }
}
