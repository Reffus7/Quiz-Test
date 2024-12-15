using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "SpriteSetSO 1", menuName = "SpriteSetSO")]
    public class SpriteSetSO : ScriptableObject {
        [SerializeField] private Sprite[] _sprites;

        public Sprite[] Sprites => _sprites;

    }
}