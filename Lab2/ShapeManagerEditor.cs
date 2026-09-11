using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
using JetBrains.Annotations;
using Codice.Client.Common.GameUI;

[CustomEditor(typeof(ShapeManager)), CanEditMultipleObjects]
public class ShapeManagerEditor : Editor
{
    //create the values for the sizes
    SerializedProperty cubeSize;
    SerializedProperty sphereSize;

    private void OnEnable()
    {
        cubeSize = serializedObject.FindProperty("cubeSize");
        sphereSize = serializedObject.FindProperty("sphereSize");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        base.OnInspectorGUI();
        //variable for color
        var cachedColor = GUI.backgroundColor;

        using (new EditorGUILayout.HorizontalScope())
        {
            //create button to select all cubes
            if (GUILayout.Button("Select all cubes"))
            {
                //select all gameobjects with the shapemanager script
                var allShapeManager = GameObject.FindObjectsByType<ShapeManager>();
                //check for null and select all shapemanager objects with Cube in their name
                if (allShapeManager != null)
                {
                    var allCubes = allShapeManager
                        .Select(shapes => shapes.gameObject)
                        .Where(cube => cube.name.Contains("Cube"))
                        .ToArray();
                    //select the cubes
                    Selection.objects = allCubes;
                }
            }
            //create button to select all spheres
            if (GUILayout.Button("Select all spheres"))
            {
                //select all gameobjects with the shapemanager script
                var allShapeManager = GameObject.FindObjectsByType<ShapeManager>();
                //check for null and select all shapemanager objects with Sphere in their name
                if(allShapeManager != null)
                {
                    var allSpheres = allShapeManager
                        .Select(shapes => shapes.gameObject)
                        .Where(sphere => sphere.name.Contains("Sphere"))
                        .ToArray();
                    //select the spheres
                    Selection.objects = allSpheres;
                }
            }
        }

        if(GUILayout.Button("Deselect all objects"))
        {
            //deselect all objects
            Selection.objects = null;
        }

        using (new EditorGUILayout.HorizontalScope())
        {
            //create color boolean
            bool isActiveCube = true;
            bool isActiveSphere = true;

            //select all gameobjects with the shapemanager script
            var allShapeManager = GameObject.FindObjectsByType<ShapeManager>(FindObjectsInactive.Include);
            //check for null and select all shapemanager objects with Cube in their name
            if (allShapeManager != null)
            {
                var allCubes = allShapeManager
                    .Select(shapes => shapes.gameObject)
                    .Where(cube => cube.name.Contains("Cube"))
                    .ToArray();
                if (allCubes[0].activeSelf == true)
                {
                    isActiveCube = true;
                }
                else
                {
                    isActiveCube = false;
                }
            }

            //set color based on active or inactive
            if (isActiveCube)
            {
                GUI.backgroundColor = Color.green;
            }
            else
            {
                GUI.backgroundColor = Color.red;
            }

            //Create button to make cubes inactive or active
            if (GUILayout.Button("Disable/Enable all Cubes",GUILayout.Height(40)))
            {
                if (allShapeManager != null)
                {
                    var allCubes = allShapeManager
                        .Select(shapes => shapes.gameObject)
                        .Where(cube => cube.name.Contains("Cube"))
                        .ToArray();
                    //cycle through each cube in the array
                    foreach(GameObject cube in allCubes)
                    {
                        //check if it is already active or not and set to opposite
                        if(cube.activeSelf == false)
                        {
                            cube.SetActive(true);
                        }
                        else
                        {
                            cube.SetActive(false);
                        }
                    }
                }
            }
            //check if the spheres are active
            if (allShapeManager != null)
            {
                var allSpheres = allShapeManager
                    .Select(shapes => shapes.gameObject)
                    .Where(sphere => sphere.name.Contains("Sphere"))
                    .ToArray();
                if (allSpheres[0].activeSelf == true)
                {
                    isActiveSphere = true;
                }
                else
                {
                    isActiveSphere = false;
                }
            }

            //set color based on active/inactive
            if (isActiveSphere)
            {
                GUI.backgroundColor = Color.green;
            }
            else
            {
                GUI.backgroundColor = Color.red;
            }

            //Create button to make spheres active or inactive
            if (GUILayout.Button("Disable/Enable all Spheres", GUILayout.Height(40)))
            {
                //check for null and select all shapemanager objects with Sphere in their name
                if (allShapeManager != null)
                {
                    var allSpheres = allShapeManager
                        .Select(shapes => shapes.gameObject)
                        .Where(sphere => sphere.name.Contains("Sphere"))
                        .ToArray();
                    //cycle through each cube in the array
                    foreach (GameObject sphere in allSpheres)
                    {
                        //check if it is already active or not and set to opposite
                        if (sphere.activeSelf == false)
                        {
                            sphere.SetActive(true);
                        }
                        else
                        {
                            sphere.SetActive(false);
                        }
                    }
                }
            }
            GUI.backgroundColor = cachedColor;
        }
        //create the slider for the cubesize and display a warning if the size is > 2
        cubeSize.intValue = EditorGUILayout.IntSlider("Cube Size",cubeSize.intValue, 1,3);
        serializedObject.ApplyModifiedProperties();
        if(cubeSize.intValue > 2)
        {
            EditorGUILayout.HelpBox("Cube size cannot be greater than 2!", MessageType.Warning);
        }

        //create the slider for the spheresize and display a warning if the size is > 2
        sphereSize.intValue = EditorGUILayout.IntSlider("Sphere Size", sphereSize.intValue, 0, 3);
        serializedObject.ApplyModifiedProperties();
        if (sphereSize.intValue < 1)
        {
            EditorGUILayout.HelpBox("Sphere size cannot be less than 1!", MessageType.Warning);
        }
    }
}
