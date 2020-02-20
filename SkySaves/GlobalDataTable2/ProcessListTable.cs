using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySaves.GlobalDataTable2
{
    class ProcessListTable
    {

        public float Unknown1 { get; private set; }
        public float Unknown2 { get; private set; }
        public float Unknown3 { get; private set; }
        public uint NextNum { get; private set; }
        public Crime.CrimeType[] AllCrimes { get; private set; }

        public ProcessListTable(byte[] data)
        {
            int index = 0;

            Unknown1 = BinHelp.GetFloat(data, ref index);
            Unknown2 = BinHelp.GetFloat(data, ref index);
            Unknown3 = BinHelp.GetFloat(data, ref index);

            NextNum = BinHelp.GetUInt32(data, ref index);


        }
    }
}
