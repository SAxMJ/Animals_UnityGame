using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Explodable))]
public class ExplodeOnClick : MonoBehaviour {

	private Explodable _explodable;
	public GameObject sangre;

	void Start()
	{
		_explodable = GetComponent<Explodable>();
	}
	public void explotaJugador()
	{
		System.Random rand= new System.Random();

		int randX = rand.Next(600, 901);
        if (randX % 2 == 0)
        {
			randX = -randX;
        }

		int randY= rand.Next(200, 300);

		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.AddForce(new Vector2(randX, randY), ForceMode2D.Impulse);
		_explodable.explode();
		ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
		ef.doExplosion(transform.position);
		Instantiate(sangre, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);

	}
}
