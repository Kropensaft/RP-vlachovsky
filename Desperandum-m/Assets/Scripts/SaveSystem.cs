using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public int playerHealth;
    public float playerFuel;
    public int playerScore;
    public Vector3 playerPosition;

    private string saveFilePath;

    private void Start()
    {
        // Set the save file path to a file named "save.txt" in the application's data folder
        saveFilePath = Path.Combine(Application.dataPath, "save.txt");
    }

    public void SavePlayerData()
    {
        // Create a string to store the player's data
        string data = playerHealth + "|" + playerFuel + "|" + playerScore + "|" + playerPosition.x + "|" + playerPosition.y + "|" + playerPosition.z;

        // Write the data to the save file
        File.WriteAllText(saveFilePath, data);
    }

    public void LoadPlayerData()
    {
        // Load the data from the save file
        string data = File.ReadAllText(saveFilePath);

        // Split the data into individual values
        string[] values = data.Split('|');

        // Set the player's values based on the data
        playerHealth = int.Parse(values[0]);
        playerFuel = float.Parse(values[1]);
        playerScore = int.Parse(values[2]);
        playerPosition = new Vector3(float.Parse(values[3]), float.Parse(values[4]), float.Parse(values[5]));
    }

    public void LoadLevelScene()
    {
        // Load the level scene
        SceneManager.LoadScene("FirstLevel");
    }

    private void Update()
    {
        // If the current scene is the level scene, update the player's position, health, fuel, and score
        if (SceneManager.GetActiveScene().name != "FirstLevel")
        {
            // Update the player's position, health, fuel, and score
            transform.position = playerPosition;
            GetComponent<Character>().currentHealth = playerHealth;
            GetComponent<Character>().currentFuel = playerFuel;
            GetComponent<Character>().score = playerScore;
            LoadLevelScene();

        }
    }
}
