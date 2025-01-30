
using System.Collections.Generic;
using UnityEngine;

public class EnemiesTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> enemies;
    public bool _allowOr;

    List<IEnemy> enemieSSs = new();
    private void Awake()
    {
        foreach (var enemy in enemies)
        {
            if(enemy.TryGetComponent<IEnemy>(out IEnemy newEn))
            {
                enemieSSs.Add(newEn);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(_allowOr)
            {
                foreach (var enemy in enemieSSs)
                {
                    enemy.AllowFollowPlayer();
                }
            }
            else
            {
                foreach (var enemy in enemieSSs)
                {
                    enemy.ForbidFollowPlayer();
                }
            }
        }
    }

}
