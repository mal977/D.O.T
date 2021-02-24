using UnityEngine;


[CreateAssetMenu(fileName = "New RecgoniseObjectData", menuName = "Recgonise Object Data", order = 51)]
public class RecgoniseObjects : ScriptableObject
{
    [SerializeField]
    private string ObjectName;
    [SerializeField]
    private string ObjectDescription;
    [SerializeField]
    private Sprite objectIcon;
    [SerializeField]
    private string option1;
    [SerializeField]
    private string option2;
    [SerializeField]
    private string option3;
    [SerializeField]
    private string option4;

    public string objectName
    {
        get
        {
            return objectName;
        }
    }

    public string objectDescription
    {
        get
        {
            return objectDescription;
        }
    }
    public Sprite Icon
    {
        get
        {
            return objectIcon;
        }
    }
    public string optionOne
    {
        get
        {
            return option1;
        }
    }
    public string optionTwo
    {
        get
        {
            return option2;
        }
    }
    public string optionThree
    {
        get
        {
            return option3;
        }
    }
    public string optionFour
    {
        get
        {
            return option4;
        }
    }
}
