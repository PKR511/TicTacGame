
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class SceneObjectList : MonoBehaviour
{
      
    public List<Object> AssetsObject;
    public List<Object> SceneObject ;
    /// <summary> The result </summary>
    public  List<Component> ReferencingSelection = new List<Component>();
    /// <summary> allComponents in the scene that will be searched to see if they contain the reference </summary>
    private  Component[] allComponents;
    public bool getref;
    void Start()
    {
        AssetsObject = new List<Object>();
        SceneObject = new List<Object>();
        GetAllAssets();
        FindAllInScene();
        
    }



    private void Update()
    {
        if (getref == true)
        {
            allComponents = GetAllActiveInScene();
           
            getref = false;
        }
    }



    /// <summary>
    /// Finds all object present in scene
    /// </summary>
    private void FindAllInScene()
    {
        Object[] tempList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
       
        GameObject temp;

        foreach (Object obj in tempList)
        {
            if (obj is GameObject)
            {
                temp = (GameObject)obj;
                if (temp.hideFlags == HideFlags.None)
                {
                    SceneObject.Add((GameObject)obj);
                    Debug.Log("Name-:- " + obj.name);
                }
                    
            }
        }
       
    }
    
    /// <summary>
    /// Finds all assets present under assets folder.
    /// </summary>
    private void GetAllAssets()
    {
       
        string[] dir = Directory.GetDirectories(Application.dataPath);
       
        foreach (string d in dir)
        {
            GetAssetsAtPath(d);
        }
       
           
    }

    /// <summary>
    /// Finds all assets present under a specific directory
    /// </summary>
    /// <param name="path"></param>
    private void GetAssetsAtPath(string path)
    {
       
        string relativePath;
        //checking if given path is another dir
        //if true then check inside first.
        string[] dir = Directory.GetDirectories(path);
        if (dir.Length >= 0)
        {
            Debug.Log("Dir Check Valid " + dir);
            foreach (string d in dir)
            {
                GetAssetsAtPath(d);
            }
        }
        //if path is for file then load it
        string[] fileEntries = Directory.GetFiles(path);
        foreach (string fileName in fileEntries)
        {
            if (fileName.StartsWith(Application.dataPath))
            {
                relativePath = "Assets" + path.Substring(Application.dataPath.Length);
                int index = fileName.LastIndexOf("\\");
                relativePath += fileName.Substring(index);
                relativePath = relativePath.Replace("\\", "/");
            }
            else
            {
                relativePath = path;
            }
                      
            if (relativePath.Substring(relativePath.LastIndexOf(".")) != ".unity")
            {           
                foreach (var obj in AssetDatabase.LoadAllAssetsAtPath(relativePath))
                {
                    AssetsObject.Add(obj);
                    //Debug.Log(obj);
                    break;
                }
            }


        }

    }

    public string MenuShowIds(GameObject tempObject)
    {
        var stringBuilder = new StringBuilder();
        int count = 0;
        string result = null;
        foreach (var obj in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(tempObject)))
        {
            string guid;
            long file;
            count++;
            Debug.Log(obj + "/" + count);
            if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out guid, out file))
            {
                stringBuilder.AppendFormat("Asset: " + obj.name +
                    "\n  Instance ID: " + obj.GetInstanceID() +
                    "\n  GUID: " + guid +
                    "\n  File ID: " + file);
               
                result = guid;
                break;
            }
        }

        Debug.Log(stringBuilder.ToString() + "/" + count);
        return result;
    }

    private  Component[] GetAllActiveInScene()
    {
        // Use new version of Resources.FindObjectsOfTypeAll(typeof(Component)) as per https://forum.unity.com/threads/editorscript-how-to-get-all-gameobjects-in-scene.224524/
        var rootObjects = UnityEngine.SceneManagement.SceneManager
            .GetActiveScene()
            .GetRootGameObjects();
        List<Component> result = new List<Component>();
        foreach (var rootObject in rootObjects)
        {
            result.AddRange(rootObject.GetComponentsInChildren<Component>());
        }
        return result.ToArray();
    }
    private  void componentReferences(Component component, Component references)
    {
        SerializedObject serObj = new SerializedObject(component);
        SerializedProperty prop = serObj.GetIterator();
        while (prop.NextVisible(true))
        {
            bool isObjectField = prop.propertyType == SerializedPropertyType.ObjectReference && prop.objectReferenceValue != null;
            if (isObjectField && prop.objectReferenceValue == references)
            {
                ReferencingSelection.Add(component);
            }
        }
    }
}//class


