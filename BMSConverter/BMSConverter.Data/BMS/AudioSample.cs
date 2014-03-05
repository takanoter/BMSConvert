using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSConverter.Data.BMS
{
    public class AudioSample
    {
        byte id;
        string file;

        public AudioSample(string pFile, byte pId)
        {
            id = pId;
            file = pFile;
        }

        public byte Id
        {
            get
            {
                return id;
            }
        }

        public string File
        {
            get
            {
                return file;
            }
        }
    }
}
