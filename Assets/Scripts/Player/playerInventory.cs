using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    [SerializeField] TextAsset json;

    //Player Stuff
    [System.Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }
    [System.Serializable]
    public class Info
    {
        public int health;
        public int selectedBlock;
        public int collected;
    }

    // Inventory Stuff
    [System.Serializable]
    public class Blocks
    {
        public string name;
        public int block_id;
        public int count;
    }
    [System.Serializable]
    public class Inventory
    {
        public List<Blocks> blocks;
    }

    //Global Stuff
    [System.Serializable]
    public class Audio{
        public float master = 1f;
        public float music = 1f;
        public float sfx = 1f;
    }

    [System.Serializable]
    public class GlobalInfo
    {
        public Audio audio;
    }

    [System.Serializable]
    public class PlayerProps
    {
        public Inventory inventory;
        public Info playerInfo;
        public GlobalInfo globalInfo;
        public float sensitivity = 5f;
    }

    public PlayerProps full = new PlayerProps();
    bool newSave = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Directory.Exists(Application.persistentDataPath + "/saveData"))
        {
            getData();
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saveData");
            newSave = true;
            getData();
        }

        //set block counts to 0
        for (int i = 0; i < full.inventory.blocks.Count; i++)
        {
            full.inventory.blocks[i].count = 0;
        }
        //set health to 3
        full.playerInfo.health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Application.persistentDataPath);
    }

    //Public Functions for Player Info
    public void changeHealth(int amount)
    {
        full.playerInfo.health += amount;
    }

    //Save All Data
    public void SaveGame()
    {
        SaveData();
    }

    void SaveData()
    {
        string saveData = JsonUtility.ToJson(full);
        File.WriteAllText(Application.persistentDataPath + "/saveData/saveFile.json", saveData);
    }

    void getData()
    {
        if(newSave){
            full = JsonUtility.FromJson<PlayerProps>(json.ToString());
            File.WriteAllText(Application.persistentDataPath + "/saveData/saveFile.json", json.ToString());
        }else{
            string saveData = File.ReadAllText(Application.persistentDataPath + "/saveData/saveFile.json");
            full = JsonUtility.FromJson<PlayerProps>(saveData);
        }
    }

    public void blockCount(int block_id, int count)
    {
        Debug.Log(1);
        for (int i = 0; i < full.inventory.blocks.Count; i++)
        {
            if (full.inventory.blocks[i].block_id == block_id)
            {
                full.inventory.blocks[i].count += count;
            }
        }
    }

    public Inventory getInv() => full.inventory;
}
