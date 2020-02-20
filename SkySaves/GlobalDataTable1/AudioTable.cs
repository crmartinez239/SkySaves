using System;

namespace SkySaves.GlobalDataTable1
{
    public class AudioTable
    {
        public RefID Unknown { get; private set; }
        public VSVal TracksCount { get; private set; }
        public RefID[] Tracks { get; private set; }
        public RefID BgMusic { get; private set; }

        public AudioTable(byte[] data)
        {
            int index = 0;

            Unknown = BinHelp.GetRefID(data, ref index);
            TracksCount = BinHelp.GetVSVal(data, ref index);

            int tracksCount = TracksCount.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop
            Tracks = new RefID[tracksCount];

            for (int i = 0; i < tracksCount; i++)
                Tracks[i] = BinHelp.GetRefID(data, ref index);

            BgMusic = BinHelp.GetRefID(data, ref index);            
        }
    }
}
