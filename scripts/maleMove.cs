using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using TabObject;


public class maleMove : MonoBehaviour
{

    // Start is called before the first frame update

    private Rigidbody rb;
    private Animation anim;
    [SerializeField] public FixedJoystick joystick;
    public float moveSpeed;
    bool animCheck = true;

    //public static GameObject Roses;
    public GameObject Joy;
    public GameObject Okay;
    public GameObject RoseHand;
    public TMPro.TextMeshProUGUI tmpTxt;
    public TMPro.TextMeshProUGUI tmpConfess;
    public TMPro.TextMeshProUGUI tmpMine;

    public AudioClip Song;
    private AudioSource audioSource;
    public int count = 0;
    public int score = 3;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
        RoseHand.SetActive(false);
        Okay.SetActive(false);
        tmpConfess.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {

        float x = joystick.Horizontal;//CrossPlatformInputManager.GetAxis("Horizontal");
        float y = joystick.Vertical;//CrossPlatformInputManager.GetAxis("Vertical");
        //Debug.Log("x: " + x);
        //Debug.Log("y: " + y);

        Vector3 movement = new Vector3(x * moveSpeed, 0.0f, y * moveSpeed);

        rb.velocity = movement * 2f;
        //if (Input.GetKey(KeyCode.Space))
        //{
        //          anim.Play("walk");
        //      }
        //else
        //{
        //          anim.Play("idle");
        if (x != 0 && y != 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);
        }

        if (x != 0 || y != 0)
        {
            if (animCheck)
            {
                anim.Play("walk");
            }
            else
            {
                anim.Play("pickup");
            }
        }
        else
        {
            if (animCheck)
            {
                anim.Play("idle");
            }
            else
            {

                anim.Play("pickup");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "roses")
        {
            Debug.Log("Collide");
            animCheck = false;
            moveSpeed = 0;
            StartCoroutine(Wait());


        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        TapObject.Rose1.SetActive(false);
        yield return new WaitForSeconds(1f);
        RoseHand.SetActive(true);
        animCheck = true;
        moveSpeed = 0.1f;
        tmpTxt.enabled = false;
        tmpConfess.enabled = true;
        //tmpTxt.text = "Hey!, I Have something to tell you";
        audioSource.PlayOneShot(Song);
		yield return new WaitForSeconds(3f);
		tmpConfess.text = "I really like you, I Want to confess.";
		yield return new WaitForSeconds(4f);
		//animCheck = ;
		moveSpeed = 0;
        Joy.SetActive(false);
        Okay.SetActive(true);
        
    }
    
        
    public void sadScene()
        {
            SceneManager.LoadScene("qs3");
        }
    public void qScene()
        {
            SceneManager.LoadScene("qs1");
        }
    public void AddHealth()
    {
        PlayerStats.Instance.AddHealth();
    }

    public void Heal(float health)
    {
        PlayerStats.Instance.Heal(health);
    }

    public void Hurt(float dmg)
    {
        PlayerStats.Instance.TakeDamage(dmg);
    }

    public void _Heal()
	{
        count++;
        score++;
        updateTextMine(count,score);
        

    }

    public void _Hurt()
	{
        count++;
        score--;
        updateTextMine(count,score);


	}


    public void updateTextMine(int num, int _score)
	{
        if(_score == 0)
		{
            SceneManager.LoadScene("qs3");
        }
        if (num == 1)
		{
            tmpMine.text = "I have fallen in love with the person that you are. " +
            "The way you make life so beautiful and simple has made me realize that you are the one that I want to love and take care of forever. " +
   "I do not know how you feel, but I just had to let you know what has been running through my mind. Whatever your answer is, " +
   "I promise that I will not make our relationship one bit awkward for you.";
        }

        if(num == 2)
		{
            tmpMine.text = "I will always love you and be the friend you want me to be.";

        }

        if(num == 3 && _score < 3)
		{
            SceneManager.LoadScene("qs3");
        }

        if(num == 3 && _score >= 3)
		{
            SceneManager.LoadScene("qs2");
		}


	}

}
