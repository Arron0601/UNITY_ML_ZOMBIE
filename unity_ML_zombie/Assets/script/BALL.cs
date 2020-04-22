
using UnityEngine;

public class BALL : MonoBehaviour
{
    public static bool complete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "感應區")
        {
            complete = true;
        }
    }
}
