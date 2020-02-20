using System;
using System.IO;
using System.Text;
using SkySaves.GlobalDataTable1;

namespace SkySaves
{
    public enum GlobalDataType : uint
    {
        // globalDataTable1
        MiscStats = 0,
        PlayerLocation,
        TES,
        GlobalVariables,
        CreatedObject,
        Effects,
        Weather,
        Audio,
        SkyCells,

        // globalDataTable2
        ProcessLists = 100,
        Combat,
        Interface,
        ActorCauses,
        Unknown104,
        DectectionManager,
        LocationMetaData,
        QuestStaticData,
        StoryTeller,
        MagicFavorties,
        PlayerControls,
        StoryEventManager,
        IngredientShared,
        MenuControls,
        MenuTopicManager,

        // globalDataTable3
        TempEffects = 1000,
        Papyrus,
        AnimObject,
        Timer,
        SynchronizedAnimations,
        Main
    }

    public class SaveFile
    {
        private const string magicValue = @"TESV_SAVEGAME";

        public byte FormVersion { get; private set; }

        public Header HeaderInfo { get; private set; }
        public PlugInInfo PlugIns { get; private set; }
        public FileLocationTable FileLocation { get; private set; }

        // globalDataTable1 0 - 8
        public MiscStatsTable MiscStats { get; private set; } // 0
        public PlayerLocationTable PlayerLocation { get; private set; } // 1
        public TESTable TES { get; private set; } // 2
        public GlobalVariableTable GlobalVariables { get; private set; } // 3
        public CreatedObjectTable CreatedObjects { get; private set; } // 4
        public EffectsTable Effects { get; private set; } // 5
        public WeatherTable Weather { get; private set; } // 6
        public AudioTable Audio { get; private set; } // 7
        public SkyCellsTable SkyCells { get; private set; } // 8

        public void Import(string fileName, bool headerOnly = false)
        {
            try
            {
                var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                // Retrieve byte value of magic string (char[13])
                byte[] magic = new byte[magicValue.Length];
                stream.Read(magic, 0, magicValue.Length);

                // Check value of magic string
                if (Encoding.UTF8.GetString(magic) != magicValue)
                    throw new Exception("Invalid file type.");

                // Retrieve the header of save file (SaveHeader)
                uint headerSize = BinHelp.ReadUInt32(ref stream);
                byte[] headerData = new byte[headerSize];
                stream.Read(headerData, 0, (int)headerSize);

                // The Header class needs access to our stream to extract screenshot data
                HeaderInfo = new Header(headerData, ref stream);
                if (headerOnly == true)
                {
                    stream.Close();
                    return;
                }

                // form version is an uint8 or byte. not sure what its used for
                FormVersion = BinHelp.ReadByte(ref stream);

                // Size of the plugin information
                uint pluginInfoSize = BinHelp.ReadUInt32(ref stream);
                byte[] pluginData = new byte[pluginInfoSize];
                stream.Read(pluginData, 0, (int)pluginInfoSize);

                PlugIns = new PlugInInfo(pluginData);
                FileLocation = new FileLocationTable(ref stream);

                parseGlobalData1(ref stream);
                parseGlobalData2(ref stream);


                stream.Close();
            }
            catch (Exception e)
            {
                //string m = e.Message;
            }

        }

        private void parseGlobalData2(ref FileStream stream)
        {
            // globalDataTable2 contains 15 different types
            for (int i = 0; i < FileLocation.GlobalDataTabel2Count; i++)
            {
                uint tableType = BinHelp.ReadUInt32(ref stream);
                uint tableLength = BinHelp.ReadUInt32(ref stream);

                byte[] data = new byte[tableLength];
                stream.Read(data, 0, (int)tableLength);

                switch ((GlobalDataType)tableType)
                {
                    case GlobalDataType.ProcessLists: //100
                        break;
                }
            }
        }

        private void parseGlobalData1(ref FileStream stream)
        {
            // globalDataTable1 contains 9 different types
            for (int i = 0; i < FileLocation.GlobalDataTabel1Count; i++)
            {
                uint tableType = BinHelp.ReadUInt32(ref stream);
                uint tableLength = BinHelp.ReadUInt32(ref stream);

                byte[] data = new byte[tableLength];
                stream.Read(data, 0, (int)tableLength);

                switch ((GlobalDataType)tableType)
                {
                    case GlobalDataType.MiscStats: // 0
                        MiscStats = new MiscStatsTable(data);
                        break;

                    case GlobalDataType.PlayerLocation: // 1
                        PlayerLocation = new PlayerLocationTable(data);
                        break;

                    case GlobalDataType.TES: // 2
                        TES = new TESTable(data);
                        break;

                    case GlobalDataType.GlobalVariables: // 3
                        GlobalVariables = new GlobalVariableTable(data);
                        break;

                    case GlobalDataType.CreatedObject: // 4
                        CreatedObjects = new CreatedObjectTable(data);
                        break;

                    case GlobalDataType.Effects: // 5
                        Effects = new EffectsTable(data);
                        break;

                    case GlobalDataType.Weather: // 6
                        Weather = new WeatherTable(data);
                        break;

                    case GlobalDataType.Audio: // 7
                        Audio = new AudioTable(data);
                        break;

                    case GlobalDataType.SkyCells: // 8
                        SkyCells = new SkyCellsTable(data);
                        break;

                    default:
                        throw new Exception(String.Format("Unexpected globalDataTable1 type: {0}", tableType));
                }
            }
        }



        //private int WriteBitmapFile(string filename, int width, int height, byte[] imageData)
        //{
        //    using (var stream = new MemoryStream(imageData))
        //    using (var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
        //    {
        //        BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0,
        //                                                        bmp.Width,
        //                                                        bmp.Height),
        //                                          ImageLockMode.WriteOnly,
        //                                          bmp.PixelFormat);
        //        IntPtr pNative = bmpData.Scan0;

        //        Marshal.Copy(imageData, 0, pNative, imageData.Length);

        //        bmp.UnlockBits(bmpData);

        //        bmp.Save(filename);
        //    }

        //    return 1;
        //}


    }
}
