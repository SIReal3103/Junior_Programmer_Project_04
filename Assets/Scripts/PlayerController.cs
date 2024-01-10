using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private bool hasPowerup;

    public float powerupStrength;

    public GameObject powerIndicator;

    public GameObject misslePrefab;
    private GameObject tmpMissle;
    Coroutine powerUpCoundown;

    public PowerUpType currentPowerUp = PowerUpType.None;
 
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput);
        powerIndicator.transform.position = transform.position + new Vector3(0, 0.9f, 0); 

        if (Input.GetKeyDown(KeyCode.F) && currentPowerUp == PowerUpType.Missle)
        {
            LaunchMissle();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback) 
        {
            Vector3 direction = collision.transform.position - transform.position;
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            enemyRb.AddForce(direction * powerupStrength, ForceMode.Impulse);
            Debug.Log("Player just collide with: " + collision.gameObject);
        }
    }

    void LaunchMissle()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpMissle = Instantiate(misslePrefab, transform.position, Quaternion.identity);
            tmpMissle.GetComponent<MissleBehauviour>().Fire(enemy.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().type;
            powerIndicator.SetActive(true);

            if (powerUpCoundown != null) StopCoroutine(powerUpCoundown);
            powerUpCoundown = StartCoroutine(PowerupCountdownCoroutine());

            Destroy(other.gameObject);
        }
    }

    IEnumerator PowerupCountdownCoroutine()
    {
        yield return new WaitForSeconds(5);

        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerIndicator.SetActive(false);
    }
}
