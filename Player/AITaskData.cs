using System.Collections.Generic;
using UnityEngine;

namespace Custom.Logic.Player
{
    [CreateAssetMenu(fileName = "AI Task Data", menuName = "Add/AI/Task Data", order = 1)]
    public class AITaskData : ScriptableObject
    {
        public List<AITask> AITasks;
    }
}