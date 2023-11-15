
namespace Calculator
{
    public abstract class Calculator
    {        
        protected List<double> Numbers;
        protected List<char> Signs;
        protected string Input;
               
       

        public string Calculate(string expression)
        {
            Input = expression;
            RegexValidation(expression);
            CalculationInsideBrekets(Input);
            SimpleCalculations(Input);
            return Numbers[0].ToString();
        }
        protected abstract void RegexValidation(string expression);        

        protected virtual void CalculationInsideBrekets(string expression) { }        

        protected void SimpleCalculations(string expression)
        {
            DivideString(expression);
            AddMinus();
            ArithmetiсOperation();
        }       

        private void DivideString(string expression)
        {
            Numbers = new List<double>();
            Signs = new List<char>();
            int i = 0;
            while (true)
            {
                if (expression[i] == '/' || expression[i] == '*' || expression[i] == '-' || expression[i] == '+')
                {
                    if (i < 1)
                    {
                        expression = StringManage(i, double.MaxValue, expression);
                        i = 0;
                        continue;
                    }
                    else
                    {
                        expression = StringManage(i, double.Parse(expression.Substring(0, i)), expression);
                        i = 0;
                        continue;
                    }

                }
                if (expression.Length - 1 == i)
                {
                    Numbers.Add(double.Parse(expression));
                    break;
                }
                i++;
            }            
        }

        private string StringManage(int i,double number,string expression)
        {
            Numbers.Add(number);
            Signs.Add(expression[i]);
            return expression.Remove(0, i + 1);            
        }

        private void AddMinus()
        {
            for (int i = 0; i < Numbers.Count; i++)
            {
                if (Numbers[i] == double.MaxValue)
                {
                    Numbers.Insert(i, Numbers[i + 1] * -1);
                    Numbers.RemoveRange(i + 1, 2);
                    Signs.RemoveAt(i);
                }
            }            
        }

        private void ArithmetiсOperation()
        {
            Fraction();
            Multiplication();
            Difference();
            Sum();
        }

        private void Fraction()
        {
            for (int i = 0; i < Signs.Count;)
            {
                if (Signs[i] == '/')
                {
                    if (Numbers[i + 1] == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    double fraction = Numbers[i] / Numbers[i + 1];
                    ListManage(fraction, i);
                    continue;
                }
                i++;
            }
        }
        private void Multiplication()
        {
            for (int i = 0; i < Signs.Count;)
            {
                if (Signs[i] == '*')
                {
                    double multiplication = Numbers[i] * Numbers[i + 1];
                    ListManage(multiplication, i);
                    continue;
                }
                i++;
            }
        }
        private void Difference()
        {
            for (int i = 0; i < Signs.Count;)
            {
                if (Signs[i] == '-')
                {
                    double difference = Numbers[i] - Numbers[i + 1];
                    ListManage(difference, i);
                    continue;
                }
                i++;
            }
        }
        private void Sum()
        {
            for (int i = 0; i < Signs.Count;)
            {
                if (Signs[i] == '+')
                {
                    double sum = Numbers[i] + Numbers[i + 1];
                    ListManage(sum, i);
                    continue;
                }
                i++;
            }
        }


        private void ListManage(double number, int i)
        {
            Numbers.RemoveRange(i, 2);
            Signs.RemoveAt(i);
            Numbers.Insert(i, number);
        }       

    }
    
}
