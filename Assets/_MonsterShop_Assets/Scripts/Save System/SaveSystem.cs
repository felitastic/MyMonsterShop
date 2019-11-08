using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Use with 
/// SaveSystem.SaveProfile(this); in Profile
/// and
/// ProfileData data = SaveSystem.LoadProfile();
/// value = data.value;
/// etc
/// https://youtu.be/XOjd_qU2Ido?t=919
/// </summary>
public static class SaveSystem 
{
    public static void SaveProfile(Profile profile)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.roar";
        FileStream stream = new FileStream(path, FileMode.Create);

        ProfileData data = new ProfileData(profile);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ProfileData LoadProfile()
    {
        string path = Application.persistentDataPath + "/gamedata.roar";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ProfileData data = formatter.Deserialize(stream) as ProfileData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No save file found in " + path);
            return null;
        }
    }
}
