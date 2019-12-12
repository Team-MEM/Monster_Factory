namespace MonsterFactory
{
    using System.Collections;
    using UnityEngine;

    public class Processor : Machine
    {
        #region Processor Recipe Struct - hold intake and output pair
        [System.Serializable]
        struct ProcessorRecipe
        {
            public string intake;
            public string output;
        }
        #endregion
        
        [Range(5f, 10f)]
        [SerializeField] float m_ProcessingRate;
        [SerializeField] ProcessorRecipe[] m_ProcessorRecipes;

        IEnumerator ProcessMaterial(string _materialName)
        {
            yield return new WaitForSeconds(m_ProcessingRate);
            
            // TODO: Need to figure out a way to debug this.
            foreach (ProcessorRecipe _recipe in m_ProcessorRecipes)
            {
                if (_materialName == _recipe.intake)
                {
                    PoolObject(_recipe.output);
                    break;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Item>().id == 0)
            {
                other.gameObject.SetActive(false);
                StartCoroutine(ProcessMaterial(other.gameObject.GetComponent<Item>().name));
            }
        }
    }
}
