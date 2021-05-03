using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PasswordManage
{
    static class FileEncryptor
    {
        public static void fileEncrypt(PasswordContainer passwordContainer, string PathFile, byte[] key)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(PasswordContainer));

            using (FileStream fileStream = new FileStream(PathFile, FileMode.OpenOrCreate))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;

                    byte[] iv = aes.IV;
                    fileStream.Write(iv, 0, iv.Length);


                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter encryptWriter = new StreamWriter(cryptoStream))
                        {
                            formatter.Serialize(encryptWriter, passwordContainer);
                        }
                    }

                }
            }
        }
        public static PasswordContainer fileDecrypt(string PathFile, byte[] key)
        {


            XmlSerializer formatter = new XmlSerializer(typeof(PasswordContainer));

            using (FileStream fileStream = new FileStream(PathFile, FileMode.Open))
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] iv = new byte[aes.IV.Length];
                    int numBytesToRead = aes.IV.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                        if (n == 0) break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                    {
                        using (StreamReader decryptReader = new StreamReader(cryptoStream))
                        {
                            return (PasswordContainer)formatter.Deserialize(decryptReader);
                        }
                    }
                }
            }
        }
    }
}
