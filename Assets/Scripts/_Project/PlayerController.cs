using Template;
using UnityEngine;

namespace _Project
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        [SerializeField] private LayerMask _tileLayerMask;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UserInput.Instance.TouchEvent += Touch;
        }

        private void Touch(Template.UserInput.TouchType touchType)
        {
            if (touchType != UserInput.TouchType.Down) return;
        
            var hit = UserInput.Instance.Raycast(_tileLayerMask);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Tile.Tile>().SwitchTileState();
            }
        }
    }
}