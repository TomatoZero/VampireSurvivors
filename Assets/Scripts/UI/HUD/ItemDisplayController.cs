using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class ItemDisplayController : MonoBehaviour
    {
        [SerializeField] private Image _itemIco;
        [SerializeField] private TMP_Text _level;

        public void Setup(Sprite ico, int level)
        {
            _itemIco.sprite = ico;
            _level.text = $"Level {level}";
        }

        public void UpdateLevel(int level)
        {
            _level.text = $"Level {level}";
        }
    }
}