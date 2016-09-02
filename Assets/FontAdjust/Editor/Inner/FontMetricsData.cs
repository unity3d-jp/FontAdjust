using UnityEngine;
using System.Text;
using System.Collections;

namespace FontAdjust
{
    public class FontMetricsData
    {
        public int ascent { get; set; }
        public int descent { get; set; }
        public int emHeight { get; set; }
        public int leading { get; set; }
        public int lineSpace { get { return this.ascent + this.leading + this.descent; } }

        public float GetCalculatedAscent(float fontSize)
        {
            return ((fontSize * this.ascent) / (float)this.emHeight);
        }
        public float GetCalculatedDescent(float fontSize)
        {
            return ((fontSize * this.descent) / (float)this.emHeight);
        }
        public float GetCalculatedLeading(float fontSize)
        {
            return ((fontSize * this.leading) / (float)this.emHeight);
        }
        public float GetCalculatedLineSpace(float fontSize)
        {
            return ((fontSize * this.lineSpace) / (float)this.emHeight);
        }



        public static FontMetricsData CreateFontMetricsData(string path)
        {
            if (path == "Library/unity default resources") { return null; }
            byte[] bin = System.IO.File.ReadAllBytes(path);
            return CreateFontMetricsData(bin);
        }
        public static FontMetricsData CreateFontMetricsData(byte[] binData)
        {
            FontMetricsData data = new FontMetricsData();
            int tableNum = GetUin16(binData, 4);
            for (int i = 0; i < tableNum; ++i)
            {
                string header = GetString(binData, 12 + i * 16, 4);
                uint offset = GetUin32(binData, 12 + i * 16 + 8);
                uint size = GetUin32(binData, 12 + i * 16 + 12);

                if (header == "head")
                {
                    // emHeight offset + 18
                    System.Console.WriteLine(header + ";;" + offset);
                    data.emHeight = GetUin16(binData, (int)offset + 18);
                }
                else if (header == "hhea")
                {
                    System.Console.WriteLine(header + ";;" + offset);
                    data.ascent = GetSint16(binData, (int)offset + 4);
                    data.descent = -GetSint16(binData, (int)offset + 6);
                    data.leading = GetSint16(binData, (int)offset + 8);
                }
            }
            return data;
        }

        private static int GetSint16(byte[] binData, int idx)
        {
            bool isNegative = (binData[idx] > 128);
            int val = (binData[idx] << 8) + binData[idx + 1];
            if (!isNegative) { return val; }
            return (val - 0x10000);
        }
        private static int GetUin16(byte[] binData, int idx)
        {
            return (binData[idx] << 8) + binData[idx + 1];
        }
        private static uint GetUin32(byte[] binData, int idx)
        {
            return ((uint)binData[idx] << 24) +
                ((uint)binData[idx + 1] << 16) +
                ((uint)binData[idx + 2] << 8) +
                ((uint)binData[idx + 3] << 0);
        }
        private static string GetString(byte[] binData, int idx, int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = idx; i < idx + length; ++i)
            {
                sb.Append((char)binData[i]);
            }
            return sb.ToString();
        }


    }

}
