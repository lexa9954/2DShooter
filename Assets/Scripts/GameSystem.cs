using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public PlayerController player;
    public EnemySystem[] mobs;

    public GameObject dead_window;
    public GameObject win_window;

    bool win;
    private void Start()
    {
        Time.timeScale = 1;
        player = FindObjectOfType<PlayerController>();
        mobs = FindObjectsOfType<EnemySystem>();

        foreach (EnemySystem mob in mobs)
        {
            mob.GS = this;
        }
    }

    private void Update()
    {
        if (player.hp<=0)
        {
            dead_window.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void dead_mob()
    {
        for (int i=0;i<mobs.Length;i++)
        {
            Debug.Log(mobs[i]);
            if (!mobs[i])
            {
                win = true;
            }
            else
            {
                win = false;
            }
        }
        if (!win)
            return;
        win_window.SetActive(true);
        Time.timeScale = 0;
    }
    public void Restart_level()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
