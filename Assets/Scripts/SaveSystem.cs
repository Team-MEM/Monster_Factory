namespace MonsterFactory
{
    using UnityEngine;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class SaveSystem
    {
        public static void SaveMap(Map map)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/map.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            MapData data = new MapData(map);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static MapData LoadMap()
        {
            string path = Application.persistentDataPath + "/map.save";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                MapData data = formatter.Deserialize(stream) as MapData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("No file found at " + path);
                return null;
            }
        }

        public static void SaveUnlocks(UnlockManager unlock)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/unlocks.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            UnlockData data = new UnlockData(unlock);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static UnlockData LoadUnlocks()
        {
            string path = Application.persistentDataPath + "/unlocks.save";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                UnlockData data = formatter.Deserialize(stream) as UnlockData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("No file found at " + path);
                return null;
            }
        }

        public static void SaveEconomy(EconomyManager eco)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/economy.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            EconomyData data = new EconomyData(eco);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static EconomyData LoadEconomy()
        {
            string path = Application.persistentDataPath + "/economy.save";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                EconomyData data = formatter.Deserialize(stream) as EconomyData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("No file found at " + path);
                return null;
            }
        }
        public static void SaveResource(ResourceManager rec)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/resource.save";
            FileStream stream = new FileStream(path, FileMode.Create);

            ResourceData data = new ResourceData(rec);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static ResourceData LoadResource()
        {
            string path = Application.persistentDataPath + "/resource.save";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                ResourceData data = formatter.Deserialize(stream) as ResourceData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("No file found at " + path);
                return null;
            }
        }

    }
}

