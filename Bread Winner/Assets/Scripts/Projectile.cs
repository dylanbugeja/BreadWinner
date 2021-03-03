using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30;
    public Vector3 launchOffset;
    public bool Throwable;

    public float splashRange = 1;

    private void Start()
    {
        if (Throwable)
        {
            var direction = transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);

        Destroy(gameObject, 4f);
    }

    public void Update()
    {
        if (!Throwable)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (splashRange > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
            foreach (var hitCollider in hitColliders)
            {
                if (true)
                {
                    var closesPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closesPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(splashRange, 0, distance);
                }
            }
        }
    }
}
