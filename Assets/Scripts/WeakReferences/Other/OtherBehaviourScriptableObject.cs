using UnityEngine;

namespace WeakReferences.Other
{
    [CreateAssetMenu(fileName = "OtherBehaviour", menuName = "ScriptableObjects/OtherBehaviour")]
    public class OtherBehaviourScriptableObject : ScriptableObject
    {
        public OtherBehaviourData Properties;

        public OtherBehaviour CreateOtherBehaviour()
        {
            OtherBehaviour otherBehaviour = new OtherBehaviour();

            otherBehaviour.SetProperties = Properties;
            return otherBehaviour;
        }
    }
}