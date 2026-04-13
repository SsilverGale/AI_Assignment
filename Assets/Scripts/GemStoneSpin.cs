using UnityEngine;

public class GemStoneSpin : MonoBehaviour
{
    [SerializeField] int spinspeed;
    void Update()
    {
        transform.Rotate(0,0,spinspeed*Time.deltaTime);
    }
}
