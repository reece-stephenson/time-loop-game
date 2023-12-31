using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using System;

public class LeaderboardController : MonoBehaviour
{
    private HighscoreItem _scorePrefab;

    [SerializeField]
    private Vector2 _scoreStart = new Vector2(4.615f, 70.7f);

    private float _yIncrement = -25f;

    public static LeaderboardController Instance;

    public bool HasStarted { get; set; }

    void Start()
    {
        MainMenuActions.HasClickedPlay = false;
        if (Instance == null)
        {
            Instance = this;
            Instance._scorePrefab = Resources.Load<HighscoreItem>("HighscoreItem");
            DontDestroyOnLoad(gameObject);

            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (response.success)
                {
                    Instance.HasStarted = true;
                    Debug.Log("Leaderboard successfully conncted");
                    Instance.GetScores();
                }
                else
                {
                    Debug.Log("Not connected");
                    Debug.Log(response.Error);
                }
            });
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //if (!Instance.HasStarted)
        
    }

    public void StartAll()
    {
        Debug.Log("Starting Leaderboard");
        //LootLockerSDKManager.StartGuestSession((response) =>
        //{
        //    if (response.success)
        //    {
        //        Debug.Log("Leaderboard successfully conncted");
        //        GetScores();
        //    }
        //    else
        //    {
        //        Debug.Log("Not connected");
        //        Debug.Log(response.Error);
        //    }
        //});
        Debug.Log("Started Leaderboard");
    }

    public void SubmitScore(string name, int score, Action<LootLockerSubmitScoreResponse> callback)
    {
        string displayName = name + "\n" + DateTime.Now;
        string lKey = "prod-leaderboard";
        int leaderboardId = 17292;

        LootLockerSDKManager.SubmitScore(displayName, score, lKey, callback);
    }

    public void GetScores(Action<LootLockerGetScoreListResponse> callback)
    {
        int leaderboardId = 17276;
        int count = 10;

        LootLockerSDKManager.GetScoreList(leaderboardId, count, callback);
    }

    public void GetScores()
    {
        int leaderboardId = 17292;
        string lKey = "prod-leaderboard";
        int count = 10;

        LootLockerSDKManager.GetScoreList(lKey, count, (response) =>
        {
            if (response.success && !MainMenuActions.HasClickedPlay)
            {
                var targetVector = _scoreStart;

                if (MainMenuActions.HasClickedPlay)
                    return;

                foreach (var item in response.items)
                {
                    if (MainMenuActions.HasClickedPlay)
                        return;

                    try
                    {
                        var score = Instantiate(_scorePrefab, targetVector, Quaternion.identity);

                        score.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

                        var scoreScript = score.GetComponent<HighscoreItem>();

                        scoreScript.Rank = item.rank.ToString();
                        scoreScript.Name = item.member_id;
                        scoreScript.Score = item.score.ToString();

                        targetVector = new Vector2(targetVector.x, targetVector.y + _yIncrement);
                    }
                    catch { }
                }
            }
            else
            {
                Debug.Log("Not fetched");
                Debug.Log(response.Error);
            }
        });
    }
}
