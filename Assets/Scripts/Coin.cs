using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectibles
{
    // Start is called before the first frame update
    private void Start()
    {
        collectibleName = "Coin";
        description = "increase score by 10"; 
        //DontDestroyOnLoad(this.GameObject);
    }

    // Update is called once per frame
    override public void Use()
    {
        player.GetComponent<playerManager>().ChangeScore(10);
    }
}
