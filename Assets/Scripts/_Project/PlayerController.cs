using Template;
using UnityEngine;

namespace _Project
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private LayerMask _tileLayerMask;
        
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