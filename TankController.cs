using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : Damagable
{
    public float movementSpeed = 5f;
    public Animator animator;
    public float maxPosition = 8f;
    public Bullet bulletPrefab;
    public Transform bulletOrgin;
    public float bulletspeed = 5f;
    public float shootRate = 2f;
    public List<DamagableType> canHit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }
    private void Movement()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        if ((direction > 0 && transform.position.x < maxPosition) || (direction < 0 && transform.position.x > -maxPosition))
        {
            transform.position += Vector3.right * direction * movementSpeed * Time.deltaTime;

        }
        animator.SetFloat("Direction", direction);
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.Setup(bulletOrgin.position, bulletspeed, Vector3.up,canHit);

        }
    }
}