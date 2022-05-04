using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour { 

    private float horizontalBoundary = 22;

    private float speed = 0.0f;

    public GameObject hayBalePrefab;
    public Transform haySpawnPoint;
    public float shootInterval;

    private float shootTimer;

    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float acceleration = 3.0f;
    [SerializeField]
    private float decceleration = 5.0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        
    }

    private void UpdateMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        UpdateShooting();

       if (moveHorizontal == 0)
        {
            speed -= Mathf.Sign(speed) * decceleration * Time.deltaTime;

            if (Mathf.Abs(speed) < 0.1)
            {
                speed = 0;
            }
        }
        else
        {
            speed += moveHorizontal * acceleration* Time.deltaTime;
        }

        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

        transform.Translate(transform.right * speed * Time.deltaTime);
        float oldPos = transform.position.x;

        transform.position= new Vector3( Mathf.Clamp(transform.position.x, -horizontalBoundary, horizontalBoundary),transform.position.y,transform.position.z);

        if (transform.position.x != oldPos)
        {
            speed = 0;
        }

        //transform.position.x += speed * Time.deltaTime * ;
    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space))
        {
            shootTimer = shootInterval;
            ShootHay();
        }
    }

    private void ShootHay()
    {
        SoundManager.Instance.playShootSound();
        Instantiate(hayBalePrefab, haySpawnPoint.position, Quaternion.identity);
    }
}
