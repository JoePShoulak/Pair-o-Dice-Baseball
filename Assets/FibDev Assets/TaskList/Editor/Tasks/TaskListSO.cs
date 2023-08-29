using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace FibDev.Tasks
{
    [CreateAssetMenu(menuName = "TaskList", fileName = "New TaskList")]
    public class TaskListSO : ScriptableObject
    {
        [SerializeField]
        private List<string> tasks;

        public List<string> GetTasks()
        {
            return tasks;
        }

        public void UpdateTasks(List<string> savedTasks)
        {
            tasks.Clear();
            foreach (var task in savedTasks) AddTask(task);
        }

        public void AddTask(string savedTask)
        {
            tasks.Add(savedTask);
        }
    }
}