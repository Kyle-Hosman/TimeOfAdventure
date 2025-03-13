using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    private float _health = 1f;

    public float health {
        set {
            _health = value;
            if (_health <= 0) {
                Die();
            }
        }
        get {
            return _health;
        }
    }


    // public void TakeDamage(int damage) {
    //     Debug.Log("Enemy took " + damage + " damage");
    //     health -= damage;
    // }
    private void Start() {
        animator = GetComponent<Animator>();
    }   

    public void Die() {
        Debug.Log("Enemy died");
        // Add any death animation or effects here
        animator.SetTrigger("Die");
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }
}
