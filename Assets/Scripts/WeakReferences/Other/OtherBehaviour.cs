using System;
using UnityEngine;

namespace WeakReferences.Other
{
    [System.Serializable]
    public class OtherBehaviour : MonoBehaviour
    {
        [SerializeField] private OtherBehaviourData _properties;


        public OtherBehaviourData SetProperties
        {
            /*
             * SomeBehavior Script'i  other'dan farklı bir sahnede yer alıp other'a ait fonksiyonları çalıştırmak isterse scritable objeden gelen,
             * properties sınıfını other'ın properties'ine setlemeli böylelikle other'ın sahip olduğu özellikler farklı bir sahnede olunmasına rağmen korunacaktır.
             *
             * */

            set { _properties = value; }
        }

        public void DoSomething()
        {
            Debug.Log("DoSomething");
        }

        public void DoSomething2()
        {
            Debug.Log("DoSomething2");
        }

        private void Awake()
        {
            Debug.Log("Other Behaviour Awake");
        }
    }
}