using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeSpellScript : MonoBehaviour
{
    public float damage=5;
    private bool doneDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        //mS = FindObjectOfType<mobScript>();
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!doneDamage)
            { 
                collision.gameObject.GetComponent<mobScript>().doDamage(damage);
                doneDamage = true;
            }
         }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
