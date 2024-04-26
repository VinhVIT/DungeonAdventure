using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : Mover
{
    private SpriteRenderer CharSprite;
    private Animator charAnim;
    private Rigidbody2D rb;
    public RectTransform Healthbar;
    public RectTransform EmtyHealthbar;

    //attack
    private bool isAlive = true;
    private bool isAttacking = false;
    public GameObject weapon;
    //Dash
    private bool canDash = true;
    private bool isDashing;
    public int direction;
    public float dashPower;
    public float dashingTime;
    private float dashCooldown = 3f;
    //dashEfect
    public ShadowEffect shadowEffect;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        CharSprite = GetComponent<SpriteRenderer>();
        charAnim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        move.x = Input.GetAxisRaw("Horizontal");//left right move
        move.y = Input.GetAxisRaw("Vertical");//up down move
        charAnim.SetFloat("speed", move.sqrMagnitude);

        if (isAlive && !isAttacking)
        {
            UpdateMotor(new Vector3(move.x, move.y, 0));
        }
    }
    private void Update()
    {
        SetDirection();
        if (isDashing)
        {//avoid do anything while dashing
            return;
        }
        // //attack
        // if(Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space)){
        //     StartCoroutine("StandingAttack");
        // }
        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            AudioManager.Instance.PlaySFX("Dash");
            StartCoroutine(Dash());
            StartCoroutine(dashEffect());
        }

    }
    public void SwapChar(int skinId)
    {
        charAnim.enabled = false;
        //CharSprite.sprite= GameManager.instance.playersprites[skinId];//no need
        charAnim.runtimeAnimatorController = GameManager.instance.playerAnims[skinId];
        charAnim.enabled = true;
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;
        base.ReceiveDamage(dmg);
        if (hitpoint <= 1 && isAlive)
        {
            AudioManager.Instance.PlayMusic("HeartBeat");

        }
    }
    public void OnLevelUp()
    {
        //upgrade health
        maxHitpoint += 2;
        hitpoint = maxHitpoint;
        EmtyHealthbar.sizeDelta = new Vector2(EmtyHealthbar.sizeDelta.x + 13f, EmtyHealthbar.sizeDelta.y);
        Healthbar.sizeDelta = EmtyHealthbar.sizeDelta;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
        {
            return;
        }
        hitpoint += healingAmount;
        Healthbar.sizeDelta = new Vector2(Healthbar.sizeDelta.x + 6.5f * healingAmount, Healthbar.sizeDelta.y);
        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
        }
        if (Healthbar.sizeDelta.x > EmtyHealthbar.sizeDelta.x)
        {
            Healthbar.sizeDelta = EmtyHealthbar.sizeDelta;
        }
        GameManager.instance.Showtext("+" + healingAmount + "hp", 30, Color.green, transform.position, Vector3.up * 25, 1f);
    }
    protected override void Death()
    {
        isAlive = false;
        weapon.SetActive(false);
        canDash = false;
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySFX("GameOver");
        GameManager.instance.DeathMenu.SetTrigger("Show");
    }
    public void Respawn()
    {
        Heal(maxHitpoint);
        Healthbar.sizeDelta = EmtyHealthbar.sizeDelta;
        isAlive = true;
        weapon.SetActive(true);
        canDash = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
    private void DashDirection()
    {
        if (direction == 0)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        }
        else if (direction == 1)
        {
            rb.velocity = new Vector2(0f, transform.localScale.y * dashPower);
        }
        else if (direction == 2)
        {
            rb.velocity = new Vector2(0f, transform.localScale.y * -dashPower);
        }
    }
    private void SetDirection()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            direction = 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            direction = 2;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = 0;
        }
    }
    // IEnumerator StandingAttack(){
    //     isAttacking = true;
    //     yield return new WaitForSeconds(0.5f);
    //     isAttacking = false;
    // }
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        DashDirection();
        yield return new WaitForSeconds(dashingTime);
        rb.velocity = new Vector2(0f, 0f);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    IEnumerator dashEffect()
    {
        shadowEffect.enabled = true;
        yield return new WaitForSeconds(dashingTime);
        shadowEffect.enabled = false;
    }
}
