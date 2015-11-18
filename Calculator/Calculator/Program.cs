using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public interface IAction
    {
        double Calculate(double a, double b);
    }

    public abstract class Action : IAction
    {
        public abstract double Calculate(double a, double b);
    }

    public class Add : Action
    {
        public override double Calculate(double a, double b)
        {
            return a + b;
        }
    }

    public class Subtract : Action
    {
        public override double Calculate(double a, double b)
        {
            return a - b;
        }
    }

    public class Multiply : Action
    {
        public override double Calculate(double a, double b)
        {
            return a * b;
        }
    }

    public class Divide : Action
    {
        public override double Calculate(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException();
            return a / b;
        }
    }
}
