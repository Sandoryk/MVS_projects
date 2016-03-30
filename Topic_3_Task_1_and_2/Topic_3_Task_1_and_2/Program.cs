using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topic_3_Task_1_and_2
{
    class Program
    {

        static void Main(string[] args)
        {
            FileStream fs;
            DirectoryInfo dir1,dir2,dir3;
            FileInfo file, fileMoved;

            //Создать 2 директории в некоторой корневой папке.Переместить одну директорию в другую.
            //Переместить в эту директорию существующий файл.
            dir1 = new DirectoryInfo("Subfolder1_Task1");
            if (dir1.Exists == false)
                dir1.Create();
            dir2 = new DirectoryInfo("Subfolder2_Task1");
            if (dir2.Exists == false)
                dir2.Create();
            //Directory.Move(dir2.FullName, dir1.FullName);
            dir3 = new DirectoryInfo(dir1.Name + @"\" + dir2.Name);
            if (dir3.Exists == false)
                dir3.Create();
            file = new FileInfo(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName + @"\Testfile.txt");
            if (file.Exists == false)
            {
                using (fs = new FileStream("Testfile.txt", FileMode.Create)){}
            }
            fileMoved = new FileInfo(dir3.FullName + @"\Testfile.txt");
            if (fileMoved.Exists == false)
                file.CopyTo(dir3.FullName + @"\Testfile.txt");




            //Создать дерево директорий от одной корневой. У каждой директории должно быть 3 вложенных, глубина вложения – 3 уровня.
            //В каждую директорию скопировать некоторый файл.
            dir1 = new DirectoryInfo("Rootfolder_Task2");
            if (dir1.Exists == false)
                dir1.Create();
            dir1.Create();
            file = new FileInfo(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName + @"\Testfile.txt");
            if (file.Exists == false)
            {
                using (fs = new FileStream("Testfile.txt", FileMode.Create)) { }
            }
            fileMoved = new FileInfo(dir1.FullName + @"\Testfile.txt");
            if (fileMoved.Exists == false)
                file.CopyTo(dir1.FullName + @"\Testfile.txt");
            MakeDirsWithFile(dir1.Name, "Subfolder", 3, file);

        }
        static void MakeDirsWithFile(string pathstr,string folderName, int maxlevel, FileInfo file,int strpos = 1)
        {
            DirectoryInfo curdir;
            FileInfo fileMoved;
            string pathpart;

            for (int i = 0; i < maxlevel; i++)
            {
                pathpart = @"\" + folderName + i;
                if (strpos == maxlevel)
                {
                    curdir = new DirectoryInfo(pathstr + pathpart);
                    if (curdir.Exists == false)
                        curdir.Create();
                    fileMoved = new FileInfo(pathstr + pathpart + @"\" + file.Name);
                    if (fileMoved.Exists == false)
                        file.CopyTo(pathstr + pathpart + @"\" + file.Name); 
                }
                else
                {
                    curdir = new DirectoryInfo(pathstr + pathpart);
                    if (curdir.Exists == false)
                        curdir.Create();
                    MakeDirsWithFile(pathstr + @"\" + curdir.Name, folderName, maxlevel, file, strpos + 1);
                }
            }
            fileMoved = new FileInfo(pathstr + @"\" + file.Name);
            if (fileMoved.Exists == false)
                file.CopyTo(pathstr + @"\" + file.Name);
        }
    }
}
