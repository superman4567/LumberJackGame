using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistance> dataPersistenceObjects;
    public static DataPersistenceManager instance { get; private set; }
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData= new GameData();
    }

    public void LoadGame()
    {
        //TODO - Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        //if no data can be loaded, initialize a new game
        if (this.gameData == null)
        {
            Debug.Log("No Data was found. Initializing data to defaults");
            NewGame();
        }

        //TODO - push the loaded data to all other scripts that need it
        foreach (IDataPersistance dataPersistanceObj in dataPersistenceObjects) 
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //TODO - pass the data to other scripts so they can update it
        foreach (IDataPersistance dataPersistanceObj in dataPersistenceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        //TODO - save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
       SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        //How do I read this?
        IEnumerable<IDataPersistance> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistenceObjects);
    }
}
