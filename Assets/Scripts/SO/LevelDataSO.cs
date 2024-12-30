using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "LevelDataSO 1", menuName = "LevelDataSO")]
    public class LevelDataSO : ScriptableObject {
        [SerializeField] private int _rows;
        [SerializeField] private int _columns;

        public int Rows => _rows;
        public int Columns => _columns;

    }
}