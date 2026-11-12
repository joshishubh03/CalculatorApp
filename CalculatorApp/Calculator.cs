namespace CalculatorApp;

public class Calculator
{
    public int Add(int firstNumber, int secondNumber)
    {
        return firstNumber + secondNumber;
    }

    public int Subtract(int firstNumber, int secondNumber)
    {
        return firstNumber - secondNumber;
    }

    public int Multiply(int firstNumber, int secondNumber)
    {
        return firstNumber * secondNumber;
    }

    public double Divide(double dividend, double divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException(
                "Cannot divide by zero.");
        }

        return dividend / divisor;
    }
}
