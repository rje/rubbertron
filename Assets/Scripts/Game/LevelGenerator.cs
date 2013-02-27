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
		
		toReturn.m_baseEnemySpawn = GetEnemyBaseSpawn(wave);
		toReturn.m_roundEnemySpawn = GetEnemyRoundSpawn(wave);
		toReturn.m_baseHumanSpawn = GetHumanBaseSpawn(wave);
		toReturn.m_roundHumanSpawn = GetHumanRoundSpawn(wave);
		
		toReturn.m_gruntChance = GetGruntOdds(wave);
		toReturn.m_robotChance = GetRobotOdds(wave);
		toReturn.m_tankChance = GetTankOdds(wave);
		toReturn.m_spawnerChance = GetSpawnerOdds(wave);
		toReturn.m_exploderChance = GetExploderOdds(wave);
		
		JustifyOdds(toReturn);
		
		toReturn.m_mobSpawnRate = toReturn.m_roundEnemySpawn == 0 ? float.MaxValue : 10.0f / toReturn.m_roundEnemySpawn;
		toReturn.m_humanSpawnRate = toReturn.m_roundHumanSpawn == 0 ? float.MaxValue : 10.0f / toReturn.m_roundHumanSpawn;
		
		return toReturn;
	}
	
	void JustifyOdds(LevelInfo info) {
		var accum = 0.0f;
		accum += info.m_gruntChance;
		accum += info.m_robotChance;
		accum += info.m_tankChance;
		accum += info.m_spawnerChance;
		accum += info.m_exploderChance;
		
		var toDistribute = 1.0f - accum;
		
		info.m_gruntChance += toDistribute / 2.0f;
		info.m_robotChance += toDistribute / 2.0f;
	}
	
	public int GetEnemyBaseSpawn(int wave) {
		return 4 + 2 * (wave / 5);
	}
	
	public int GetEnemyRoundSpawn(int wave) {
		return (wave % 5) * 2 * (1 + (wave / 10));
	}
	
	public int GetHumanBaseSpawn(int wave) {
		if(wave > 0 && wave % 5 == 0) {
			return 15;
		}
		return 10;
	}
	
	public int GetHumanRoundSpawn(int wave) {
		return 2 * (wave % 5);
	}
	
	public float GetGruntOdds(int wave) {
		return Mathf.Max (0.3f, 1.0f - 0.02f * wave) * (1.0f - GetTankOdds(wave));
	}
	
	public float GetRobotOdds(int wave) {
		return Mathf.Min (0.3f, 0.02f * wave) * (1.0f - GetTankOdds(wave));
	}
	
	public float GetTankOdds(int wave) {
		return (wave > 0) && (wave % 5 == 0) ? 0.5f : 0.0f;
	}
	
	public float GetSpawnerOdds(int wave) {
		if(wave < 15) {
			return 0;
		}
		return Mathf.Min (0.2f, (wave - 15) * 0.01f) * (1.0f - GetTankOdds(wave));
	}
	
	public float GetExploderOdds(int wave) {
		if(wave < 35) {
			return 0;
		}
		return Mathf.Min (0.2f, (wave - 35) * 0.01f) * (1.0f - GetTankOdds(wave));
	}
}
