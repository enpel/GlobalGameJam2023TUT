using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Security.Cryptography;


namespace Gekkou
{
    public static class SaveSystem<T> where T : struct
    {
        private static readonly string ENCRYPTKEY = "c6eahbq9sjuawhvdr9kvhpsm5qv393ga";
        private static readonly int ENCRYPTPASSWORDCOUNT = 16;
        private static readonly string PASSWORDCHARS = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly int PASSWORDCHARSLENGTH = PASSWORDCHARS.Length;
        private static readonly string SAVEPATH = "/Resources/SaveData";

        private static readonly bool OnPassword = false; // 暗号化をするかどうか

        /// <summary>
        /// AES暗号化(Base64)
        /// </summary>
        private static void EncryptAesBase64(string json, out string iv, out string base64)
        {
            byte[] src = Encoding.UTF8.GetBytes(json);
            byte[] dst;
            EncryptAes(src, out iv, out dst);
            base64 = Convert.ToBase64String(dst);
        }

        /// <summary>
        /// AES複合化(Base64)
        /// </summary>
        private static void DecryptAesBase64(string base64, string iv, out string json)
        {
            byte[] src = Convert.FromBase64String(base64);
            byte[] dst;
            DecryptAes(src, iv, out dst);
            json = Encoding.UTF8.GetString(dst).Trim('\0');
        }

        /// <summary>
        /// AES暗号化
        /// </summary>
        private static void EncryptAes(byte[] src, out string iv, out byte[] dst)
        {
            iv = CreatePassword(ENCRYPTPASSWORDCOUNT);
            dst = null;
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Mode = CipherMode.CBC;
                rijndael.KeySize = 256;
                rijndael.BlockSize = 128;

                byte[] key = Encoding.UTF8.GetBytes(ENCRYPTKEY);
                byte[] vec = Encoding.UTF8.GetBytes(iv);

                using (ICryptoTransform encryptor = rijndael.CreateEncryptor(key, vec))
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(src, 0, src.Length);
                    cs.FlushFinalBlock();
                    dst = ms.ToArray();
                }
            }
        }

        /// <summary>
        /// AES複合化
        /// </summary>
        private static void DecryptAes(byte[] src, string iv, out byte[] dst)
        {
            dst = new byte[src.Length];
            using (RijndaelManaged rijndael = new RijndaelManaged())
            {
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Mode = CipherMode.CBC;
                rijndael.KeySize = 256;
                rijndael.BlockSize = 128;

                byte[] key = Encoding.UTF8.GetBytes(ENCRYPTKEY);
                byte[] vec = Encoding.UTF8.GetBytes(iv);

                using (ICryptoTransform encryptor = rijndael.CreateEncryptor(key, vec))
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Read))
                {
                    cs.Read(dst, 0, dst.Length);
                }
            }
        }

        /// <summary>
        /// パスワード生成
        /// </summary>
        /// <param name="count"> 文字列数 </param>
        /// <returns> パスワード </returns>
        private static string CreatePassword(int count)
        {
            StringBuilder sb = new StringBuilder(count);
            for (int i = count - 1; i >= 0; i--)
            {
                char c = PASSWORDCHARS[UnityEngine.Random.Range(0, PASSWORDCHARSLENGTH)];
                sb.Append(c);
            }
            return sb.ToString();
        }

        private static string GetSavePath()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return Application.dataPath + SAVEPATH;
                case RuntimePlatform.Android:
                    return Application.persistentDataPath + SAVEPATH;
                default:
                    return Application.dataPath + SAVEPATH;
            }
        }

        public static void SavingGameData(T argData)
        {
            string json = JsonUtility.ToJson(argData);
            string savepath = GetSavePath();

            if (!OnPassword)
            {
                savepath += ".json";
                var writer = new StreamWriter(savepath, false);
                writer.Write(json);
                writer.Flush();
                writer.Close();

                Log.Info("Save Data Saving.");
                return;
            }

            string iv;
            string base64;
            EncryptAesBase64(json, out iv, out base64);

            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
            byte[] base64Bytes = Encoding.UTF8.GetBytes(base64);
            using (FileStream fs = new FileStream(savepath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                bw.Write(ivBytes.Length);
                bw.Write(ivBytes);

                bw.Write(base64Bytes.Length);
                bw.Write(base64Bytes);
            }

            Log.Info("Save Data Saving.");
        }

        public static bool LoadingGameData(ref T data)
        {
            string json;
            string savepath = GetSavePath();

            if (!OnPassword)
            {
                savepath += ".json";
                if (File.Exists(savepath))
                {
                    var reader = new StreamReader(savepath);
                    json = reader.ReadToEnd();
                    reader.Close();

                    Log.Info("Save Data Loading.");

                    data = JsonUtility.FromJson<T>(json);
                    return true;
                }
                else
                {
                    Log.Info("Save Data Creating.");

                    data = new T();
                    return false;
                }
            }

            byte[] ivBytes = null;
            byte[] base64Bytes = null;

            try
            {
                using (FileStream fs = new FileStream(savepath, FileMode.Open, FileAccess.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    int length = br.ReadInt32();
                    ivBytes = br.ReadBytes(length);

                    length = br.ReadInt32();
                    base64Bytes = br.ReadBytes(length);
                }

                string iv = Encoding.UTF8.GetString(ivBytes);
                string base64 = Encoding.UTF8.GetString(base64Bytes);
                DecryptAesBase64(base64, iv, out json);

                Log.Info("Save Data Loading.");

                data = JsonUtility.FromJson<T>(json);
                return true;
            }
            catch
            {
                Log.Info("Save Data Creating.");

                data = new T();
                return false;
            }
        }
    }

}
