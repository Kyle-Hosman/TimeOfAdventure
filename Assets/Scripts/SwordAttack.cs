using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 3;
    Vector2 rightAttackOffset;
    public BoxCollider2D swordCollider;

    private void Start() {
        rightAttackOffset = transform.position;
        swordCollider.enabled = false;
    }

    public void AttackRight() {
        Debug.Log("Sword attack right");
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() {
        Debug.Log("Sword attack left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(-rightAttackOffset.x, rightAttackOffset.y);
    }

    public void StopAttack() {
        swordCollider.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log("Hit enemy");
            print("Hit Enemy");
            // Assuming the enemy has a script with a health property
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null) {
                enemy.health -= damage;
            }
        }
    }

}
