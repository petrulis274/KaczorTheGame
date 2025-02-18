using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

public class FileDataHandler
{
   private string dataDirPath = "";
   
   private string dataFileName = "";
   
   private bool useEncryption = false;

   private readonly string ecntryptionCodeWord = "babaganoush";

   public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
   {
      this.dataDirPath = dataDirPath;
      this.dataFileName = dataFileName;
      this.useEncryption = useEncryption;
   }

   public GameData Load()
   {
      string FullPath = Path.Combine(dataDirPath, dataFileName);
      GameData loadedData = null;

      if (File.Exists(FullPath))
      {
         try
         {
            string dataToLoad = "";
            using (FileStream stream = new FileStream(FullPath, FileMode.Open))
            {
               using (StreamReader reader = new StreamReader(stream))
               {
                  dataToLoad = reader.ReadToEnd();
               }
            }

            if (useEncryption)
            {
               dataToLoad = EncryptDecrypt(dataToLoad);
            }
            
            loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            
         }
         catch (Exception e)
         {
            Debug.LogError("Error occured while trying to load the game from file: " + FullPath + "\n" + e);
         }
      }
      return loadedData;
   }

   public void Save(GameData data)
   {
      string FullPath = Path.Combine(dataDirPath, dataFileName);
      try
      {
         //create a directory path
         Directory.CreateDirectory(Path.GetDirectoryName(FullPath));
         
         string dataToStore = JsonUtility.ToJson(data, true);
         
         if (useEncryption)
         {
            dataToStore = EncryptDecrypt(dataToStore);
         }
         
         using (FileStream stream = new FileStream(FullPath, FileMode.Create))
         {
            using (StreamWriter writer = new StreamWriter(stream))
            {
               writer.Write(dataToStore);
            }
         }
      }
      catch (Exception e)
      {
         Debug.LogError("Error occured while trying to save data to file: " + FullPath + "\n" + e);
      }
   }

   private string EncryptDecrypt(string data)
   {
      string modifiedData = "";
      for (int i = 0; i < data.Length; i++)
      {
         modifiedData += (char) (data[i] ^ ecntryptionCodeWord[i % ecntryptionCodeWord.Length]);
      }
      return modifiedData;
   }
}
