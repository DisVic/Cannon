using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float speed;
    public float friction;
    public float lerpSpeed;
    private float xDegrees;
    private float yDegrees;
    private Quaternion fromRotation;
    private Quaternion toRotation;
    private Camera camera;

    public GameObject cannonBall;
    private Rigidbody cannonballRB;
    public Transform shotPos;
    public GameObject explosion;
    public float firePower = 2000f;

    public AudioClip audio;
    public AudioSource aSource;

    private float maxUpAngle = 90f;
    private float maxDownAngle = 20f;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        MoveCannon();
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
    }

    public void MoveCannon()
    {
        if (Input.GetKey(KeyCode.W)) xDegrees -= speed * friction;        
        else if (Input.GetKey(KeyCode.S)) xDegrees += speed * friction;
        else if (Input.GetKey(KeyCode.A)) yDegrees -= speed * friction;
        else if (Input.GetKey(KeyCode.D)) yDegrees += speed * friction;
        xDegrees = Mathf.Clamp(xDegrees, -maxUpAngle, maxDownAngle);//ограничения подвижности пушки

        fromRotation = transform.rotation;
        toRotation = Quaternion.Euler(xDegrees, yDegrees, 0);
        transform.rotation = Quaternion.Lerp(fromRotation, toRotation, lerpSpeed * Time.deltaTime);
    }
    public void Fire()
    {
        GameObject cannonballCopy = Instantiate(cannonBall, shotPos.position, transform.rotation) as GameObject;
        cannonballRB = cannonballCopy.GetComponent<Rigidbody>();
        cannonballRB.AddForce(transform.forward * firePower);
        GameObject spawnedExplosion = Instantiate(explosion, shotPos.position, shotPos.rotation);
        Destroy(spawnedExplosion, 1f);
        aSource.PlayOneShot(audio);
    }
}
