using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")] 
    
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;
    
    
    private GameData gameData;
    
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
   public static DataPersistenceManager instance { get; private set; }

   private void Awake()
   {
      if (instance != null)
      {
       Debug.LogError("More than one DataPersistenceManager in scene.");  
       Destroy(this.gameObject); 
       return;
      }
      instance = this;
      
      DontDestroyOnLoad(this.gameObject);
      
      this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
   }

   private void OnEnable()
   {
       SceneManager.sceneLoaded += onSceneLoaded;
       SceneManager.sceneUnloaded += onSceneUnloaded;
   }

   private void OnDisable()
   {
       SceneManager.sceneLoaded -= onSceneLoaded;
       SceneManager.sceneUnloaded -= onSceneUnloaded;
   }

   public void onSceneLoaded(Scene scene, LoadSceneMode mode)
   {
       this.dataPersistenceObjects = FindAllDataPersistenceObjects();
       LoadGame();
   }

   public void onSceneUnloaded(Scene scene)
   { 
       SaveGame();
   }
   
   public void NewGame()
   { 
       this.gameData = new GameData();   
   }

   public void LoadGame()
   {
       this.gameData = dataHandler.Load();
       
       if (this.gameData == null)
       {
           Debug.Log("No data was found. A new game needs to be started before data can be loaded");
           return;
       }
       
       foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
       {
           dataPersistenceObj.LoadData(gameData);
       }
   }

   public void SaveGame()
   {
       if (this.gameData == null)
       {
           Debug.LogWarning("No data was found. A new game needs to be started before data can be saved");
           return;
       }
       foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
       {
           dataPersistenceObj.SaveData(ref gameData);
       }
       dataHandler.Save(gameData);
   }

   private void OnApplicationQuit()
   {
       SaveGame();
   }

   private List<IDataPersistence> FindAllDataPersistenceObjects()
   {
       IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
       
       return new List <IDataPersistence>(dataPersistenceObjects);
   }

   public bool hasGameData()
   {
       return gameData != null;
   }
}
