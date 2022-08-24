using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Invader : Damagable
{
    public List<DamagableType> canHit;
    public Bullet bulletPrefab;
    public Transform bulletOrgin;
    public float bulletSpeed;
    public float shootMinDelay = 3f;
    public float shootMaxDelay = 6f;

    private Action<Invader> onInvaderDestroy;


    public Vector2Int ArrayPosition { get; private set; }
    public void Setup(Vector3 position,Action<Invader> onInvaderDestroy, Vector2Int arrayPosition)
    {
        transform.position = position;
        this.onInvaderDestroy = onInvaderDestroy;
        ArrayPosition = arrayPosition;

    }
        public override void GetDamage()
    {
        onInvaderDestroy?.Invoke(this);
        base.GetDamage();
    }

    public void StartShooting()
    {
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {

    while(true)
        {
        float delay = UnityEngine.Random.Range(shootMinDelay, shootMaxDelay);
            yield return new WaitForSeconds(delay);
            var bullet = Instantiate(bulletPrefab);
            bullet.Setup(bulletOrgin.position, bulletSpeed, Vector3.down, canHit);
        }
    }
    
}
