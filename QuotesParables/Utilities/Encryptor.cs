using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// A class containing only shared members to be used for encrypting and decrypting
/// settings such as passwords or connection strings.
/// Uses the .NET DESCryptoServiceProvider to perform the encryption
/// using an 8 character key
/// </summary>
/// <remarks>
/// Converted to Visual Basic By CJMills from C# Code by hanusoftware
/// http://www.hanusoftware.com
/// </remarks>
/// 
namespace QuotesParables.Utilities
{

    public class Encryptor
    {

        // If you wish change the encrpyopn key,
        // first 8 character will be used

        private static string EncryptionKey = "&%#@?,:*";
        /// <summary>
        /// This method retrieve the string to encrypt from the Presentation Layer
        /// And return the Encrypted String
        /// </summary>
        /// <param name="strText">The text to encrypt</param>
        /// <returns>Encypted String</returns>
        public static string EncryptString(string strText)
        {
            if (strText == string.Empty)
            {
                return string.Empty;
            }
            else
            {
                return Encrypt(strText, EncryptionKey);
            }
        }

        /// <summary>
        /// This method retrieve the encrypted string to decrypt from the Presentation Layer
        /// And return the decrypted string
        /// </summary>
        /// <param name="strText">The text to decrypt</param>
        /// <returns>Decrypted string</returns>
        public static string DecryptString(string strText)
        {
            if (strText == string.Empty)
            {
                return string.Empty;
            }
            else
            {
                return Decrypt(strText, EncryptionKey);
            }
        }

        /// <summary>
        /// This method has been used to get the Encrypted string for the
        /// passed string
        /// </summary>
        /// <param name="strText">The text to be Encrypted</param>
        /// <param name="strEncrypt">The key to be used</param>
        /// <returns>The encrypted string</returns>
        private static string Encrypt(string strText, string strEncrypt)
        {

            byte[] byKey = new byte[21];
            byte[] dv = {
            0x27,
            0x34,
            0x58,
            0x78,
            0x90,
            0xab,
            0xcd,
            0xef
        };

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strText);

                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write);

                cs.Write(inputArray, 0, inputArray.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method has been used to Decrypt the Encrypted String
        /// </summary>
        /// <param name="strText">The text to be Decrypted</param>
        /// <param name="strEncrypt">The key to use</param>
        /// <returns>The Decrypted string</returns>
        private static string Decrypt(string strText, string strEncrypt)
        {

            byte[] bKey = new byte[21];
            byte[] IV = {
            0x27,
            0x34,
            0x58,
            0x78,
            0x90,
            0xab,
            0xcd,
            0xef
        };


            try
            {
                bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));

                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                byte[] inputByteArray = Convert.FromBase64String(strText);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, DES.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);

                cs.FlushFinalBlock();

                System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                return encoding.GetString(ms.ToArray());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}