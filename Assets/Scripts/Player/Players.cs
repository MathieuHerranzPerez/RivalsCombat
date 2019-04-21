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
                listRes.Add(p);
        }

        if(listRes.Count <= 0)  // in case we have no player
        {
            Player p1 = transform.GetChild(0).GetComponent<Player>();
            p1.SetControllerNumber(1);
            Player p2 = transform.GetChild(2).GetComponent<Player>();
            p2.SetControllerNumber(3);

            listRes.Add(p1);
            listRes.Add(p2);
        }

        return listRes;
    }
}
