using UnityEngine;

public class Utility
{

    //Parse Test Object to Json Format
    //TODO: Change to TestData object
    public static string JsonParser(TestDataPlaceHolder obj)
    {
        return JsonUtility.ToJson(obj);
    }
}
