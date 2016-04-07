using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace Topic_4_Task_1
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Test_FormatCardID_GetFormat_nullReturn()
        {
            Object actualresult;

            FormatCardID fc = new FormatCardID();
            Type t = typeof(FormatCardID);
            actualresult = fc.GetFormat(t);
            Assert.IsNull(actualresult);
        }
        [TestMethod]
        public void Test_FormatCardID_GetFormat_ICustomFormatterReturn()
        {
            Object actualresult;

            FormatCardID fc = new FormatCardID();
            Type t = typeof(ICustomFormatter);
            actualresult = fc.GetFormat(t);
            Assert.IsInstanceOfType(actualresult, t);
        }
        [TestMethod]
        public void Test_ClientCompare_Compare_Ascending()
        {
            int actresult;
            int expresult = -1;

            ListViewItem item1 = new ListViewItem("Иван");
            ListViewItem item2 = new ListViewItem("Сергей");
            
            ClientComparer cc = new ClientComparer(0,SortOrder.Ascending);
            actresult = cc.Compare(item1, item2);
            Assert.AreEqual(expresult, actresult);
        }
    }
}
