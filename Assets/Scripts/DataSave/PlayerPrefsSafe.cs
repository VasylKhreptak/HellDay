using System;
using UnityCipher;
using UnityEngine;

public static class PlayerPrefsSafe 
{
    private static string _password = "&aHB3FcB5vb@Q@s3$H25^WVLfazT$%zuf4&aEAYZphLJGk8*bK%Fk" +
                                      "Uh?-T*z#WL&Ws=kSajE=Q%JMcp#q%QBX9xQCgRTzUZhv5xzD!zS$k" +
                                      "8&jkR7b!jChj4WcgL43zH7%^45465423x5%^&$%$#$@23434d";

    public static void SetString(string key, string value)
    {
        string encryptedValue = RijndaelEncryption.Encrypt(value, _password);
        
        PlayerPrefs.SetString(key, encryptedValue);
    }

    public static string GetString(string key)
    {
        if (PlayerPrefs.HasKey(key) == false)
        {
            throw new NotImplementedException("No key: " + (key));
        }
        
        string decrypted = RijndaelEncryption.Decrypt(PlayerPrefs.GetString(key), _password);
        
        return decrypted;
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}