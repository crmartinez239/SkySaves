using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SkySaves.GlobalDataTable2
{
    public class Crime
    {
        public uint WitnessNumber { get; private set; }
        public uint CrimeType { get; private set; }
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


        public Crime (ref FileStream fileStream)
        {
            WitnessNumber = BinHelp.ReadUInt32(ref fileStream);
            CrimeType = BinHelp.ReadUInt32(ref fileStream);
                
            Unknown1 = (byte)fileStream.ReadByte();

            Quantity = BinHelp.ReadUInt32(ref fileStream);
            SerialNumber = BinHelp.ReadUInt32(ref fileStream);

            Unknown2 = (byte)fileStream.ReadByte();
            Unknown3 = BinHelp.ReadUInt32(ref fileStream);

            ElapsedTime = BinHelp.ReadFloat(ref fileStream);

            VictimID = BinHelp.ReadRefID(ref fileStream);
            CriminalID = BinHelp.ReadRefID(ref fileStream);
            ItemBaseID = BinHelp.ReadRefID(ref fileStream);
            OwnershipID = BinHelp.ReadRefID(ref fileStream);

            WitnessCount = BinHelp.ReadVSVal(ref fileStream);
            int witnessCount = WitnessCount.ValueByInt;

            Witnesses = new RefID[witnessCount];

            for (int i = 0; i < witnessCount; i++)
                Witnesses[i] = BinHelp.ReadRefID(ref fileStream);

            Bounty = BinHelp.ReadUInt32(ref fileStream);
            CrimeFactionID = BinHelp.ReadRefID(ref fileStream);

            IsCleared = (byte)fileStream.ReadByte();

            Unknown4 = BinHelp.ReadUInt16(ref fileStream);
        }

        public string CrimeTypeF
        {
            get
            {
                switch (CrimeType)
                {
                    case 0: return "Theft";
                    case 1: return "Pickpockting";
                    case 2: return "Tresspassing";
                    case 3: return "Assualt";
                    case 4: return "Murder";
                    // 5 for some reason has been left blank in the file format
                    case 6: return "Lycanthropy";
                    default: return "Unknown";
                }
            }
        }
    }
}
