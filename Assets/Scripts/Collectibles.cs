using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectibles : MonoBehaviour
{
    public string collectibleName;
    public string description; 
    public GameObject player;

    public abstract void Use();
    


    
}
