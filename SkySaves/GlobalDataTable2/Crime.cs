using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkySaves.GlobalDataTable2
{
    class Crime
    {
        public enum CrimeType : uint
        {
            Theft,
            Pickpocketing,
            Tresspassing,
            Assualt,
            Murder,
            Blank,
            Lycanthropy
        }

        public uint WitnessNumber { get; private set; }
        public CrimeType TypeOfCrime { get; private set; }
        public byte Unknown1 { get; private set; }
        public uint Quantity { get; private set; }
        public uint SerialNumber { get; private set; }
        public byte Unknown2 { get; private set; }
        public uint Unknown3 { get; private set; }
        public float ElapsedTime { get; private set; }
        public RefID VictimID { get; private set; }
        public RefID CriminalID { get; private set; }
        public RefID ItemBaseID { get; private set; }
        public RefID OwnershipID { get; private set; }
        public VSVal WitnessCount { get; private set; }
        public RefID[] Witnesses { get; private set; }
        public uint Bounty { get; private set; }
        public RefID CrimeFactionID { get; private set; }
        public byte IsCleared { get; private set; }
        public ushort Unknown4 { get; private set; }

        public Crime (byte[] data)
        {
            int index = 0;

            WitnessNumber = BinHelp.GetUInt32(data, ref index);
            TypeOfCrime = (CrimeType)BinHelp.GetUInt32(data, ref index);

            Unknown1 = data[index++];

            Quantity = BinHelp.GetUInt32(data, ref index);
            SerialNumber = BinHelp.GetUInt32(data, ref index);

            Unknown2 = data[index++];
            Unknown3 = BinHelp.GetUInt32(data, ref index);

            ElapsedTime = BinHelp.GetFloat(data, ref index);

            VictimID = BinHelp.GetRefID(data, ref index);
            CriminalID = BinHelp.GetRefID(data, ref index);
            ItemBaseID = BinHelp.GetRefID(data, ref index);
            OwnershipID = BinHelp.GetRefID(data, ref index);

            WitnessCount = BinHelp.GetVSVal(data, ref index);
            int witnessCount = WitnessCount.ValueByInt;

            Witnesses = new RefID[witnessCount];

            for (int i = 0; i < witnessCount; i++)
                Witnesses[i] = BinHelp.GetRefID(data, ref index);

            Bounty = BinHelp.GetUInt32(data, ref index);
            CrimeFactionID = BinHelp.GetRefID(data, ref index);

            IsCleared = data[index++];

            Unknown4 = BinHelp.GetUInt16(data, ref index);
        }
    }
}
