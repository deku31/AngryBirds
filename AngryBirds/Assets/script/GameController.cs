using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    private Bird _shotBird;
    public BoxCollider2D TapCollider;

    private bool _isGameEnded = false;

    public List<Bird> Birds;
    public List<Enemy> Enemies;

    [Header("UI Txt Lv")]
    //UI text Pemberitahuan lv
    public Text lvTxt;
    public int lv;
    //waktu muncul text lv
    float timetxt;
    public GameObject panellv;
    [Header("UI ending")]
    //UI ending permainan
    public GameObject Ending;
    public GameObject NextLv;
    public Button Restart, Quit;
    public Text endingTxt;

    void Start()
    {
        Ending.SetActive(false);
        //text lv
        panellv.SetActive(true);
        lvTxt.text = "Level : " + (lv+1);
        timetxt = 60;
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;

        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];

    }
    //button next lv
    public void next()
    {
        if (lv<2)
        {
            Application.LoadLevel(lv + 1);

        }
    }
    //button restart
    public void restart()
    {
        Application.LoadLevel(lv);
    }
    //tombol quit
    public void quit() 
    {
        Application.Quit();
    }
    private void FixedUpdate()
    {
        //menghilangkan panellv
        if (timetxt>0)
        {
            timetxt -=1+Time.deltaTime;
        }
        else
        {
            panellv.SetActive(false);
        }
    }
    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }
    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
        }
        else
        {
            //menampilkan object UI
            Ending.SetActive(true);
            NextLv.SetActive(false);
            Restart.enabled = true;
            Quit.enabled = true;
            endingTxt.enabled = true;
            endingTxt.text = "Game Over";
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);

                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;
            //menampilkan object UI
            Ending.SetActive(true);
            NextLv.SetActive(true);
            Restart.enabled=true;
            Quit.enabled = true;
            endingTxt.enabled = true;
            endingTxt.text = "You Win. . . ";
        }
    }

}
