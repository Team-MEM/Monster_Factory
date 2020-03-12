namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "", menuName = "Task")]
    public class Task : ScriptableObject
    {
        public bool isCompleted;
        public Item item;
        public int goal;
        public int reward;
    }
}


