using UnityEngine;

namespace Forma.Runtime.Core.Features.Damage.Views
{
    public class DamageDealerView : MonoBehaviour
    {
        public int Damage => _damage;
        
        int _damage;

        public void Initialize(int damage)
        {
            _damage = damage;
        }
    }
}
