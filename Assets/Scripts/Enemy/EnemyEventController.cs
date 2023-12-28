using UnityEngine;
using UnityEngine.Events;

namespace Enemy
{
    public class EnemyEventController : MonoBehaviour
    {
        [SerializeField] private EnemyMovementController _enemyMovementController;
        [SerializeField] private EnemyHealthController _enemyHealthController;
        
        public void Instantiate(Transform player, UnityAction<EnemyEventController> dieMethods)
        {
            _enemyMovementController.SetPlayer(player);
            _enemyHealthController.EnemyDie.AddListener(_ => dieMethods(this));
        }
    }
}