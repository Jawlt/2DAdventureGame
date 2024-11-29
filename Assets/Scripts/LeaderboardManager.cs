using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Dan.Main;
using System.Threading;

public class LeaderboardManager : MonoBehaviour
{
    public string mainMenuScence;

    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    private string publicLeaderBoardKey = "aaaab56c9872d3cf210e56b1c63acd5e95f5faf97a1b503c01b8b60985c97be8";
    private string secretLeaderBoardKey = "e3e20859c133d9295b6116f69d2380d8bbf991f1d75672f6ae8f9ca2d4363a6b36a3ddb617ff41a8c66baf391adfffaabb61aa5995e7f524e95899d258cba9622d636d7d68121327d91b14128dbd570f28b09518b4c7d882f70ac08e1fd54111ec7c00334a595ed1e886ce859da6663e7ce95b4d945001c8d20e821d8ff86314";

    void Start()
    {
        LeaderboardCreator.ResetPlayer();
        GetLeaderBoard();
    }

    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderBoardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderBoardKey, username, score, ((msg) =>
        {
            GetLeaderBoard();
            LeaderboardCreator.ResetPlayer();
        }));
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScence);
    }
}
