using UnityEngine;

public class ExitDoor : MonoBehaviour
{

    public GameObject levelCompleteUI;
    public GameObject backToMenuUI;
    public GameObject nextLevelUI;
    public GameObject imageUI;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelCompleteUI.SetActive(true);
            backToMenuUI.SetActive(true);
            nextLevelUI.SetActive(true);
            imageUI.SetActive(true);

            Time.timeScale = 0f; // Pause the game
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true;
        }
    }
    
}