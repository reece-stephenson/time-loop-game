using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _rank;

    [SerializeField]
    private GameObject _name;

    [SerializeField]
    private GameObject _score;

    public string Rank { get; set; }
    public string Name { get; set; }
    public string Score { get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        if(_rank != null)
        _rank.GetComponent<TextMeshProUGUI>().text = Rank;

        if(_name != null)
        _name.GetComponent<TextMeshProUGUI>().text = Name;

        if(_score != null)
        _score.GetComponent<TextMeshProUGUI>().text = Score;
    }
}
