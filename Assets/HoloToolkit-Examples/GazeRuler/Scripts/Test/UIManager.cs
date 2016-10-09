using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    [SerializeField]
    GameObject popup;
    static UIManager instance;
    List<GameObject> UIs = new List<GameObject>();

    public static UIManager Instance{
        get{ return instance; } }

	// Use this for initialization
	void Start () {
        instance = this;
        //initiateUI("good name", "asdfasfewfs awefsdfsda aefasdfs adafds dasfasdfa dasfsadfas afeafeaf", new Vector3(0, 1, 10));
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.D))
          //  removeUI();
	}

    public void initiateUI(string name, string detail, Vector3 position)
    {
        Vector3 tempos = position;
        tempos.y += 0.2f;
        GameObject temp = (GameObject)GameObject.Instantiate(popup, tempos, Quaternion.identity);
        UIs.Add(temp);
        temp.GetComponent<PopUpUI>().Init(name,detail);
    }

    public void removeUI()
    {
        for (int i = UIs.Count - 1; i >= 0; --i)
        {
            Destroy(UIs[i]);
        }
        UIs.Clear();
    }
}
