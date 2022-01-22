using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    public Player player;
    [SerializeField]
    private Text _timerText;
    public AudioClip _brmusic;
    public AudioClip _openingMusic;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score " + 0;
        StartCoroutine(musicDelay());
        AudioSource.PlayClipAtPoint(_openingMusic, new Vector3(0, 0, 0), 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void updateScore(int _pickups)
    {
        _scoreText.text = "Score: " + _pickups.ToString();
    }
    public void updateTime(float _time)
    {
        _timerText.text = "" + _time.ToString(".0");
    }
    public IEnumerator musicDelay()
    {

        yield return new WaitForSeconds(2);
        AudioSource.PlayClipAtPoint(_brmusic, new Vector3(0,0,0), .2f);
    }
}
