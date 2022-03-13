using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public Canvas gameMenu;
    public InputField nameText;
    private string newString;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        nameText.text = "";
    }

    public void StartGame()
    {
        int nameLength = nameText.text.Count();

        if (nameLength >= 2)
        {
            SceneManager.LoadScene(1);
            gameMenu.gameObject.SetActive(false);
            newString = nameText.text.ToString();
        }
        else
        {
            Debug.Log("Name was not entered.");
        }
    }


    [System.Serializable]

    class SaveData
    {
        public InputField nameText;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.nameText = nameText;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            nameText = data.nameText;
        }
    }
}
