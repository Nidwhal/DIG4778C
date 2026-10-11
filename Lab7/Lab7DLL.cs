using UnityEngine;
using System.Runtime.InteropServices;

public class Lab7DLL : MonoBehaviour
{
    // The imported function
    [DllImport("Lab7DLL", EntryPoint = "TestSort")]
    public static extern void TestSort(int[] a, int length);

    public int[] a;

    void Start()
    {
        TestSort(a, a.Length);
    }
}