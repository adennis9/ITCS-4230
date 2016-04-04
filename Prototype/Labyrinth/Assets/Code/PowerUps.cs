using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour 
{

    public float IncreaseJumpHeight = 8;
	public Projectile NewProjectile;
    public AudioClip CollectSound;


	public void OnTriggerEnter2D(Collider2D other)
	{
		var player = other.GetComponent<Player> ();
		var parameters = player.GetComponent<CharacterController2D> ();

		if (player == null)
			return;
		
		if (IncreaseJumpHeight != 0) 
		{
			parameters.PowerUpJump (IncreaseJumpHeight, gameObject);
            FloatingText.Show("High Jump Acquired!", "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 3, 15));

        }

        if (NewProjectile != null) 
		{
			player.changeProjectile (NewProjectile);
            FloatingText.Show("Missiles Acquired!", "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 3, 15));

        }

        if (CollectSound != null)
            AudioSource.PlayClipAtPoint(CollectSound, transform.position, 1000);

        gameObject.SetActive (false);
	}

}
