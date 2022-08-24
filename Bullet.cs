using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private List<DamagableType> canHit;

    private bool isSet;
    public void Setup(Vector3 position, float speed, Vector3 direction, List<DamagableType> canHit )
    {
        transform.position = position;
        this.speed = speed;
        this.direction = direction;
        this.canHit = canHit;
        isSet = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (isSet)
        {
            transform.position += direction * speed * Time.deltaTime;

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable!= null && canHit.Contains(damagable.type))
        {
            damagable.GetDamage();
            

        }
        Destroy(gameObject);
    }
}
