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
    private string option0;
    [SerializeField]
    private string option1;
    [SerializeField]
    private string option2;
    [SerializeField]
    private string option3;
    [SerializeField]
    private int correctOption;
    [SerializeField]
    private string sourceURL;
    

    public string ObjectName1 { get => ObjectName; set => ObjectName = value; }
    public string ObjectDescription1 { get => ObjectDescription; set => ObjectDescription = value; }
    public Sprite ObjectIcon { get => objectIcon; set => objectIcon = value; }
    public string Option0 { get => option0; set => option0 = value; }
    public string Option1 { get => option1; set => option1 = value; }
    public string Option2 { get => option2; set => option2 = value; }
    public string Option3 { get => option3; set => option3 = value; }
    public int CorrectOption { get => correctOption; set => correctOption = value; }
}
