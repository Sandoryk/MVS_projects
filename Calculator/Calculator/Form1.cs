using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{

    public partial class Form1 : Form
    {
        Add makeadd = new Add();
        Subtract makesub = new Subtract();
        Multiply makemulti = new Multiply();
        Divide makediv = new Divide();
        double fval, sval,tempval;

        public Form1()
        {
            InitializeComponent();
        }

        public void PrepareVals()
        {
            fval = Double.Parse(a_param.Text);
            sval = Double.Parse(b_param.Text);
        }


        private void add_but_Click(object sender, EventArgs e)
        {
            try
            {
                PrepareVals();
                result.Text = "" + makeadd.Calculate(fval, sval);
            }
            catch (FormatException err)
            {
                result.Text = err.Message;
            }
            catch
            {
                result.Text = "Неизвестная ошибка";
            }
        }

        private void subtract_but_Click(object sender, EventArgs e)
        {
            try
            {
                PrepareVals();
                result.Text = "" + makesub.Calculate(fval, sval);
            }
            catch (FormatException err)
            {
                result.Text = err.Message;
            }
            catch
            {
                result.Text = "Неизвестная ошибка";
            }
        }

        private void multi_but_Click(object sender, EventArgs e)
        {
            try
            {
                PrepareVals();
                result.Text = "" + makemulti.Calculate(fval, sval);
            }
            catch (FormatException err)
            {
                result.Text = err.Message;
            }
            catch
            {
                result.Text = "Неизвестная ошибка";
            }
        }

        private void devide_but_Click(object sender, EventArgs e)
        {
            try
            {
                PrepareVals();
                result.Text = "" + makediv.Calculate(fval, sval);
            }
            catch (FormatException err)
            {
                result.Text = err.Message;
            }
            catch(DivideByZeroException err)
            {
                result.Text = err.Message;
            }
            catch
            {
                result.Text = "Неизвестная ошибка";
            }
        }
    }

}

