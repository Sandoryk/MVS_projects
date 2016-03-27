using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringOperations
{
    class Program
    {
        static string SplitText(string instr,int maxlen)
        {
            string outstr="",ch="";
            int i,j,curlen,sep;

            sep = -1;
	        i = 0;
	        j = 0;
            curlen = 0;

            while(i<instr.Length)
	        {
                ch = instr.Substring(i,1);
		        if(ch=="\n")
		        {
                    outstr += instr.Substring(j,i-j) + "\n";

			        i++;
			        sep = -1;
			        j = i;
                    curlen = 0;
			        goto continuelabel;
		        }
		        if(ch==" ")
		        {
			        sep = i;
		        }

                curlen += 1;

                if (curlen > maxlen)
		        {
			        if(sep==-1)
			        {
				        if(i==j)
					        i++;
                        outstr += instr.Substring(j, i - j) + "\n";
			        }
			        else
			        {
                        outstr += instr.Substring(j, sep - j) + "\n";
				        i = sep+1;
			        }
			        sep = -1;
			        j = i;
                    curlen = 0;
		        }
		        else
			        i++;
            continuelabel:; 
	        }
		    outstr += instr.Substring(j,i-j);    

            return outstr;
        }
        static void Main(string[] args)
        {
            string teststr = @"C:\Users\Admin\helloworld.txt; " + "Text:Sherlock \nHolmes is a fictional private detective created by British author Sir Arthur Conan Doyle. Known as a \"consulting detective\" in the stories, Holmes is known for a proficiency with observation, forensic science, and logical reasoning that borders on the fantastic, which he employs when investigating cases for a wide variety of clients, including Scotland Yard.";
            teststr = teststr.Replace("n","");
            Console.WriteLine("Removed \"n\": \n" + teststr);
            Console.WriteLine("\n---------\n");
            teststr = teststr.Replace(",", ",-");
            Console.WriteLine("Added \"-\" after \",\": \n" + teststr);
            Console.WriteLine("\n---------\n");
            teststr = SplitText(teststr,50);
            Console.WriteLine("Text splited by blank char: \n" + teststr);
            Console.WriteLine("\n---------\n");
            teststr += " !!!One more string!!!!";
            Console.WriteLine("One more string added: \n" + teststr);
            Console.WriteLine("\n---------\n");
            teststr.Contains("hello");
            Console.WriteLine("String contains \"hello\": {0}\n", teststr.Contains("hello").ToString());
        }
    }
}
