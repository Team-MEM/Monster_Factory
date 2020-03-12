
namespace MonsterFactory
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TaskManager : Singleton<TaskManager>
    {
        [SerializeField] Task[] m_Tasks;
        [SerializeField] int m_CurrentProgress;
        public Task currentTask;

        void Awake()
        {
            base.Awake();
            StartCoroutine("Delay");
        }

        //has to wait for MainUIManageer to setup before running
        IEnumerator Delay()
        {
            yield return null;
            SetupTask();
        }

        public void SetupTask()
        {
            m_CurrentProgress = 0;
            currentTask =  m_Tasks[Random.Range(0, m_Tasks.Length - 1)];
            currentTask.isCompleted = false;
            MainUIManager.Instance.UpdateTaskMonsterSprite(currentTask.item.sprite);
            MainUIManager.Instance.UpdateTaskUI(0, currentTask.goal);
        }

        public void ProgressCurrentTask()
        {
            m_CurrentProgress++;
            m_CurrentProgress = Mathf.Clamp(m_CurrentProgress, 0, currentTask.goal);

            if (m_CurrentProgress == currentTask.goal)
                currentTask.isCompleted = true;

            MainUIManager.Instance.UpdateTaskUI(m_CurrentProgress, currentTask.goal);
        }


    }
}
