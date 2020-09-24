using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Collectibles
{
    // Start is called before the first frame update
    private void Start()
    {
        collectibleName = "HealthPotion";
        description = "increase health by 25 points"; 
        //DontDestroyOnLoad(this.GameObject);
    }

    // Update is called once per frame
    override public void Use()
    {
        player.GetComponent<playerManager>().ChangeHealth(25);
    }
}