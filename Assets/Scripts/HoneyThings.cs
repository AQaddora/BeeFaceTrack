using UnityEditor;
using UnityEngine;
public class HoneyThings : MonoBehaviour
{
    private Transform player;
    public float speed;

    private float disToPlayer;

	private void Awake()
	{
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update()
	{
        transform.Translate(0, -speed*Time.deltaTime*CharacterThings.speedMultiplier, 0);

        disToPlayer = transform.position.y - player.transform.position.y;

        if (disToPlayer < -500)
		{
            CharacterThings.Instance.Tries--;
            Destroy(gameObject);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterThings.Instance.Score++;
            Destroy(gameObject);
        }
    }

}
