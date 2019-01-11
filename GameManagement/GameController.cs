using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public GameData gameData;

    [HideInInspector]
    public string gameDataFile;
    [HideInInspector]
    public string stage;
    [HideInInspector]
    public string level;

    private void Awake()
    {
        if(gameController == null)
        {
            DontDestroyOnLoad(gameObject);
            gameController = this;
        }
        else if(gameController != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameDataFile = "/gameData.dat";
        if(!Load())
        {
            stage = "Stage_01";
            level = "Level_01";
        }
    }

    public void Save(string stage, string level)
    {
        Debug.Log(Application.persistentDataPath);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + gameDataFile);

        GameData data = new GameData();
        data.stage = stage;
        data.level = level;

        bf.Serialize(file, data);
        file.Close();
    }

    public bool Load()
    {
        if (File.Exists(Application.persistentDataPath + gameDataFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + gameDataFile, FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            if (CheckGameData(data))
            {
                stage = data.stage;
                level = data.level;

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public bool CheckGameData(GameData data)
    {
        if(data.stage == "Stage_1" || data.stage == "Stage_2" || data.stage == "Stage_3" || data.stage == "Stage_4")
        {
            for(int i = 0; i <= 16; i++)
            {
                if(data.level == "Level_" + i)
                {
                    return true;
                }
            }
        }

        return false;
    }

    [System.Serializable]
    public class GameData
    {
        [HideInInspector]
        public string stage;
        [HideInInspector]
        public string level;
    }
}
