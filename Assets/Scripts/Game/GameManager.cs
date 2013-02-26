using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public int m_lives;
	public int m_score;
	public int m_wave;
	public GameObject m_playerPrefab;
	public float m_spawnDelay;
	
	void Start() {
		StartCoroutine(SpawnPlayerAfterDelay(m_spawnDelay));
	}
	
	void SpawnNewPlayer() {
		m_lives--;
		GameObject.Instantiate(m_playerPrefab);
	}
	
	public void HandlePlayerDeath() {
		if(m_lives > 0) {
			StartCoroutine(SpawnPlayerAfterDelay(m_spawnDelay));
		}
		else {
			HandleGameOver();
		}
	}
	
	IEnumerator SpawnPlayerAfterDelay(float delay) {
		yield return new WaitForSeconds(delay);
		SpawnNewPlayer();
	}
	
	void HandleGameOver() {
	}
}
