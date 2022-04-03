using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDisplay : MonoBehaviour
{

    [SerializeField] WaveSystem waveSystem;

    [SerializeField] TextMeshProUGUI waveNumText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        waveNumText.text = ("Round " + waveSystem.waveNum.ToString());
    }
}
