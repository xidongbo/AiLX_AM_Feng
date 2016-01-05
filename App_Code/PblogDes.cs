using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// PblogDes 的摘要说明
/// </summary>
public class PblogDes
{
    private string key;
    public PblogDes()
	{
        this.key = "PblogKey";
        //
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public PblogDes(string key)
    {
        this.key = key;
    }

    public string Decrypt(string s)
    {
        return this.Decrypt(s, this.key);
    }

    private string Decrypt(string pToDecrypt, string sKey)
    {
        DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        byte[] buffer = new byte[pToDecrypt.Length / 2];
        for (int i = 0; i < (pToDecrypt.Length / 2); i++)
        {
            int num2 = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 0x10);
            buffer[i] = (byte)num2;
        }
        provider.Key = Encoding.ASCII.GetBytes(sKey);
        provider.IV = Encoding.ASCII.GetBytes(sKey);
        MemoryStream stream = new MemoryStream();
        CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
        stream2.Write(buffer, 0, buffer.Length);
        stream2.FlushFinalBlock();
        new StringBuilder();
        return Encoding.Default.GetString(stream.ToArray());
    }

    public string Encrypt(string s)
    {
        return this.Encrypt(s, this.key);
    }

    private string Encrypt(string pToEncrypt, string sKey)
    {
        DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
        provider.Key = Encoding.ASCII.GetBytes(sKey);
        provider.IV = Encoding.ASCII.GetBytes(sKey);
        MemoryStream stream = new MemoryStream();
        CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
        stream2.Write(bytes, 0, bytes.Length);
        stream2.FlushFinalBlock();
        StringBuilder builder = new StringBuilder();
        foreach (byte num in stream.ToArray())
        {
            builder.AppendFormat("{0:X2}", num);
        }
        builder.ToString();
        return builder.ToString();
    }
}
