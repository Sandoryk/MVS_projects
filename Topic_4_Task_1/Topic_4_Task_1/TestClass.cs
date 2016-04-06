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
        [TestMethod]
        public void Test_FormatCardID_GetFormat_nullReturn()
        {
            Object actualresult;

            FormatCardID fc = new FormatCardID();
            Type t = typeof(FormatCardID);
            actualresult = fc.GetFormat(t);
            Assert.Equals(actualresult, null);
        }

    }
}
