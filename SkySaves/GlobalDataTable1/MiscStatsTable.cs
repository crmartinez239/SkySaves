using System;

namespace SkySaves
{
    public class MiscStatsTable
    {
        public struct Stat
        {
            public const byte General = 0;
            public const byte Quest = 1;
            public const byte Combat = 2;
            public const byte Magic = 3;
            public const byte Crafting = 4;
            public const byte Crime = 5;
            public const byte DLCStats = 6;

            public readonly string Name;
            public readonly string CategoryF;
            public readonly byte Category;
            public readonly int Value;

            public Stat(string name, byte category, int value)
            {
                Name = name;
                Category = category;
                Value = value;

                switch(category)
                {
                    case General:
                        CategoryF = "General";
                        break;

                    case Quest:
                        CategoryF = "Quest";
                        break;

                    case Combat:
                        CategoryF = "Combat";
                        break;

                    case Magic:
                        CategoryF = "Magic";
                        break;

                    case Crafting:
                        CategoryF = "Crafting";
                        break;

                    case Crime:
                        CategoryF = "Crime";
                        break;

                    default:
                        CategoryF = "DLCStats";
                        break;
                }
            }

        }
  
        public uint Count { get; private set; }
        public Stat[] Stats { get; private set; } 

        public MiscStatsTable(byte[] data)
        {
            int index = 0;

            Count = BinHelp.GetUInt32(data, ref index);
            Stats = new Stat[Count];

            for (int i = 0; i < Count; i++)
            {
                string name = BinHelp.GetWString(data, ref index);
                byte category = data[index++];
                int value = BinHelp.GetInt32(data, ref index);
                Stats[i] = new Stat(name, category, value);
            }
        }


    }
}
