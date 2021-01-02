using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SkySaves.GlobalDataTable2
{
    public class ProcessListTable
    {

        public float Unknown1 { get; private set; }
        public float Unknown2 { get; private set; }
        public float Unknown3 { get; private set; }
        public uint NextNum { get; private set; }
        public Crime[] AllCrimes { get; private set; }

        public ProcessListTable(ref FileStream fileStream)
        {
            const int numberOfCrimeTypes = 7;

            Unknown1 = BinHelp.ReadFloat(ref fileStream);
            Unknown2 = BinHelp.ReadFloat(ref fileStream);
            Unknown3 = BinHelp.ReadFloat(ref fileStream);

            NextNum = BinHelp.ReadUInt32(ref fileStream);

            var crimes = new List<Crime>();

            for (int i = 0; i < numberOfCrimeTypes; i++)
            {
                VSVal crimeCount = BinHelp.ReadVSVal(ref fileStream);
                for (int crimeIndex = 0; crimeIndex < crimeCount.ValueByInt; crimeIndex++)
                {
                    crimes.Add(new Crime(ref fileStream));   
                }
            }

            AllCrimes = crimes.ToArray();
        }
    }
}
