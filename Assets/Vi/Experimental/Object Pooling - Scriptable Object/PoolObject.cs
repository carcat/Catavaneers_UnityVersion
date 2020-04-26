using UnityEngine;

namespace Experimental
{
    [CreateAssetMenu(fileName = "New Pool Object", menuName = "Pool Object")]
    public class PoolObject : ScriptableObject
    {
        public string objectName;
        public GameObject prefab;
        public int amount;
        public float activeTime;


        private void UpdatePooledObjectInEditor()
        {
            if (prefab)
                objectName = prefab.name;
            else
                objectName = "";

            if (amount < 0)
                amount = 0;
        }


        private void OnValidate()
        {
            UpdatePooledObjectInEditor();
        }
    }
}