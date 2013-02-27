using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public int m_lives;
	public int m_score;
	public int m_wave;
	public float m_spawnDelay;
	
	public GameObject m_playerPrefab;
	public GameObject m_gruntPrefab;
	public GameObject m_robotPrefab;
	public GameObject m_tankPrefab;
	public GameObject m_humanPrefab;
	
	List<GameObject> m_mobs;
	List<GameObject> m_humans;
	LevelInfo m_info;
	
	public LevelGenerator m_generator;
	
	void Start() {
		SpawnNewWave();
		StartCoroutine(SpawnPlayerAfterDelay(m_spawnDelay));
	}
	
	void Update() {
		if(WaveIsFinished()) {
			ClearRemnants();
			m_wave++;
			SpawnNewWave();
		}
	}
	
	void ClearRemnants() {
		foreach(var go in m_mobs) {
			Destroy (go);
		}
		m_mobs.Clear ();
	}
	
	bool WaveIsFinished() {
		foreach(var go in m_mobs) {
			var mob = go.GetComponent<Mob>();
			if(mob.m_type != MobType.Tank) {
				return false;
			}
		}
		return m_humans.Count == 0;
	}
	
	void SpawnNewPlayer() {
		m_lives--;
		foreach(var go in m_mobs) {
			var mob = go.GetComponent<Mob>();
			mob.PauseFor(1);
		}
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
	
	void SpawnNewWave() {
		m_mobs = new List<GameObject>();
		m_humans = new List<GameObject>();
		m_info = m_generator.GetInfoForLevel(m_wave);
		for(var i = 0; i < m_info.m_baseEnemySpawn; i++) {
			var type = m_info.GetTypeToSpawn();
			var go = InstantiateMobType(type);
			var mob = go.GetComponent<Mob>();
			mob.PauseFor(2);
			go.transform.localPosition = m_info.GetRandomStartingPosition();
			m_mobs.Add (go);
		}
		for(var i = 0; i < m_info.m_baseHumanSpawn; i++) {
			var go = (GameObject)GameObject.Instantiate(m_humanPrefab);
			go.transform.localPosition = m_info.GetRandomStartingPosition();
			m_humans.Add (go);
		}
	}
	
	GameObject InstantiateMobType(MobType type) {
		switch(type) {
		case MobType.Robot:
			return (GameObject)GameObject.Instantiate(m_robotPrefab);
		case MobType.Tank:
			return (GameObject)GameObject.Instantiate(m_tankPrefab);
		default:
		case MobType.Grunt:
			return (GameObject)GameObject.Instantiate(m_gruntPrefab);
		}
	}
	
	IEnumerator SpawnPlayerAfterDelay(float delay) {
		yield return new WaitForSeconds(delay);
		SpawnNewPlayer();
	}
	
	void HandleGameOver() {
	}
	
	public void CollectHuman(Human toCollect) {
		m_humans.Remove(toCollect.gameObject);
		m_score += 100;
	}
	
	public void DestroyHuman(Human toDestroy) {
		m_humans.Remove (toDestroy.gameObject);
	}
	
	public void RemoveMob(GameObject toDestroy) {
		var mob = toDestroy.GetComponent<Mob>();
		AddScoreForMobType(mob.m_type);
		m_mobs.Remove (toDestroy);
	}
	
	void AddScoreForMobType(MobType type) {
		switch(type) {
		case MobType.Grunt:
			m_score += 50;
			break;
		case MobType.Robot:
			m_score += 125;
			break;
		}
	}
}
