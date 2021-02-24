using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecgoniseGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<RecgoniseObjects> recgoniseObjectList;
    public Sprite sprite;
    public GameObject spriteObject;
    public GameObject btn1Object;
    public GameObject btn2Object;
    public GameObject btn3Object;
    public GameObject btn4Object;
    void Start()
    {
        Image spriteRender = spriteObject.GetComponent<Image>();
        spriteRender.sprite = recgoniseObjectList[0].Icon;

        btn1Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjectList[0].optionOne;
        btn2Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjectList[0].optionTwo;
        btn3Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjectList[0].optionThree;
        btn4Object.transform.GetChild(0).GetComponent<Text>().text = recgoniseObjectList[0].optionFour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
