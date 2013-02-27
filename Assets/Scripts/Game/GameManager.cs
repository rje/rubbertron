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
	
	
	int m_enemyRoundSpawnCount = 0;
	int m_humanRoundSpawnCount = 0;
	float m_enemySinceSpawn = 0;
	float m_humanSinceSpawn = 0;
	
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
		UpdateSpawnTimers();
		SpawnIfNecessary();
	}
	
	void UpdateSpawnTimers() {
		m_enemySinceSpawn += Time.deltaTime;
		m_humanSinceSpawn += Time.deltaTime;
	}
	
	void SpawnIfNecessary() {
		if(ShouldSpawnEnemy()) {
			SpawnEnemy(1.0f);
			m_enemyRoundSpawnCount++;
			m_enemySinceSpawn -= m_info.m_mobSpawnRate;
		}
		if(ShouldSpawnHuman()) {
			SpawnHuman ();
			m_humanRoundSpawnCount++;
			m_humanSinceSpawn -= m_info.m_humanSpawnRate;
		}
	}
	
	bool ShouldSpawnEnemy() {
		return m_enemySinceSpawn >= m_info.m_mobSpawnRate && m_enemyRoundSpawnCount < m_info.m_roundEnemySpawn;
	}
	
	bool ShouldSpawnHuman() {
		return m_humanSinceSpawn >= m_info.m_humanSpawnRate && m_humanRoundSpawnCount < m_info.m_roundHumanSpawn;
	}
	
	void ClearRemnants() {
		foreach(var go in m_mobs) {
			Destroy (go);
		}
		m_mobs.Clear ();
	}
	
	bool WaveIsFinished() {
		if(m_enemyRoundSpawnCount < m_info.m_roundEnemySpawn || m_humanRoundSpawnCount < m_info.m_roundHumanSpawn) {
			return false;
		}
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
		m_enemyRoundSpawnCount = 0;
		m_enemySinceSpawn = 0;
		m_humanRoundSpawnCount = 0;
		m_humanSinceSpawn = 0;
		m_info = m_generator.GetInfoForLevel(m_wave);
		for(var i = 0; i < m_info.m_baseEnemySpawn; i++) {
			SpawnEnemy (2);
		}
		for(var i = 0; i < m_info.m_baseHumanSpawn; i++) {
			SpawnHuman ();
		}
	}
	
	void SpawnEnemy(float pauseDelay) {
		var type = m_info.GetTypeToSpawn();
		var go = InstantiateMobType(type);
		var mob = go.GetComponent<Mob>();
		mob.PauseFor(pauseDelay);
		go.transform.localPosition = m_info.GetRandomStartingPosition();
		m_mobs.Add (go);
	}
	
	void SpawnHuman() {
		var go = (GameObject)GameObject.Instantiate(m_humanPrefab);
		go.transform.localPosition = m_info.GetRandomStartingPosition();
		m_humans.Add (go);
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
