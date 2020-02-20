using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace SkySaves
{
    public class Header
    {
        public struct ScreenShotData
        {
            public readonly int Width;
            public readonly int Height;
            public readonly byte[] RawData;
            public readonly Bitmap ScreenShotImage;

            public ScreenShotData(int width, int height, byte[] rawData, Bitmap screenShot)
            {
                Width = width;
                Height = height;
                RawData = rawData;
                ScreenShotImage = screenShot;
            }
        }

        public ScreenShotData ScreenShot { get; private set; }

        public ushort PlayerSex { get; private set; }
        /// <summary>
        /// Current format version. supported 7-9
        /// </summary>
        public uint Version { get; private set; }

        public float PlayerCurrentExp { get; private set; }
        public float PlayerLevelUpExp { get; private set; }

        /// <summary>
        /// Default name of save file. Presumably this is a counter keeping track of the total number of saves you have made to date.
        /// </summary>
        public uint SaveNumber { get; private set; }
        public uint PlayerLevel { get; private set; }
        public string PlayerName { get; private set; }

        public string PlayerLocation { get; private set; }
        public string GameDate { get; private set; }
        public string PlayerRace { get; private set; }

        public string PlayerSexF
        {
            get
            {
                if (PlayerSex == 0)
                    return "Male";
                else
                    return "Female";
            }
        }

        public Header(byte[] headerData, ref FileStream fs)
        {
            //this method will convert all player related data in the header
            int headerIndex = 0;
            convertHeaderData(headerData, ref headerIndex);

            //the next 8 bytes are for filetime info (struct filetime)
            //for now we will ignore these bytes
            headerIndex += 8;

            //now for the screen shot size
            //first will be the width (uint32)
            uint shotWidth = BinHelp.GetUInt32(headerData, ref headerIndex);

            //then the screen shot height (uint32)
            uint shotHeight = BinHelp.GetUInt32(headerData, ref headerIndex);

            //so ends all header data
            //now extract RGB pixel data for the save screen shot
            //the length of data will be 3 * shotWidth * shotHeight
            uint screenShotLength = 3 * shotWidth * shotHeight;
            byte[] screenShotData = new byte[screenShotLength];


            fs.Read(screenShotData, 0, (int)screenShotLength);

            Bitmap ss = bmpFromByte(screenShotData, (int)shotWidth, (int)shotHeight);
            ScreenShot = new ScreenShotData((int)shotWidth, (int)shotHeight, screenShotData, ss);
        }

        private void convertHeaderData(byte[] headerData, ref int index)
        {
            //first value is file format version (uint32)
            //supported versions are 7 - 9
            Version = BinHelp.GetUInt32(headerData, ref index);

            //next is save number (uint32). user for default name of save
            SaveNumber = BinHelp.GetUInt32(headerData, ref index);

            //next get player name
            PlayerName = BinHelp.GetWString(headerData, ref index);

            //next is current player level (uint32)
            PlayerLevel = BinHelp.GetUInt32(headerData, ref index);

            //next get player location
            PlayerLocation = BinHelp.GetWString(headerData, ref index);

            //next get in game date at time of save
            GameDate = BinHelp.GetWString(headerData, ref index);

            //next get player race
            PlayerRace = BinHelp.GetWString(headerData, ref index);

            //next 2 bytes is the player gender (unit16)
            //0 = male, 1 = female
            PlayerSex = BinHelp.GetUInt16(headerData, ref index);

            //next 4 bytes are for the current exp player has (float32)
            PlayerCurrentExp = BinHelp.GetFloat(headerData, ref index);

            //last 4 bytes are is the exp needed for player to level up (float32)
            PlayerLevelUpExp = BinHelp.GetFloat(headerData, ref index);

            //return current index in header 
            //so filetime and screen shot info can be extracted
        }

        //source: https://stackoverflow.com/questions/47918451/creating-a-bitmap-from-rgb-array-in-c-result-image-difference
        private Bitmap bmpFromByte(byte[] buffer, int width, int height)
        {
            Bitmap b = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            Rectangle BoundsRect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = b.LockBits(BoundsRect,
                                            ImageLockMode.WriteOnly,
                                            b.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            // add back dummy bytes between lines, make each line be a multiple of 4 bytes
            int skipByte = bmpData.Stride - width * 3;
            byte[] newBuff = new byte[buffer.Length + skipByte * height];
            for (int j = 0; j < height; j++)
            {
                Buffer.BlockCopy(buffer, j * width * 3, newBuff, j * (width * 3 + skipByte), width * 3);
            }

            // fill in rgbValues
            Marshal.Copy(newBuff, 0, ptr, newBuff.Length);
            b.UnlockBits(bmpData);
            return b;
        }

    }
}
