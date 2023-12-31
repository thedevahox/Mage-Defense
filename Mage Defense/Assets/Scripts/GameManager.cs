using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    //for text
    public Text textElement;
    public TextMeshProUGUI coinsValue;
    public TextMeshProUGUI waveCounter;
    
    public HealthBarScript SN;

    //
    public static GameManager gm;
    private int enemyCount;
    SpellUsage sU;
    public float chestHealth = 1000;
    public int round = 0;
    public bool isSpawning = false;
    public bool damage = false;
    public bool inProgress = false;


    //VFX
    private Animator anim;

    //SFX
    private AudioSource audioSource;

    public Music[] musics;

    private string currentMusicID;

    public AudioClip fireballClip;
    public AudioClip iceSpikesClip;
    public AudioClip earthquakeClip;
    public AudioClip blackHoleClip;



    //spawn manager
    public GameObject Goblin;
    public GameObject Orc;
    public GameObject Spider;
    public Vector3 spawnLocation = new Vector3(-233f, -23.4f, 28.42f);
    public float coins = 0;
    
    public void addCoins(float coins)
    {

        this.coins += coins;
    }
    IEnumerator wait1()
    {
        print("coro");
        isSpawning = true;
        yield return new WaitForSeconds(1);
        isSpawning = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
        SN.SetMaxHealth(chestHealth);


        spawnEnemies(round);

        sU = FindObjectOfType<SpellUsage>();

        gm = this;
        chestHealth = 1000;


        //Game Over screen
        

        //audio
        
        audioSource = GetComponent<AudioSource>();
    }

    public void attacking(float dmg)
    {
        if (damage)
        {
            chestHealth -= dmg;
            damage = false;
            SN.SetHealth(chestHealth);
        }

    }
    public IEnumerator spawnEnemies(int round)
    {
        if (!isSpawning)
        {
            inProgress = true;
            if (round <= 3)
            {
                for (int count = 0; count < round * 3; count++)
                {

                    spawnMob("Goblin");
                    yield return new WaitForSeconds(1);

                }
            }
            else if (round > 3 && round <= 10)
            {
                for (int count = 0; count < round * 2; count++)
                {
                    spawnMob("Goblin");
                    yield return new WaitForSeconds(1);
                }
                while (enemyCount != 0)
                {
                    yield return null;
                }
                for (int count = 0; count < round; count++)
                {
                    spawnMob("Spider");
                    yield return new WaitForSeconds(1);
                }

            }
            else if (round > 10 && round < 15)
            {
                for (int count = 0; count < round * 2; count++)
                {
                    spawnMob("Goblin");
                    yield return new WaitForSeconds(1);
                    spawnMob("Spider");
                }
            }
            if (round == 15)
            {
                for (int count = 0; count < 12; count++)
                {
                    spawnMob("Goblin");
                    yield return new WaitForSeconds(1);
                }
                while (enemyCount != 0)
                {
                    yield return null;
                }
                for (int count = 0; count < 10; count++)
                {
                    spawnMob("Goblin");
                    yield return new WaitForSeconds(1);
                    yield return new WaitForSeconds(1);
                    spawnMob("Spider");
                }
                while (enemyCount != 0)
                {
                    yield return null;
                }
                for (int count = 0; count < 10; count++)
                {
                    spawnMob("Goblin");
                    yield return new WaitForSeconds(1);
                }
                spawnMob("Orc");
                yield return new WaitForSeconds(1);
                spawnMob("Orc");
            }
            if (round == 16)
            {
                SceneManager.LoadScene("GameWin");
            }
            inProgress = false;
        }
    }
    public void spawnMob(string mobName)
    {
        if (mobName.Equals("Goblin"))
        {
            Instantiate(Goblin, spawnLocation, Quaternion.identity);
        }
        if (mobName.Equals("Spider"))
        {
            Instantiate(Spider, spawnLocation, Quaternion.identity);
        }
        if (mobName.Equals("Orc"))
        {
            Instantiate(Orc, spawnLocation, Quaternion.identity);
        }
    }

    //VFX


    // Update is called once per frame
    void Update()
    {
        if(chestHealth<=0)
        {
            GameOver();
        }
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        
        if(enemyCount==0 && !inProgress)
        {
            round++;
            sU.resetCooldowns();
            StartCoroutine(spawnEnemies(round));
        }
        textElement.text = "Health: " + chestHealth+" Wave: "+ round;
        coinsValue.text = "" + coins;
        waveCounter.text = "Wave: " + round;
    }

    //SFX
    private void Awake()
    {
        //create audiosource
        foreach (Music m in musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
           
        }
    }

    public void ChangeMusic(string newMusicID)
    {

        //stop current music
        foreach (Music m in musics)
        {
            if (m.musicID == currentMusicID)
            {
                m.source.Stop();
                break;
            }


        }

        //change currentmusicID

        currentMusicID = newMusicID;

        //play new music
        foreach (Music m in musics)
        {
            if (m.musicID == currentMusicID)
            {
                m.source.Play();
                audioSource.loop = true;
                break;
            }


        }


    }

    //*Game Over  SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void GameOver()
    {
    SceneManager.LoadScene("GameOver");
    
    }

    public void glueGunnerSFX()
    {
    audioSource.PlayOneShot(iceSpikesClip , 0.5f);
    }
}
