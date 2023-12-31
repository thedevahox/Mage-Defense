using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUsage : MonoBehaviour
{
    public GameObject lightningSpellPrefab;
    public GameObject fireballSpellPrefab;
    public GameObject earthquakeSpellPrefab;
    public GameObject blackHoleSpellPrefab;
    public GameObject glueTowerPrefab;

    public GameObject thunderSpellRefresherPrefab;
    public GameObject fireballSpellRefresherPrefab;
    public GameObject earthquakeSpellRefresherPrefab;
    public GameObject blackHoleSpellRefresherPrefab;



    public float lightningCooldown = 0f;
    public float lightningCooldownRate = 5f;
    public bool isLightningCooldown = false;

    public float fireballCooldown = 0f;
    public float fireballCooldownRate = 5f;
    public bool isFireballCooldown = false;

    public float earthquakeCooldown = 0f;
    public float earthquakeCooldownRate = 5f;
    public bool isEarthquakeCooldown = false;

    public float blackHoleCooldown = 0f;
    public float blackHoleCooldownRate = 15f;
    public bool isBlackHoleCooldown = false;

    public float glueCooldown = 0f;
    public float glueCooldownRate = 5f;
    public bool isGlueCooldown = false;

    public float topBound=14f;
    public float bottomBound=-0.5f;
    public float leftBound =-267f;
    public float rightBound=-237f;

    //sfx
    public AudioClip fireballClip;
    public AudioClip iceSpikesClip;
    public AudioClip earthquakeClip;
    public AudioClip blackHoleClip;
    
    private AudioSource audioSource;

    public Vector3 screenPosition;

    public Vector3 worldPosition;

    public float screenPositionDepth = 44f;

    public Image fireballImage;
    public Image iceSpikesImage;
    public Image earthquakeImage;
    public Image blackHoleImage1;
    public Image blackHoleImage2;

    // Start is called before the first frame updat
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        iceSpikesImage.fillAmount = 0;
        fireballImage.fillAmount = 0;
        earthquakeImage.fillAmount = 0;
        blackHoleImage1.fillAmount = 0;
        blackHoleImage2.fillAmount = 0;
    }

    IEnumerator ThunderSpellRefresher()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(thunderSpellRefresherPrefab, worldPosition, thunderSpellRefresherPrefab.transform.rotation);
    }
    IEnumerator FireballSpellRefresher()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(fireballSpellRefresherPrefab, worldPosition, fireballSpellRefresherPrefab.transform.rotation);
    }
    IEnumerator EarthquakeSpellRefresher()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(earthquakeSpellRefresherPrefab, worldPosition, earthquakeSpellRefresherPrefab.transform.rotation);
    }
    IEnumerator BlackHoleSpellRefresher()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(blackHoleSpellRefresherPrefab, worldPosition, blackHoleSpellRefresherPrefab.transform.rotation);
    }
    public void resetCooldowns()
    {
        lightningCooldown = 0f;
        fireballCooldown = 0f;
        earthquakeCooldown = 0f;
        glueCooldown = 0f;
        iceSpikesImage.fillAmount = 0f;
        fireballImage.fillAmount = 0f;
        earthquakeImage.fillAmount = 0f;
        
    }
    //FIX ROTATION AND DEPTH
    void PlaceGlue()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isGlueCooldown && GameManager.gm.coins >= 30)
        {
            Instantiate(glueTowerPrefab, worldPosition, /*ROTATION GOES HERE PLEASE FIX IT*/ glueTowerPrefab.transform.rotation);
            isGlueCooldown = true;
            glueCooldown = 1f;



            GameManager.gm.addCoins(-30);

        }
        if (isGlueCooldown)
        {
            glueCooldown -= 1 / glueCooldownRate * Time.deltaTime;
            if (glueCooldown <= 0f)
            {
                glueCooldown = 0f;
                isGlueCooldown = false;
            }
        }
    }


    void CastLightning()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isLightningCooldown)
        {
            Instantiate(lightningSpellPrefab, worldPosition, lightningSpellPrefab.transform.rotation);
            StartCoroutine(ThunderSpellRefresher());
            audioSource.PlayOneShot(iceSpikesClip , 1f);
            isLightningCooldown = true;
            lightningCooldown = 1;
            iceSpikesImage.fillAmount = 1;
        }
        if(isLightningCooldown)
        {
            lightningCooldown -= 1 / lightningCooldownRate * Time.deltaTime;
            if(lightningCooldown <= 0f)
            {
                lightningCooldown = 0f;
                isLightningCooldown = false;
            }
            iceSpikesImage.fillAmount -= 1 / lightningCooldownRate * Time.deltaTime;
            if(iceSpikesImage.fillAmount <= 0f)
            {
                iceSpikesImage.fillAmount = 0f;
            }
        }
    }
    void CastFireball()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isFireballCooldown)
        {
            Instantiate(fireballSpellPrefab, worldPosition, fireballSpellPrefab.transform.rotation);
            StartCoroutine(FireballSpellRefresher());
            audioSource.PlayOneShot(fireballClip, 1f);
            isFireballCooldown = true;
            fireballCooldown = 1f;
            fireballImage.fillAmount = 1;

        }
        if (isFireballCooldown)
        {
            fireballCooldown -= 1 / fireballCooldownRate * Time.deltaTime;
            if (fireballCooldown <= 0f)
            {
                fireballCooldown = 0f;
                isFireballCooldown = false;
            }
            fireballImage.fillAmount -= 1 / fireballCooldownRate * Time.deltaTime;
            if (fireballImage.fillAmount <= 0f)
            {
                fireballImage.fillAmount = 0f;
            }
        }
    }
    void CastEarthquake()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isEarthquakeCooldown)
        {
            Instantiate(earthquakeSpellPrefab, worldPosition, earthquakeSpellPrefab.transform.rotation);
            StartCoroutine(EarthquakeSpellRefresher());
            audioSource.PlayOneShot(earthquakeClip , 1f);
            isEarthquakeCooldown = true;
            earthquakeCooldown = 1f;
            earthquakeImage.fillAmount = 1;

        }
        if (isEarthquakeCooldown)
        {
            earthquakeCooldown -= 1 / earthquakeCooldownRate * Time.deltaTime;
            if (earthquakeCooldown <= 0f)
            {
                earthquakeCooldown = 0f;
                isEarthquakeCooldown = false;
            }
            earthquakeImage.fillAmount -= 1 / earthquakeCooldownRate * Time.deltaTime;
            if (earthquakeImage.fillAmount <= 0f)
            {
                earthquakeImage.fillAmount = 0f;
            }
        }
    }
    void CastBlackHole()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isBlackHoleCooldown)
        {
            Instantiate(blackHoleSpellPrefab, worldPosition, blackHoleSpellPrefab.transform.rotation);
            StartCoroutine(BlackHoleSpellRefresher());
            audioSource.PlayOneShot(blackHoleClip , 1f);
            isBlackHoleCooldown = true;
            blackHoleCooldown = 1f;
            blackHoleImage1.fillAmount = 1;
            blackHoleImage2.fillAmount = 1;

        }
        if (isBlackHoleCooldown)
        {
            blackHoleCooldown -= 1 / blackHoleCooldownRate * Time.deltaTime;
            if (blackHoleCooldown <= 0f)
            {
                blackHoleCooldown = 0f;
                isBlackHoleCooldown = false;
            }
            blackHoleImage1.fillAmount -= 1 / blackHoleCooldownRate * Time.deltaTime;
            if (blackHoleImage1.fillAmount <= 0f)
            {
                blackHoleImage1.fillAmount = 0f;
            }
            blackHoleImage2.fillAmount -= 1 / blackHoleCooldownRate * Time.deltaTime;
            if (blackHoleImage2.fillAmount <= 0f)
            {
                blackHoleImage2.fillAmount = 0f;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + screenPositionDepth;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = worldPosition;

        CastLightning();
        CastFireball();
        CastEarthquake();
        CastBlackHole();
        PlaceGlue();
    }
}
