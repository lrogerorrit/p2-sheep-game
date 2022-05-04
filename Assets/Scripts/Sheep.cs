using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool wasHitByHay;
    public float shrinkRate= 0.1f;
    
    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;
    private bool doShrink = false;
    private SheepSpawner sheepSpawner;

    public float heartOffset;
    public GameObject heartPrefab;

    [Range(0.0f,10.0f)]
    public float runChance;
    [Min(1.0f)]
    public float runMultiplier;
    private float speedMultiplier;
    
    

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
        speedMultiplier = (Random.Range(0.0f, 10.0f) < runChance)?runMultiplier:1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime*speedMultiplier);
        if (doShrink)
        {
            transform.localScale -= new Vector3(shrinkRate, shrinkRate, shrinkRate)*Time.deltaTime;
            if (transform.localScale.x <= 0.0)
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
        }
        
    }

    private void HitByHay()
    {

        sheepSpawner.RemoveSheepFromList(gameObject);
        
        wasHitByHay = true;
        doShrink = true;
        runSpeed = 0;
        GameStateManager.Instance.SheepSaved();
        SoundManager.Instance.playSheepHitSound();
        Destroy(gameObject, gotHayDestroyDelay);
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
    }

    private void Drop()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);
        GameStateManager.Instance.SheepDropped();
        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        SoundManager.Instance.playSheepDroppedSound();
        Destroy(gameObject, dropDestroyDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !wasHitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
            Drop();
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
