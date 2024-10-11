using DesignPatterns.Interfaces;
using System.ComponentModel;

namespace DesignPatterns.Patterns.Other;

/// <summary>
/// Интерпретатор.
/// </summary>
/// <remarks>
/// Определение представления грамматики и интерпретатора для этой грамматики.
/// 
/// Паттерн Интерпретатор (Interpreter) определяет представление грамматики для заданного языка и интерпретатор предложений этого языка. Как правило, данный шаблон проектирования применяется для часто повторяющихся операций.
/// 
/// <see cref="https://metanit.com/sharp/patterns/3.8.php"/>
/// </remarks>
[Description("Интерпретатор: Определение представления грамматики и интерпретатора для этой грамматики.\r\n\r\nПаттерн Интерпретатор (Interpreter) определяет представление грамматики для заданного языка и интерпретатор предложений этого языка. Как правило, данный шаблон проектирования применяется для часто повторяющихся операций.")]
internal class Interpreter : IPattern
{
    /// <summary>
    /// Интерфейс выражения.
    /// </summary>
    interface IExpression
    {
        int Interpret();
    }

    /// <summary>
    /// Числовое выражение.
    /// </summary>
    class NumberExpression : IExpression
    {
        private int _number;

        public NumberExpression(int number)
        {
            _number = number;
        }

        public int Interpret()
        {
            return _number;
        }
    }

    /// <summary>
    /// Выражение суммы.
    /// </summary>
    class SumExpression : IExpression
    {
        private IExpression _leftExpression;
        private IExpression _rightExpression;

        public SumExpression(IExpression leftExpression, IExpression rightExpression)
        {
            _leftExpression = leftExpression;
            _rightExpression = rightExpression;
        }

        public int Interpret()
        {
            return _leftExpression.Interpret() + _rightExpression.Interpret();
        }
    }

    /// <summary>
    /// Выражение вычитания.
    /// </summary>
    class SubstractExpression : IExpression
    {
        private IExpression _leftExpression;
        private IExpression _rightExpression;

        public SubstractExpression(IExpression leftExpression, IExpression rightExpression)
        {
            _leftExpression = leftExpression;
            _rightExpression = rightExpression;
        }

        public int Interpret()
        {
            return _leftExpression.Interpret() - _rightExpression.Interpret();
        }
    }

    /// <summary>
    /// Парсер выражений.
    /// </summary>
    class ExpressionParser
    {
        private int GetPrecenence(string op)
        {
            if (op == "+" || op == "-")
            {
                return 1;
            }

            return 0;
        }
        
        private bool IsOperator(string token)
        {
            return token == "+" || token == "-";
        }

        public IExpression Parse(string input)
        {
            Stack<string> operators = new Stack<string>();
            Stack<IExpression> operands = new Stack<IExpression>();
            string[] tokens = input.Split(' ');

            foreach (var token in tokens)
            {
                if (int.TryParse(token, out var number))
                {
                    operands.Push(new NumberExpression(number));
                }
                else if (IsOperator(token))
                {
                    while (operators.Count > 0 && GetPrecenence(operators.Peek()) >= GetPrecenence(token))
                    {
                        string op = operators.Pop();
                        ProcessOperator(op, operands);
                    }

                    operators.Push(token);
                }
                else
                {
                    throw new InvalidOperationException($"Неподдерживаемый токен: {token}");
                }
            }

            while (operators.Count > 0)
            {
                string op = operators.Pop();
                ProcessOperator(op, operands);
            }

            if (operands.Count != 1)
            {
                throw new InvalidOperationException("Ошибка в выражении. Стек должен содержать одно финальное выражение.");
            }

            return operands.Pop();
        }

        private void ProcessOperator(string op, Stack<IExpression> operands)
        {
            if (operands.Count < 2)
            {
                throw new InvalidOperationException("Недостаточно операндов для операции.");
            }

            IExpression right = operands.Pop();
            IExpression left = operands.Pop();

            if (op == "+")
            {
                operands.Push(new SumExpression(left, right));
            }
            else if (op == "-")
            {
                operands.Push(new SubstractExpression(left, right));
            }
        }
    }

    /// <summary>
    /// Применение паттерна проектирования.
    /// </summary>
    public void ShowExample()
    {
        ExpressionParser parser = new ExpressionParser();
        string stringExpression = "5 + 3 - 2";

        Console.WriteLine($"Выражение \"{stringExpression}\" помещается в парсер выражений.");

        IExpression expression = parser.Parse(stringExpression);

        int result = expression.Interpret();

        Console.WriteLine($"Результат выражения: {result}.");
    }
}
