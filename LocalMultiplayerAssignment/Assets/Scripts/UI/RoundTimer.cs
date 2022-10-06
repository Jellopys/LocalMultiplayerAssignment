using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _roundtime;
    private int _roundTimeInt;

    // void Awake()
    // {
    //     _roundtime = 10f;
    // }

    void Update()
    {
        if (_roundtime <= 0f) {return;}

        _roundtime -= 1f * Time.deltaTime;
        _roundTimeInt = (int)_roundtime;
        _timerText.text = _roundTimeInt.ToString();
    }

    public void StartRoundTimer(float time)
    {
        _roundtime = time;
    }
}
