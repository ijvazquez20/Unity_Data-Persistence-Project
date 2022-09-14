using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler Instance;

    public GameObject inputField;
    public string playerName;
    public string bestPlayerName;
    public int bestPlayerScore;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadScore();
        } else {
            Destroy(gameObject);
            return;
        }
    }

    public void StartNew() {
        playerName = GetInputField();
        SceneManager.LoadScene(1);
    }

    public string GetInputField() {
        string nameText = inputField.GetComponent<TMP_InputField>().text;
        return nameText;
    }

    [System.Serializable]
    class SaveData {
        public string bestPlayerName;
        public int bestPlayerScore;
    }

    public void SaveScore() {
        SaveData data = new SaveData();
        data.bestPlayerScore = bestPlayerScore;
        data.bestPlayerName = bestPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile1.json", json);
    }

    public void LoadScore() {
        string path = Application.persistentDataPath + "/savefile1.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerScore = data.bestPlayerScore;
            bestPlayerName = data.bestPlayerName;
        }
    }
}
