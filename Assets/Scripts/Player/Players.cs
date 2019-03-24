using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public static Players Instance;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /**
     * Get the list of the active players
     */ 
    public List<Player> GetPlayerList()
    {
        List<Player> listRes = new List<Player>();
        foreach(Transform child in transform)
        {
            Player p = child.GetComponent<Player>();
            if(p.isPlayed)
                listRes.Add(child.GetComponent<Player>());
        }
        return listRes;
    }
}
