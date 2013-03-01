using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	public GameManager m_game;
	// Use this to loop and set things active/inactive
	public GameObject[] m_children;
	
	public TextMesh m_finalScore;
	
	public void ShowWithScore(int score) {
		m_finalScore.text = string.Format ("Final Score: {0}", score);
		SetChildrenVisible(true);
	}
	
	public void Hide() {
		SetChildrenVisible(false);
	}
	
	void SetChildrenVisible(bool val) {
		foreach(var child in m_children) {
			child.SetActive(val);
		}
	}
	
	void RestartGame() {
		m_game.StartGame();
		Hide ();
	}
	
	void MainMenu() {
		Application.LoadLevel("main_menu");
	}
}
