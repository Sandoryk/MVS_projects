using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace Topic_5_Task_1
{
    class ProgramRun
    {
        public FileStream file;
        public StreamWriter fileoutput;
        public StreamReader fileinput;
        public string filename = "BufferFIle.txt";
        static void Main(string[] args)
        {
            ProgramRun pr = new ProgramRun();
            FileOperator custfwr = new FileOperator(20000);
            custfwr.OnOverStack = pr.OverStackMessageMethod;

            try
            {
                pr.file = new FileStream(pr.filename, FileMode.Create, FileAccess.ReadWrite);
                using (pr.fileoutput = new StreamWriter(pr.file))
                {
                    
                    custfwr.WriteDataInFile(pr.fileoutput);
                    //fileoutput.WriteLine("----------------------------------");
                    //fileoutput.Flush();
                }
                pr.file = new FileStream(pr.filename, FileMode.Open, FileAccess.ReadWrite);
                using (pr.fileinput = new StreamReader(pr.file))
                {
                    custfwr.ReadDataInFile(pr.fileinput);
                }
                Console.ReadLine();
            }
            catch (IOException Error)
            {
                Console.WriteLine(Error.Message);
            }
        }
        public int OverStackMessageMethod(object sender, OverStackEventArgs args)
        {
            int result = 0;
            MessageBoxManager.Yes = "Cancel storing";
            MessageBoxManager.No = "Left stored only";
            MessageBoxManager.Register();
            DialogResult dialogResult = MessageBox.Show("File size reached  " + args.FileSize + " bytes.\nChoose action", "OverStack happened", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                result = 1;
            }
            return result;
        }
    }
}
