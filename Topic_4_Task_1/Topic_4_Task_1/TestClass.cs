using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Topic_4_Task_1
{
    [TestClass]
    class TestClass
    {
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void TestMethod1()
        {
            MessageBox.Show("s");
            Assert.Equals(4,3);
        }

    }
}
