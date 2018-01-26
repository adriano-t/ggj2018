using UnityEditor;
using System.Collections;
using UnityEngine;
using System.IO;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

[InitializeOnLoad]
public class EditorTools
{

    private const string name = "Custom";


    [MenuItem (name + "/group selected")]
    private static void GroupSelected ()
    {
        GameObject[] objects = Selection.gameObjects;
        Transform parent = new GameObject ().transform;

        Vector3 mean = Vector3.zero;
        foreach (GameObject g in objects)
            mean += g.transform.position;
        mean /= objects.Length;

        parent.position = mean;
        parent.name = "group";
        foreach (GameObject g in objects)
            g.transform.parent = parent;
    }

    private static int lastIndex = 0;

    private static StringBuilder sVerts = new StringBuilder ();
    private static StringBuilder sUvs = new StringBuilder ();
    private static StringBuilder sNorms = new StringBuilder ();
    private static StringBuilder sTris = new StringBuilder ();
    private static bool exportSingle;

    [MenuItem (name + "/export scene (.obj)")]
    private static void ExportScene ()
    {
        exportSingle = false;
        lastIndex = 0;
        List<GameObject> rootObjects = new List<GameObject> ();
        foreach (Transform t in UnityEngine.Object.FindObjectsOfType<Transform> ())
            if (t.parent == null)
                rootObjects.Add (t.gameObject);

        string filename = Application.dataPath + "/ExportedModels/mod_" + SceneManager.GetActiveScene ().name.ToLower () + ".obj";


        sVerts.Length = 0;
        sUvs.Length = 0;
        sNorms.Length = 0;
        sTris.Length = 0;

        Debug.Log ("Exporting " + rootObjects.Count + " objects to " + filename);

        foreach (GameObject obj in rootObjects)
            if (obj.activeInHierarchy)
                ExportRecursive (obj);

        using (StreamWriter sw = new StreamWriter (filename))
        {
            sw.Write (sVerts.ToString ());
            sw.Write (sUvs.ToString ());
            sw.Write (sNorms.ToString ());
            sw.Write (sTris.ToString ());
        }
    }


    [MenuItem (name + "/export selected (.obj)")]
    private static void ExportSelected ()
    {
        exportSingle = true;
        lastIndex = 0;

        GameObject[] objects = Selection.gameObjects;
        if (objects.Length == 0)
            return;

        string filename = Application.dataPath + "/ExportedModels/mod_" + objects[0].name + ".obj";


        sVerts.Length = 0;
        sUvs.Length = 0;
        sNorms.Length = 0;
        sTris.Length = 0;

        Debug.Log ("Exporting " + objects.Length + " objects to " + filename);

        foreach (GameObject obj in objects)
            if (obj.activeInHierarchy)
                ExportRecursive (obj);

        using (StreamWriter sw = new StreamWriter (filename))
        {
            sw.Write (sVerts.ToString ());
            sw.Write (sUvs.ToString ());
            sw.Write (sNorms.ToString ());
            sw.Write (sTris.ToString ());
        }
    }

    private static void ExportRecursive (GameObject obj)
    {
        WriteObjString (obj);

        foreach (Transform c in obj.transform)
            if (c.gameObject.activeInHierarchy)
                ExportRecursive (c.gameObject);
    }

    private static void WriteObjString (GameObject obj)
    {
        MeshFilter mf = obj.GetComponent<MeshFilter> ();
        if (mf == null)
            return;

        Mesh mesh = mf.sharedMesh;
        if (mesh == null)
            return;

        Debug.Log ("exporting " + obj.name);

        Vector3 pos = obj.transform.position;

        //mesh.vertices
        Vector3[] vertices = mesh.vertices;
        if (!exportSingle)
        {
            foreach (Vector3 vert in vertices)
            {
                Vector3 v = obj.transform.rotation * vert;
                sVerts.Append ("v " + (pos.x + v.x) + " " + (pos.y + v.y) + " " + (pos.z + v.z) + "\n");
            }
        }
        else
        {
            foreach (Vector3 vert in vertices)
                sVerts.Append ("v " + (vert.x) + " " + (vert.y) + " " + (vert.z) + "\n");
        }
        sVerts.Append ("\n");

        Vector3[] normals = mesh.normals;
        foreach (Vector3 v in normals)
            sNorms.Append ("vn " + v.x + " " + v.y + " " + v.z + "\n");
        sNorms.Append ("\n");

        Vector2[] uv = mesh.uv;
        foreach (Vector2 v in normals)
            sUvs.Append ("vt " + v.x + " " + v.y + "\n");
        sUvs.Append ("\n");


        for (int material = 0; material < mesh.subMeshCount; material++)
        {
            int[] triangles = mesh.GetTriangles (material);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                sTris.Append (string.Format ("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                    lastIndex + triangles[i] + 1, lastIndex + triangles[i + 1] + 1, lastIndex + triangles[i + 2] + 1));

            }
        }
        sTris.Append ("\n");

        lastIndex += vertices.Length;
    }

    /*
     * ArrayList layerNames=new ArrayList();
 for(int i=8;i<=31;i++) //user defined layers start with layer 8 and unity supports 31 layers
 {
   var layerN=LayerMask.LayerToName(i); //get the name of the layer
   if(layerN.Length>0) //only add the layer if it has been named (comment this line out if you want every layer)
     layerNames.Add(layer)
 }*/

    [MenuItem (name + "/Generate Layers Script")]
    private static void GenerateLayers ()
    {
        Debug.Log ("generating");
        string filename = Application.dataPath + "/Scripts/Core/Layers.cs";

        StringBuilder sb = new StringBuilder ();
        sb.AppendLine ("using UnityEngine;");
        sb.AppendLine ("public class Layers");
        sb.AppendLine ("{");

        
        for (int i = 0; i <= 31; i++)
        {
            string name = LayerMask.LayerToName (i);
            if(name.Length > 0)
            { 
                name = name.Trim ();
                name = name.ToUpper ();
                name = Regex.Replace (name, @"\s+", "_");  
                sb.AppendLine ("    public static int LAYER_" + name + " = " + i + ";");

            }
        }
         
        for (int i = 0; i <= 31; i++)
        {
            string name = LayerMask.LayerToName (i);
            if (name.Length > 0)
            {
                name = name.ToUpper ();
                name = name.Trim ();
                name = Regex.Replace (name, @"\s+", "_"); 
                sb.AppendLine ("    public static int MASK_" + name + " = 1 << " + i + ";");

            }
        }
        
        sb.AppendLine ("");
        sb.AppendLine ("    public static int Negate (int layer)");
        sb.AppendLine ("    {");
        sb.AppendLine ("        return ~layer;");
        sb.AppendLine ("    }");

        sb.AppendLine ("}");

        using (StreamWriter sw = new StreamWriter (filename))
        {
            sw.Write (sb.ToString ());
        }

        Debug.Log ("DONE!");
    }
}


 