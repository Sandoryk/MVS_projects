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

        public delegate int OverStackHandler(object sender,EventArgs e);

        public OverStackHandler OnOverStack;
        public FileOperator(long maxlen)
        {
            this.maxlen = maxlen;
        }
        public void WriteDataInFile(StreamWriter filewriter)
        {
            int choise;
            bool choiseIsMade = false;

            Console.WriteLine("Start writing");
            for (int i = 0; i < 1500; i++)
            {
                filewriter.WriteLine("Recorded string #" + (i+1));
                if(i%100==0)
                    Console.WriteLine("Length " + filewriter.BaseStream.Length);
                if (choiseIsMade == false && filewriter.BaseStream.Length > maxlen)
                {
                    choiseIsMade = true;
                    choise = OnOverStack(this, new EventArgs());
                    if (choise>0)
                        break;
                    else if (choise==0)
                    {
                        FileStream g = (FileStream)filewriter.BaseStream;
                        g.SetLength(0);
                        break;
                    }
                }  
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
