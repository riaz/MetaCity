using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour {

    //public RectTransform test;
    string text = "";
    Text child;
    float floatGap = 0.004f, floatRate = 0.001f;
    

    Vector3 pos, rota;
	// Use this for initialization
	void Start () {
        pos = transform.position;
        rota = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        //test.position = transform.position;

        transform.LookAt(Camera.main.transform);
        Vector3 temprot = transform.eulerAngles;
        temprot.y -= 180;
        transform.eulerAngles = new Vector3(rota.x, temprot.y, rota.z);
        if (text.Length > 0)
        {
            print(child.text.Length);
            child.text = text.Substring(0, (child.text.Length + 1));
            if (child.text == text) text = "";
        }
        Vector3 temp = GetComponent<RectTransform>().position;
        temp.y += floatRate;
        floatGap -= 0.001f;
        if(floatGap <= 0)
        {
            floatGap = 0.004f;
            floatRate *= -1;
        }
        GetComponent<RectTransform>().position = temp;


    }

    public void Init(string name, string detail)
    {
        child = transform.GetChild(0).FindChild("Info").GetComponent<Text>();
        transform.GetChild(0).FindChild("Name").GetComponent<Text>().text = name;
        text = detail;
    }
}
