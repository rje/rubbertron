using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {
	
	public int m_seed;
	
	System.Random m_rand;
	
	void Awake() {
		m_rand = new System.Random(m_seed);
	}
	
	public LevelInfo GetInfoForLevel(int wave) {
		var toReturn = new LevelInfo();
		toReturn.m_rand = m_rand;
		toReturn.m_level = wave;
		toReturn.m_baseEnemySpawn = 4 + 2 * (wave / 5) + 2 * (wave % 5);
		if(wave % 5 != 0) {
			toReturn.m_roundEnemySpawn = 2 * (wave / 5) + 2 * (wave % 5);
		}
		else {
			toReturn.m_roundEnemySpawn = 0;
		}
		toReturn.m_baseHumanSpawn = 10;
		toReturn.m_roundHumanSpawn = 0;
		
		if(wave == 0 || wave % 5 != 0) {
			toReturn.m_gruntChance = 1.0f - (0.02f * wave);
			toReturn.m_robotChance = 0.02f * wave;
		}
		else {
			toReturn.m_gruntChance = (1.0f - (0.02f * wave)) / 2.0f;
			toReturn.m_robotChance = (0.02f * wave) / 2.0f;
			toReturn.m_tankChance = 0.5f;
		}
		
		toReturn.m_mobSpawnRate = 0.33f;
		toReturn.m_humanSpawnRate = float.MaxValue;
		
		return toReturn;
	}
}
