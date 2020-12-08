using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Properties

    /// <summary>
    /// rigidbody component of player
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// animator component of player
    /// </summary>
    private Animator anim;
    // Start is called before the first frame update

    /// <summary>
    /// true if game started, false otherwise
    /// </summary>
    private bool inGame;

    /// <summary>
    /// position of player when game starts
    /// </summary>
    private Vector3 posOrigin;

    /// <summary>
    /// rotation of player when game starts
    /// </summary>
    private Quaternion rotOrigin;

    #endregion

    #region Private Methods

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        posOrigin = this.transform.position;
        rotOrigin = this.transform.rotation;

        this.Idle();
        GameManager.Instance.OnStateChange += this.OnGameStateChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inGame)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0f, 250f), ForceMode2D.Force);
        }
    }

    void Init()
    {
        this.transform.position = posOrigin;
        this.transform.rotation = Quaternion.identity;
        this.Fly();
    }

    void Idle()
    {
        inGame = false;
        anim.SetTrigger("Idle");
        rb.Sleep();
    }

    void Fly()
    {
        rb.WakeUp();
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        inGame = true;

        anim.SetTrigger("Fly");
    }


    void Die()
    {
        inGame = false;
        anim.SetTrigger("Die");

        GameManager.Instance.EndGame();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        this.Die();
    }

    void OnGameStateChange(GameManager.Game_State state)
    {
        if (state == GameManager.Game_State.GAME_PLAY)
        {
            this.Init();
        }
    }

    #endregion
}
