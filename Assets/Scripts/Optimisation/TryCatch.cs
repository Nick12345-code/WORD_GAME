using UnityEngine;

public class TryCatch : MonoBehaviour
{
    public GameObject test;

    void Update()
    {
        ArrayErrorTest();
        NullExceptionTest();
    }

    void ArrayErrorTest()
    {
        try
        {
            string[] strings = new string[2];
            string s = strings[3];
        }
        catch 
        {
            Debug.LogError("That index doesn't exist");         
        }
    }

    void NullExceptionTest()
    {
        try
        {
            test.transform.position = test.transform.position;
        }
        catch 
        {
            Debug.LogError("That doesn't exist lolz");           
        }
    }
}
