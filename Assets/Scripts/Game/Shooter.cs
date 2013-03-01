using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	
	public GameObject m_bulletPrefab;
	public float m_fireRate;
	public Vector3 m_slop;
	
	public AudioSource m_source;
	public AudioClip m_sound;
	
	bool m_isFiring = false;
	float m_sinceLastFire = 0;
	Vector3 m_target;
	
	public void StartFiring() {
		m_isFiring = true;
	}
	
	public void StopFiring() {
		m_isFiring = false;
	}
	
	public void SetTarget(Vector3 target) {
		m_target = target;
	}
	
	void Update() {
		if(m_isFiring) {
			UpdateFiring();
		}
	}
	
	void UpdateFiring() {
		m_sinceLastFire += Time.deltaTime;
		
		if(m_sinceLastFire >= m_fireRate) {
			FireBullet();
			m_sinceLastFire -= m_fireRate;
		}
	}
	
	void FireBullet() {
		var bulletGO = (GameObject)GameObject.Instantiate(m_bulletPrefab);
		bulletGO.transform.position = transform.position;
		var bullet = bulletGO.GetComponent<Bullet>();
		var dir = (m_target + GetSlop()) - transform.position;
		dir.z = 0;
		dir.Normalize();
		bullet.m_direction = dir;
		if(m_source != null && m_sound != null) {
			m_source.PlayOneShot(m_sound);
		}
	}
	
	Vector3 GetSlop() {
		return new Vector3(
			Random.Range (-m_slop.x, m_slop.x),
			Random.Range (-m_slop.y, m_slop.y),
			0.0f);
	}
}
