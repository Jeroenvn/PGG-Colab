using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 10;
    public float damage;

    private void FixedUpdate()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent(out IDamageable damageable);
        if (damageable == null) return;
        damageable.Damage(damage);
        Destroy(gameObject);
    }
}
