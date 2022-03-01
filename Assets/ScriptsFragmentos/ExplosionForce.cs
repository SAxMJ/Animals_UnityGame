using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ExplosionForce : MonoBehaviour
{
    public float force = 50;
    public float radius = 5;
    public float upliftModifer = 5;
    System.Random aleat = new System.Random();
    int seed;

    /// <summary>
    /// create an explosion force
    /// </summary>
    /// <param name="position">location of the explosion</param>
    public void doExplosion(Vector3 position)
    {
        Debug.Log("doExplosion");
        transform.localPosition = position;
        this.waitAndExplode();
        seed = aleat.Next(0, 1000);
    }

    /// <summary>
    /// exerts an explosion force on all rigidbodies within the given radius
    /// </summary>
    /// <returns></returns>
	private void waitAndExplode()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D coll in colliders)
        {
            if (coll.GetComponent<Rigidbody2D>())// && coll.name != "hero")
            {
                AddExplosionForce(coll.GetComponent<Rigidbody2D>(), force, transform.position, radius, upliftModifer);
            }
        }
    }

    /// <summary>
    /// adds explosion force to given rigidbody
    /// </summary>
    /// <param name="body">rigidbody to add force to</param>
    /// <param name="explosionForce">base force of explosion</param>
    /// <param name="explosionPosition">location of the explosion source</param>
    /// <param name="explosionRadius">radius of explosion effect</param>
    /// <param name="upliftModifier">factor of additional upward force</param>
    private void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier = 0)
    {
        seed++;
        System.Random rand = new System.Random(seed);
        
        int randX = rand.Next(4, 9);
        if (randX % 2 == 0)
        {
            randX = -randX;
        }

        int randY = rand.Next(2, 8);

        body.AddForce(new Vector2(randX, randY), ForceMode2D.Impulse);
       

    }
}
