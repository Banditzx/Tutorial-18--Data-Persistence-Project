using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// The menu manager UI.
/// </summary>
public class MenuManagerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private TextMeshProUGUI errorMessage;
    [SerializeField] private TMP_InputField usernameField;

    private string MyName { get; set; }
    private string BestUsername { get; set; }
    private int BestScore { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        MyName = StoreManager.Instance.Username;
        BestUsername = StoreManager.Instance.BestUsername;
        BestScore = StoreManager.Instance.BestScore;
        usernameField.text = MyName != null ? MyName : "";

        if (BestUsername != null && BestUsername.Length > 0 && BestScore > 0)
        {
            bestScore.text = string.Format("Best Score: {0} ({1})", BestUsername, BestScore);
        }
    }

    /// <summary>
    /// Resets the score.
    /// </summary>
    public void ResetScore()
    {
        StoreManager.Instance.Reset();
    }

    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        if (usernameField == null || usernameField.text.Length < 3)
        {
            errorMessage.gameObject.SetActive(true);
        }
        else
        {
            errorMessage.gameObject.SetActive(false);
            StoreManager.Instance.Username = usernameField.text;
            StoreManager.Instance.Save();
            SceneManager.LoadScene(1);
        }
    }

    /// <summary>
    /// Exits this instance.
    /// </summary>
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}