using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSubmitter : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;

    public void Submit()
    {
        string name = _inputField.text;

        if ( (string.IsNullOrWhiteSpace(name)))
        {
            name = "anon";
        }

        if (name.Length > 8 )
        {
            name = name.Substring(0, 9);
        }

        int score = LoopController.CloneCount;

        if (score <= 0)
            score = 9999;

        Debug.Log(score);

        LeaderboardController.Instance.SubmitScore(name, score, (response) =>
        {
            if (response.success)
            {
                Debug.Log(response.rank);
            }
            else
            {
                Debug.Log("Not inserted");
                Debug.Log(response.Error);
            }
        });

        Destroy(GameObject.Find("Canvas"));
    }
}
