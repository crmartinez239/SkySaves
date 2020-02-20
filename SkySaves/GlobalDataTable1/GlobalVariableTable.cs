using System;

namespace SkySaves
{
    public class GlobalVariableTable
    {
        public struct GlobalVariable
        {
            public readonly RefID FormID;
            public readonly float Value;

            public GlobalVariable(RefID formID, float value)
            {
                FormID = formID;
                Value = value;
            }
        }

        public VSVal Count { get; private set; }
        public GlobalVariable[] Globals { get; private set; }

        public GlobalVariableTable(byte[] data)
        {
            int index = 0;

            Count = BinHelp.GetVSVal(data, ref index);
            int countValue = Count.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            Globals = new GlobalVariable[countValue];

            for (int i = 0; i < countValue; i++)
            {
                RefID formID = BinHelp.GetRefID(data, ref index);
                float value = BinHelp.GetFloat(data, ref index);

                Globals[i] = new GlobalVariable(formID, value);
            }
        }
    }
}
