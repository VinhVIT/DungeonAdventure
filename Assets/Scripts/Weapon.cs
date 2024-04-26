using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage Manage
    public int[] damage = { 1, 2, 3, 4, 5 };
    public float[] pushForce = { 15f, 16f, 17f, 18f, 20f };//push enemy back when attack
    //Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;
    //Swing
    private float cooldown = 0.5f;
    private float lastSwing;
    public BoxCollider2D attackRange;
    private bool attackBlocked;
    private float delay = 0.5f;
    public bool isAttacking { get; private set; }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    protected override void OnCollide(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Damage dmg = new Damage
            {
                damagedeal = damage[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            col.SendMessage("ReceiveDamage", dmg);
        }
    }
    private void Swing()
    {
        if (attackBlocked)
            return;
        AudioManager.Instance.PlaySFX("Slash");
        GetComponent<Animator>().SetTrigger("Attack");
        attackBlocked = true;
        isAttacking = true; 
        StartCoroutine(DelayAttack());
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
    }
    public void UpgradeWeapon()
    {
        weaponLevel++;
        attackRange.size = new Vector2(attackRange.size.x, attackRange.size.y + 0.02f);
        attackRange.offset = new Vector2(attackRange.offset.x, attackRange.offset.y + 0.01f);
        spriteRenderer.sprite = GameManager.instance.Weaponsprites[weaponLevel];
        //Change wep stat
    }
    public void SetWeapLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.Weaponsprites[weaponLevel];
    }
    public void ResetIsAttacking() => isAttacking = false;
}
