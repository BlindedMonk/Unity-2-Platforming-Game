using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerManager : MonoBehaviour
{
    //  private List<Collectibles> inventory = new List<Collectibles>();
    playerinfo info;
    public TextMeshProUGUI inventoryText;
    public TextMeshProUGUI descriptionText;
    private int currentIndex;
    // Player specific variables
    //private int health;
    //private int score;

    // Boolean values
    private bool isGamePaused = false;

    // UI stuff
    public Text healthText;
    public Text scoreText;
    public GameObject pauseMenu;
    public GameObject winMenu;
    public GameObject loseMenu;

    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.FindWithTag("Info").GetComponent<playerinfo>();
        foreach (Collectibles item in info.inventory)
        {
            item.player = this.gameObject;
        }
        // Makes sure game is "unpaused"
        isGamePaused = false;
        Time.timeScale = 1.0f;

        // Make sure all menus are filled in
        FindAllMenus();

        //Start player with initial health and score
        //health = 100;
        //score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + info.health.ToString();
        scoreText.text  = "Score:  " + info.score.ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (info.health <= 0)
        {
            LoseGame();
        }
        if(info.inventory.Count == 0)
        {
            //if inventory is empty
            inventoryText.text = "Current Selection: None";
            descriptionText.text = "";
        }
        else
        {
            inventoryText.text = "Current Selection: " + info.inventory[currentIndex].collectibleName + " " + currentIndex.ToString();
            descriptionText.text = "Press [e] to " + info.inventory[currentIndex].description;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            //using
            info.inventory[currentIndex].Use();
            info.inventory.RemoveAt(currentIndex);
            currentIndex = (currentIndex + 1) % info.inventory.Count;
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if(info.inventory.Count > 0)
            {
                //move to the next item in the inventory 
                currentIndex = (currentIndex + 1) % info.inventory.Count;
            }
        }
    }

   void FindAllMenus()
    {
        if (healthText == null)
        {
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
        }
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        if (winMenu == null)
        {
            winMenu = GameObject.Find("WinGameMenu");
            winMenu.SetActive(false);
        }
        if (loseMenu == null)
        {
            loseMenu = GameObject.Find("LoseGameMenu");
            loseMenu.SetActive(false);
        }
        if (pauseMenu == null)
        {
            pauseMenu = GameObject.Find("PauseGameMenu");
            pauseMenu.SetActive(false);
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0.0f;
        winMenu.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0.0f;
        loseMenu.SetActive(true);
    }

    public void PauseGame()
    {
        if (isGamePaused)
        {
            // Unpause game
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            isGamePaused = false;
        }
        else
        {
            // Pause game
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            isGamePaused = true;
        }
    }

    public void ChangeHealth(int value)
    {
        info.health += value;
    }

    public void ChangeScore(int value)
    {
        info.score += value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Collectibles>() != null)
        {
            collision.GetComponent<Collectibles>().player = this.gameObject;
            collision.gameObject.transform.parent=null;
            info.inventory.Add(collision.GetComponent<Collectibles>());
            collision.gameObject.SetActive(false);
        }
    }

}
