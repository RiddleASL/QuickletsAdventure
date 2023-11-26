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
    public class Position{
        public float x;
        public float y;
        public float z;
    }
    [System.Serializable]
    public class Info{
        public Position position;
        public int health;
        public int selectedBlock;
    }

    // Inventory Stuff
    [System.Serializable]
    public class Blocks{
        public string name;
        public int block_id;
        public int count;
    }
    [System.Serializable]
    public class Inventory{
        public List<Blocks> blocks;
    }    

    [System.Serializable]
    public class PlayerProps{
        public Inventory inventory;
        public Info playerInfo;
    }

    public PlayerProps full = new PlayerProps();
    // Start is called before the first frame update
    void Start()
    {
        if(json == null){
            json = Resources.Load<TextAsset>("JSON/playerProps");
        }
        full = JsonUtility.FromJson<PlayerProps>(json.text);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            SaveData();
        }
    }

    void SaveData(){
        string saveData = JsonUtility.ToJson(full);
        File.WriteAllText(Application.dataPath + "/Scripts/Player/playerProps.json", saveData);
    }

    public void blockCount(int block_id, int count){
        Debug.Log(1);
        for(int i = 0; i < full.inventory.blocks.Count; i++){
            if(full.inventory.blocks[i].block_id == block_id){
                full.inventory.blocks[i].count += count;
            }
        }
    }

    public Inventory getInv() => full.inventory;
}
