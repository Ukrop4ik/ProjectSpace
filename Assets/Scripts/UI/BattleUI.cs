using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUI : MonoBehaviour {

	// Use this for initialization
	void Start () {

        this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;

	}

    public void ReturnButton()
    {
        SceneManager.LoadScene(ContextManagerGamePro.Instance().PreviousScene);
    }
}
