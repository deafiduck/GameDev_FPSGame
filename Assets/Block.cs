using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	[SerializeField] int BlockHealth = 100;

	[SerializeField] GameObject GameObjectHide;
	[SerializeField] GameObject GameObjectShow;
	[SerializeField] List<GameObject> Zombies;
	[SerializeField] AudioClip interactionSound; // Ses dosyas�n� tan�mlay�n

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource bile�eni ekleyin
	}

	private void Update()
	{
		if (BlockHealth < 0)
		{
			BlockHealth = 0;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			PlayInteractionSound(); // Ses efektini �al
			GameObjectShow.SetActive(true);
			GameObjectHide.SetActive(false);
			if (Zombies != null && Zombies.Count > 0)
			{
				foreach (GameObject zombie in Zombies)
				{
					if (zombie != null)
					{
						zombie.SetActive(true); // Her bir zombiyi aktif hale getir
					}
				}
			}
		}
	}

	public void ReduceHealth(int reduceHealth)
	{
		BlockHealth -= reduceHealth;
		Debug.Log("Block Health Reduced: " + BlockHealth);

		if (BlockHealth <= 0)
		{
			BlockHealth = 0;
			gameObject.SetActive(false);
		}
	}

	private void PlayInteractionSound()
	{
		if (interactionSound != null)
		{
			audioSource.clip = interactionSound;
			audioSource.Play();
		}
	}
}