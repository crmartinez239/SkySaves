using System;

namespace SkySaves.GlobalDataTable1
{
    public class EffectsTable
    {
        public struct Effect
        {
            public readonly float Strength;
            public readonly float Timestamp;
            public readonly uint Unknown;
            public readonly RefID EffectID;

            public Effect (float strength, float timestamp, 
                uint unknown, RefID effectID)
            {
                Strength = strength;
                Timestamp = timestamp;
                Unknown = unknown;
                EffectID = effectID;
            }
        }

        public VSVal Count { get; private set; }
        public Effect[] ImageSpaceModifiers { get; private set; }
        public float Unknown1 { get; private set; }
        public float Unknown2 { get; private set; }

        public EffectsTable(byte[] data)
        {
            int index = 0;

            Count = BinHelp.GetVSVal(data, ref index);
            int effectCount = Count.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            ImageSpaceModifiers = new Effect[effectCount];

            for (int i = 0; i < effectCount; i++)
            {
                float strength = BinHelp.GetFloat(data, ref index);
                float timestamp = BinHelp.GetFloat(data, ref index);
                uint unknown = BinHelp.GetUInt32(data, ref index);
                RefID effectID = BinHelp.GetRefID(data, ref index);

                ImageSpaceModifiers[i] = new Effect(strength, timestamp,
                    unknown, effectID);
            }

            Unknown1 = BinHelp.GetFloat(data, ref index);
            Unknown2 = BinHelp.GetFloat(data, ref index);
        }
    }
}
