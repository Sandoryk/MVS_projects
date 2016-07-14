using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MatrixHandler
{
    public class SerializeMatrix
    {
        BinaryFormatter formatter;
        public SerializeMatrix()
        {
            formatter = new BinaryFormatter();
        }
        public bool DoSerializing(string path,TwoDimentionsMatrixHolder mtrHolder)
        {
            bool res = false;

            using (FileStream fs = new FileStream(path,FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, mtrHolder);
                res = true;
            }

            return res;
        }

        public TwoDimentionsMatrixHolder DoDeSerializing(string path)
        {
            TwoDimentionsMatrixHolder resMtrx = null;

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                resMtrx = (TwoDimentionsMatrixHolder)formatter.Deserialize(fs);
            }

            return resMtrx;
        }
    }
}
