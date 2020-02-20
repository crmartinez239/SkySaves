using System;

namespace SkySaves
{
    public class CreatedObjectTable
    {
        public struct Enchantment
        {
            public readonly RefID FormID;
            public readonly uint TimesUsed;
            public readonly VSVal Count;
            public readonly MagicEffect[] Effects;
            
            public Enchantment
                (RefID formID, uint timesUsed, VSVal count, MagicEffect[] effects)
            {
                FormID = formID;
                TimesUsed = timesUsed;
                Count = count;
                Effects = effects;
            }
        }
        public struct MagicEffect
        {
            public readonly RefID EffctID;
            public readonly EnchInfo Info;
            public readonly float Price;

            public MagicEffect(RefID effectID, EnchInfo info, float price)
            {
                EffctID = effectID;
                Info = info;
                Price = price;
            }
        }
        public struct EnchInfo
        {
            public readonly float Magnitude;
            public readonly uint Duration;
            public readonly uint Area;

            public EnchInfo(float magnitude, uint duration, uint area)
            {
                Magnitude = magnitude;
                Duration = duration;
                Area = area;
            }
        }

        public VSVal WeaponCount { get; private set; }
        public Enchantment[] WeaponEnchTable { get; private set; }
        public VSVal ArmourCount { get; private set; }
        public Enchantment[] ArmourEnchTable { get; private set; }
        public VSVal PotionCount { get; private set; }
        public Enchantment[] PotionTable { get; private set; }
        public VSVal PoisonCount { get; private set; }
        public Enchantment[] PoisonTable { get; private set; }

        public CreatedObjectTable(byte[] data)
        {
            int index = 0;

            //Weapon enchantments//////////////////////////
            WeaponCount = BinHelp.GetVSVal(data, ref index);
            int weaponCount = WeaponCount.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            WeaponEnchTable = new Enchantment[weaponCount];

            for (int i = 0; i < weaponCount; i++)
                WeaponEnchTable[i] = getEnchantment(data, ref index);
            ///////////////////////////////////////////////

            //Armour enchantments//////////////////////////
            ArmourCount = BinHelp.GetVSVal(data, ref index);
            int armourCount = ArmourCount.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            ArmourEnchTable = new Enchantment[armourCount];

            for (int i = 0; i < armourCount; i++)
                ArmourEnchTable[i] = getEnchantment(data, ref index);
            ///////////////////////////////////////////////

            //Potions//////////////////////////////////////
            PotionCount = BinHelp.GetVSVal(data, ref index);
            int potionCount = PotionCount.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            PotionTable = new Enchantment[potionCount];

            for (int i = 0; i < potionCount; i++)
                PotionTable[i] = getEnchantment(data, ref index);
            ///////////////////////////////////////////////

            //Poisons//////////////////////////////////////
            PoisonCount = BinHelp.GetVSVal(data, ref index);
            int poisonCount = PoisonCount.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            PoisonTable = new Enchantment[poisonCount];

            for (int i = 0; i < poisonCount; i++)
                PoisonTable[i] = getEnchantment(data, ref index);
            ///////////////////////////////////////////////
        }

        private Enchantment getEnchantment(byte[]data, ref int index)
        {
            RefID refId = BinHelp.GetRefID(data, ref index);
            uint timesUsed = BinHelp.GetUInt32(data, ref index);

            VSVal count = BinHelp.GetVSVal(data, ref index);
            int effectCount = count.ValueByInt; //store ValueByInt in variable to avoid unnesscary casting in for loop

            MagicEffect[] effects = new MagicEffect[effectCount];

            for (int i = 0; i < effectCount; i++)
                effects[i] = getMagicEffect(data, ref index);

            return new Enchantment (refId, timesUsed, count, effects);
        }

        private MagicEffect getMagicEffect(byte[] data, ref int index)
        {
            RefID effectID = BinHelp.GetRefID(data, ref index);

            //EnchInfo struct///////
            float magnitude = BinHelp.GetFloat(data, ref index);
            uint duration = BinHelp.GetUInt32(data, ref index);
            uint area = BinHelp.GetUInt32(data, ref index);
            EnchInfo enchInfo = new EnchInfo(magnitude, duration, area);
            ///////////////////////////

            float price = BinHelp.GetFloat(data, ref index);

            return new MagicEffect(effectID, enchInfo, price);
        }
    }
}
