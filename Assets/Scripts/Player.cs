using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float speed;
    public int _pickups;
    float horizontal;
    float vertical;
    Vector2 lookDirection = new Vector2(1, 0);
    Rigidbody2D rigidbody2d;
    private UIManager _uiManager;
    public bool _isgameover = false;
    private SpriteRenderer _sprite;
    [SerializeField]
    public float _timeleft;
    public GameObject _winText;
    public GameObject _losetext;
    public AudioClip _winsource;
    public AudioClip _losesource;
    public AudioClip _pickupsound;
    private AudioSource _Audiosource;
    public GameObject _startText;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
        rigidbody2d = GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _sprite = GetComponent<SpriteRenderer>();
        _Audiosource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        StartCoroutine(Timer());
        StartCoroutine(Playermovement());
        if (_timeleft < 0)
        {
            _isgameover = true;
            Destroy(_sprite);
            AudioSource.PlayClipAtPoint(_losesource, transform.position, 0.5f);
            _losetext.SetActive(true);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R) && _isgameover == true)
            {
                SceneManager.LoadScene("10SecondGame");
            }

        }
        if (_timeleft <= 0)
        {
            _timeleft = 0;
        }
        if (_pickups >= 4)
        {
            _isgameover = true;
            _winText.SetActive(true);
            AudioSource.PlayClipAtPoint(_winsource, transform.position, 0.2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            Destroy(_sprite);
            _isgameover = true;
            _losetext.SetActive(true);
            AudioSource.PlayClipAtPoint(_losesource, transform.position, 0.5f);
        }
        if (other.tag == "Coin")
        {
            _pickups++;
            _uiManager.updateScore(_pickups);
            Destroy(other.gameObject);
            AudioSource.PlayClipAtPoint(_pickupsound, transform.position, 0.5f);
        }
    }
    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        if (_isgameover == false)
        {
            _timeleft -= Time.deltaTime;
            _uiManager.updateTime(_timeleft);
            _startText.SetActive(false);
        }

    }
    public IEnumerator Playermovement()
    {
        yield return new WaitForSeconds(2);
        if (_isgameover == false)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector2 move = new Vector2(horizontal, vertical);

            Vector2 position = rigidbody2d.position;
            position.x = position.x + speed * horizontal;
            position.y = position.y + speed * vertical;

            rigidbody2d.MovePosition(position);
        }
    }
}
