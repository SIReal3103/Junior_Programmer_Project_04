using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleBehauviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15f;
    private bool homing;

    private float missleStrength = 15f;
    private float aliveTime = 5f;

    private void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(this.gameObject, aliveTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRb.AddForce(away * missleStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
