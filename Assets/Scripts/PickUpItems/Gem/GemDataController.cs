using UnityEngine;

namespace PickUpItems.Gem
{
    public class GemDataController : MonoBehaviour
    {
        private int _xpBonus;

        public int XpBonus => _xpBonus;

        public void Setup(int xpBonus)
        {
            _xpBonus = xpBonus;
        }
    }
}