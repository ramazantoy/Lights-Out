using _Project.GameManager;
using Template;
using UnityEngine;

namespace _Project
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private LayerMask _tileLayerMask;
        
        private void Start()
        {
            UserInput.Instance.TouchEvent += Touch; // UserInput'dan gelen dokunma event'i
        }

        private void Touch(Template.UserInput.TouchType touchType) //Dokunmanın tipine ve oyunun duruma göre tile'ın state'inin değiştirilmesi
        {
            if (touchType != UserInput.TouchType.Down || GameManager.GameManager.Instance.GameState!=GameState.Playing) return;
        
            var hit = UserInput.Instance.Raycast(_tileLayerMask);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Tile.Tile>().SwitchTileState();
            }
        }
    }
}