using UnityEngine;
using UnityEngine.iOS;

public abstract class BaseAdapter : MonoBehaviour
{
    protected bool PhoneDeviceCheck()
    {
        if (Device.generation.ToString().StartsWith("iPhone"))
        {
            return true;
        }

        return false;
    }
}