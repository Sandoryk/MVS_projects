using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Topic_5_Task_1
{
    class FileOperator
    {
        long maxlen;

        public delegate int OverStackHandler(object sender, OverStackEventArgs e);

        public OverStackHandler OnOverStack;
        public FileOperator(long maxlen)
        {
            this.maxlen = maxlen;
        }
        public void WriteDataInFile(StreamWriter filewriter)
        {
            int choise;
            bool choiseIsMade = false;
            string strToWrite;
            int strBytesCount;

            Console.WriteLine("Start writing");
            for (int i = 0; i < 1500; i++)
            {
                strToWrite = "Recorded string #" + (i + 1);
                strBytesCount = System.Text.ASCIIEncoding.Unicode.GetByteCount(strToWrite);
                if(i%100==0)
                    Console.WriteLine("FIle size " + filewriter.BaseStream.Length);
                if (choiseIsMade == false && (filewriter.BaseStream.Length + strBytesCount) > maxlen)
                {
                    choiseIsMade = true;
                    choise = OnOverStack(this, new OverStackEventArgs(filewriter.BaseStream.Length));
                    if (choise>0)
                        break;
                    else if (choise==0)
                    {
                        FileStream stream = (FileStream)filewriter.BaseStream;
                        stream.SetLength(0);
                        break;
                    }
                }
                filewriter.WriteLine(strToWrite);
            }
        }
        public void ReadDataInFile(StreamReader filereader)
        {
            string line = "";
            Console.WriteLine("Start reading");
            line = filereader.ReadLine();
            while (String.IsNullOrEmpty(line)==false)
            {
                Console.WriteLine(line);
                line = filereader.ReadLine();
            }
                
        }
    }
}
