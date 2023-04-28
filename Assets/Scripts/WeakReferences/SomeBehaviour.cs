using Unity.VisualScripting;
using UnityEngine;
using WeakReferences.Other;

namespace WeakReferences
{
    public class SomeBehaviour : MonoBehaviour
    {
        [SerializeField] private OtherBehaviour _otherBehaviour; //Aynı sahnede olduklarını varsayarsak.

        [SerializeField]
        private OtherBehaviourScriptableObject _otherBehaviourScriptableObject; // Farklı sahnede  yer alıyorlarsa scriptable obje  yardımıyla  Other Behaviour oluşturulur. 

        /*
         * Bu yöntemle otherbehaviour üzerinde yer alan unity fonksiyonlarını kullanamayız.
         * Update,Start,Awake gibi. bu fonksiyonları kullanmak istiyorsak addcomponent yapmalıyız.
         */
        void Start()
        {
            if (_otherBehaviour == null)
            {
                /*
                 * Unity fonksiyonlarını kullanmayacaksak bu yöntemi  tercih edebiliriz.
                 * Bunun yanında other'ın monobehaviour'dan aldığı mirası kaldırıp bu script üzerinden update fixedupdate gibi fonksiyonları otherda oluşturup buradan deltaTime vb. parametreler göndererek çağırabiliriz.
                 * Ama bu şekilde otherBehavior'un hiç bir sahne üzerinde oyun objesi olarak yer alması gerekmiyor.
                 */
                _otherBehaviour = _otherBehaviourScriptableObject.CreateOtherBehaviour(); //
                
                /*
                 //Unity Fonksiyonlarını kullanmamız gerekiyorsa addcomponenet yapabiliriz.
                 //Yada  sahne üzerinde ayrı bir obje olarak instantiate işlemide gerçekleşebilir.
                  
                _otherBehaviour = transform.AddComponent<OtherBehaviour>();
                _otherBehaviour.SetProperties = _otherBehaviourScriptableObject.Properties;
                */
                
            }

            _otherBehaviour.DoSomething();
            _otherBehaviour.DoSomething2();
        }
    }
}