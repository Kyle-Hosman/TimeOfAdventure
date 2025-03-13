using UnityEngine;

public class Enemy : MonoBehaviour
{
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

    public void Die() {
        Destroy(gameObject);
        Debug.Log("Enemy died");
    }
}
