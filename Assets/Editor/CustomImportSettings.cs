using UnityEngine;
using UnityEditor;
using System;
using System.IO;

//Sets our settings for all new Models and Textures upon first import
public class CustomImportSettings : AssetPostprocessor {
    //public const float importScale = 1.0f;
     
    void OnPreprocessModel()
    {
        ModelImporter importer = assetImporter as ModelImporter; 
        String name = importer.assetPath.ToLower();

        importer.importMaterials = false;

        //if the file already exist return
        if (File.Exists(AssetDatabase.GetTextMetaFilePathFromAssetPath(name)))
            return;

        importer.importAnimation = false;
        importer.animationType = ModelImporterAnimationType.None;
    }
     

    static string[] modelsExtensions = new String[] { ".obj", ".fbx" , ".3ds"};
    static string[] soundsExtensions = new String[] { ".ogg", ".mo3", ".wav", ".flac"};
    static string modPrefix = "mod_";
    static string sndPrefix = "snd_";
    static string matPrefix = "mat_";
    static string texPrefix = "tex_";
    static string normPrefix = "norm_";


    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {

        foreach (string path in importedAssets)
        {
            string extension = Path.GetExtension (path).ToLower();
            
            //Prefabs
            if (extension == ".prefab")
            {
                string name = Path.GetFileNameWithoutExtension (path);
                name = ChangePrefix (name, "mod_", "");
                string newName = ToUnderScore (name);
                
                if (name != newName)
                {
                    AssetDatabase.RenameAsset (path, newName);
                }
            }

            //Materials
            else if (extension == ".mat")
            {
                string name = Path.GetFileNameWithoutExtension (path);
                string newName = ToUnderScore (name); 
                newName = ChangePrefix (newName, texPrefix, matPrefix);
                if (!newName.StartsWith (matPrefix))
                    newName = matPrefix + newName;
                if (name != newName)
                    AssetDatabase.RenameAsset (path, newName);
            }

            //Models
            else if (Array.IndexOf (modelsExtensions, extension) >= 0)
            {
                string name = Path.GetFileNameWithoutExtension (path);
                string newName = ToUnderScore (name);
                if (!newName.StartsWith (modPrefix))
                    newName = modPrefix + newName;
                if (name != newName)
                    AssetDatabase.RenameAsset (path, newName);
            }
             

            //Sounds
            else if (Array.IndexOf (soundsExtensions, extension) >= 0)
            {
                string name = Path.GetFileNameWithoutExtension (path);
                string newName = ToUnderScore (name);
                if (!newName.StartsWith (sndPrefix))
                    newName = sndPrefix + newName;
                if (name != newName)
                    AssetDatabase.RenameAsset (path, newName);
            }

        }
         
    }


    void OnPostprocessTexture (Texture2D texture)
    {
        string name = Path.GetFileNameWithoutExtension (assetPath); 
        string newName = ToUnderScore (name);
        TextureImporter importer = assetImporter as TextureImporter;
         
        //if it's normalmap
        if (importer.textureType == TextureImporterType.NormalMap)
            newName = ChangePrefix (newName, texPrefix, normPrefix);
        //if its texture
        else
            newName = ChangePrefix (newName, normPrefix, texPrefix);

        if (name != newName)
            AssetDatabase.RenameAsset (assetPath, newName); 
    }

    public static string ChangePrefix(string name, string prefixFrom, string prefixTo)
    {
        if (name.StartsWith (prefixTo))
            return name;
        if (name.StartsWith (prefixFrom))
            name = name.Substring (prefixFrom.Length);
        if (name.Length == 0)
            name = "new";
        return prefixTo + name;
    }

    public static string ToUnderScore(string input)
    {
        input = input.Replace (" ", string.Empty);
        return System.Text.RegularExpressions.Regex.Replace (input, "(?<=.)([A-Z])", "_$0",
            System.Text.RegularExpressions.RegexOptions.Compiled).ToLower();
    }
    
    public static void TestRenameAsset(string path, string newName)
    { 
        string ext = Path.GetExtension (path);
        string name = Path.GetFileNameWithoutExtension (path);
        string dir = Application.dataPath + "/" + Path.GetDirectoryName (path).Substring("Assets/".Length) + "/";
        if(!File.Exists(dir + "/" + newName + ext))
        {
            File.Move (dir + name + ext, dir + newName + ext);
            File.Move (dir + name + ext + ".meta", dir + newName + ext + ".meta");
        }
        //string dir = 
    }

    //void OnPreprocessModel(){
    //    ModelImporter importer = assetImporter as ModelImporter;
    //    importer.importAnimation = false;
    //    importer.importMaterials = false;
    //}


    //void OnPostprocessModel(GameObject obj) {
    //    ModelImporter importer = assetImporter as ModelImporter;
    //    if(importer.globalScale == 0.01f)
    //    importer.globalScale  = importScale;
    //}


    //void OnPreprocessTexture () {	
    //    TextureImporter importer = assetImporter as TextureImporter;
    //    importer.filterMode = FilterMode.Point;
    //    importer.mipmapEnabled = false;
    //    importer.generateMipsInLinearSpace = false;
    //    importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
    //}
}