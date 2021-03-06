﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace DevExpress.ProductsDemo.Win.Common
{
    public static class DK1Util
    {

        public static byte[] ToByteArray(object source)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                return stream.ToArray();
            }
        }

        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }


        public static byte GetCheckSum(byte[] message, int startadd, int endadd)
        {
            try
            {
                //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
                //return the CRC values:

                int sumFull = 0;

                for (int i = startadd; i <= endadd; i++)
                {

                    sumFull += message[i];
                }

                return (byte)(sumFull & 0xFF);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static byte GetCheckXOR(byte[] message, int startadd, int endadd)
        {
            try
            {
                //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
                //return the CRC values:

                int sumxor = message[startadd];

                for (int i = startadd + 1; i <= endadd; i++)
                {

                    sumxor ^= message[i];
                }

                return (byte)(sumxor & 0xFF);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
