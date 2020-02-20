using System;

namespace SkySaves
{
   public class PlugInInfo
    {
        public byte PlugInCount { get; private set; }
        public string[] PlugIns { get; private set; }

        public PlugInInfo(byte[]plugInData)
        {
            int index = 0;

            PlugInCount = plugInData[index++];
            PlugIns = new string[PlugInCount];

            for(byte i = 0; i < PlugInCount; i++)
            {
                PlugIns[i] = BinHelp.GetWString(plugInData, ref index);
            }
        }

    }
}
