using System;

namespace SkySaves.GlobalDataTable1
{
    public class WeatherTable
    {
        public RefID Climate { get; private set; }
        public RefID Weather { get; private set; }
        public RefID PrevWeather { get; private set; }
        public RefID UnknownWeather1 { get; private set; }
        public RefID UnknownWeather2 { get; private set; }
        public RefID RegnWeather { get; private set; }

        public float CurrentTime { get; private set; }
        public float BeginningTime { get; private set; }
        public float WeatherPct { get; private set; }

        public uint Unknown1 { get; private set; }
        public uint Unknown2 { get; private set; }
        public uint Unknown3 { get; private set; }
        public uint Unknown4 { get; private set; }
        public uint Unknown5 { get; private set; }
        public uint Unknown6 { get; private set; }
        public float Unknown7 { get; private set; }
        public uint Unknown8 { get; private set; }

        public byte Flags { get; private set; }

        public byte[] UnknownData { get; private set; }
        public bool UnknownDataPresent { get; private set; }

        public WeatherTable(byte[] data)
        {
            int index = 0;

            Climate = BinHelp.GetRefID(data, ref index);
            Weather = BinHelp.GetRefID(data, ref index);
            PrevWeather = BinHelp.GetRefID(data, ref index);
            UnknownWeather1 = BinHelp.GetRefID(data, ref index);
            UnknownWeather2 = BinHelp.GetRefID(data, ref index);
            RegnWeather = BinHelp.GetRefID(data, ref index);

            CurrentTime = BinHelp.GetFloat(data, ref index);
            BeginningTime = BinHelp.GetFloat(data, ref index);
            WeatherPct = BinHelp.GetFloat(data, ref index);

            Unknown1 = BinHelp.GetUInt32(data, ref index);
            Unknown2 = BinHelp.GetUInt32(data, ref index);
            Unknown3 = BinHelp.GetUInt32(data, ref index);
            Unknown4 = BinHelp.GetUInt32(data, ref index);
            Unknown5 = BinHelp.GetUInt32(data, ref index);
            Unknown6 = BinHelp.GetUInt32(data, ref index);
            Unknown7 = BinHelp.GetFloat(data, ref index);
            Unknown8 = BinHelp.GetUInt32(data, ref index);

            Flags = data[index++];

            //store the rest of the data array into UnknownData
            if (index < data.Length)
            {
                UnknownData = new byte[data.Length - index];
                for (int x = index; x < index; x++)
                {
                    UnknownData[x - index] = data[x];
                }
                UnknownDataPresent = true;
            }

            UnknownDataPresent = false;
        }
    }
}
