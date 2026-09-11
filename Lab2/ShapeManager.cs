using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShapeManager : MonoBehaviour
{
    //create variables for size
    public int cubeSize = 1;
    public int sphereSize = 1;

    private void OnValidate()
    {
        //set size based on value
        if (this.name.Contains("Cube"))
        {
            //check for max size
            if (!(cubeSize > 2))
            {
                transform.localScale = Vector3.one * cubeSize;
            }
        }
        else if (this.name.Contains("Sphere"))
        {
            //check for low size
            if(!(sphereSize < 1))
            {
                transform.localScale = Vector3.one * sphereSize;
            }
        }
    }
}
