using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameStateManager Instance;

    [HideInInspector]
    public int sheepSaved;
    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver;
    public SheepSpawner sheepSpawner;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Title");
            
        }
        
    }
    private void GameOver()
    {
        sheepSpawner.canSpawn = false;
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();

    }

    public void SheepSaved()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();

    }


    public void SheepDropped()
    {
        UIManager.Instance.UpdateSheepDropped();
        sheepDropped++;

        if (sheepDropped >= sheepDroppedBeforeGameOver)
        {
            GameOver();
        }
    }
    
}
