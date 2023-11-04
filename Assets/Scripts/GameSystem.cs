using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public PlayerController player; 
    public EnemySystem[] mobs;

    public GameObject start_window;//окно начала игры
    public GameObject dead_window;//окно поражени€
    public GameObject win_window;//окно победы

    bool win;
    private void Start()
    {
        Time.timeScale = 0;
        player = FindObjectOfType<PlayerController>();
        mobs = FindObjectsOfType<EnemySystem>();

        //закидываем всех врагов на карте в наш массив дл€ вычислени€ победы
        foreach (EnemySystem mob in mobs)
        {
            mob.GS = this;
        }
    }

    private void Update()
    {
        //провер€ем на смерть игрока
        if (player.hp<=0)
        {
            dead_window.SetActive(true);
            Time.timeScale = 0;
        }
    }
    //вызываетс€ из скрипта EnemySystem
    public void dead_mob()
    {
        //после каждого убитого врага провер€ем всех мобов на смерть
        for (int i=0;i<mobs.Length;i++)
        {
            if (mobs[i].dead)
            {
                win = true;
            }
            else
            {
                win = false;
                return;
            }
        }
        //по результатам проверки сверху обь€вл€ем победу если все убиты
        if (!win)
            return;
        //отображаем окно с выигрышем
        win_window.SetActive(true);
        //—тавим игру на паузу
        Time.timeScale = 0;
    }
    public void Restart_level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        start_window.SetActive(false);
    }
}
