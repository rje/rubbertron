using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelInfo {
	public int m_level;
	public System.Random m_rand;
	public int m_baseEnemySpawn;
	public int m_roundEnemySpawn;
	public int m_baseHumanSpawn;
	public int m_roundHumanSpawn;
	
	public float m_gruntChance;
	public float m_robotChance;
	public float m_tankChance;
	public float m_spawnerChance;
	public float m_exploderChance;
	
	public float m_mobSpawnRate;
	public float m_humanSpawnRate;
	
	public MobType GetTypeToSpawn() {
		var next = m_rand.NextDouble();
		var accum = m_gruntChance;
		if(next <= accum) {
			return MobType.Grunt;
		}
		accum += m_robotChance;
		if(next <= accum) {
			return MobType.Robot;
		}
		accum += m_spawnerChance;
		if(next <= accum) {
			return MobType.Spawner;
		}
		accum += m_exploderChance;
		if(next <= accum) {
			return MobType.Exploder;
		}
		accum += m_tankChance;
		if(next <= accum) {
			return MobType.Tank;
		}
		
		return MobType.Grunt;
	}	
	
	public Vector3 GetRandomStartingPosition() {
		var x = -11.0 + 10.0 * m_rand.NextDouble();
		if(m_rand.Next() % 2 == 0) {
			x = -x;
		}
		var y = -8.0 + 16.0 * m_rand.NextDouble();
		return new Vector3((float)x, (float)y, 0);
	}
}