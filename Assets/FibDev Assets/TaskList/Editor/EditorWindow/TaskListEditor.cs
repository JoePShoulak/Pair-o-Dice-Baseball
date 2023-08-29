using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace FibDev.Tasks
{
    public struct Progress
    {
        public readonly float Value;
        public readonly string Title;

        public Progress(float value, string title)
        {
            Value = value;
            Title = title;
        }
    }

    public class TaskListEditor : EditorWindow
    {
        private VisualElement _container;

        private TextField _taskText;
        private Button _addTaskButton;
        private ScrollView _taskListScrollView;
        private ObjectField _savedTasksObjectField;
        private Button _loadTasksButton;
        private Button _saveProgressButton;
        private ProgressBar _taskProgressBar;
        private ToolbarToggle _formatToggle;
        private ToolbarSearchField _searchField;
        private Label _notificationLabel;

        private TaskListSO _taskListSo;


        /* == WINDOW == */
        [MenuItem("FibDev/TaskList")]
        public static void ShowWindow()
        {
            var window = GetWindow<TaskListEditor>("TaskList");
            window.minSize = new Vector2(350, 565);
        }

        public void CreateGUI()
        {
            _taskListSo = null;
            var filePath = FindPathToFolder("FibDev Assets") + "/TaskList/Editor/EditorWindow/";

            // Get UXML / USS
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(filePath + "TaskListEditor.uxml");
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(filePath + "TaskListEditor.uss");

            // Load UXML / USS
            _container = rootVisualElement;
            _container.Add(uxml.Instantiate());
            _container.styleSheets.Add(stylesheet);

            // Query and store all needed UI references
            _taskText = _container.Q<TextField>("taskText");
            _addTaskButton = _container.Q<Button>("addTaskButton");
            _taskListScrollView = _container.Q<ScrollView>("taskList");
            _savedTasksObjectField = _container.Q<ObjectField>("savedTasksObjectField");
            _loadTasksButton = _container.Q<Button>("loadTasksButton");
            _saveProgressButton = _container.Q<Button>("saveProgressButton");
            _taskProgressBar = _container.Q<ProgressBar>("taskProgressBar");
            _formatToggle = _container.Q<ToolbarToggle>("formatToggle");
            _searchField = _container.Q<ToolbarSearchField>("searchField");
            _notificationLabel = _container.Q<Label>("notificationLabel");

            // Add our callbacks
            _taskText.RegisterCallback<KeyDownEvent>(AddTask);
            _addTaskButton.clicked += AddTask;
            _loadTasksButton.clicked += LoadTasks;
            _formatToggle.RegisterValueChangedCallback(DisplayProgress);
            _saveProgressButton.clicked += SaveProgress;
            _searchField.RegisterValueChangedCallback(Search);
        }

        /* == TASKS == */
        private void AddTask(KeyDownEvent e) { if (Event.current.Equals(Event.KeyboardEvent("Return"))) AddTask(); }

        private void AddTask()
        {
            if (_taskListSo == null)
            {
                UpdateNotification("No TaskList Loaded");
                return;
            }

            if (string.IsNullOrEmpty(_taskText.value))
            {
                UpdateNotification("Task Has No Text");
                return;
            }

            var wasFocused = _taskText.panel.focusController.focusedElement == _taskText;

            _taskListScrollView.Add(CreateTask(_taskText.value));
            SaveTask(_taskText.value);
            _taskText.value = "";

            if (wasFocused) _taskText.Focus();

            DisplayProgress();
            UpdateNotification("Added Task");
        }

        private Toggle CreateTask(string text)
        {
            var taskItem = new Toggle
            {
                text = text
            };
            taskItem.RegisterValueChangedCallback(DisplayProgress);
            return taskItem;
        }

        private void LoadTasks() { LoadTasks(true); }

        private void LoadTasks(bool notify)
        {
            _taskListSo = _savedTasksObjectField.value as TaskListSO;
            if (_taskListSo == null)
            {
                UpdateNotification("No TaskList Selected");
                return;
            }

            _taskListScrollView.Clear();
            foreach (var task in _taskListSo.GetTasks()) _taskListScrollView.Add(CreateTask(task));

            DisplayProgress();
            if (notify) UpdateNotification($"Loaded \"{_taskListSo.name}\"");
        }

        private void SaveTask(string task)
        {
            _taskListSo.AddTask(task);
            SaveScriptableObject(_taskListSo);
        }

        /* == Progress == */
        private void SaveProgress()
        {
            if (_taskListSo == null)
            {
                UpdateNotification("No TaskList Loaded");
                return;
            }

            var tasks = (from Toggle task in _taskListScrollView.Children() where !task.value select task.text).ToList();

            _taskListSo.UpdateTasks(tasks);
            SaveScriptableObject(_taskListSo);

            LoadTasks(false);
            UpdateNotification("Progress Saved");
        }

        private Progress CalculateProgress()
        {
            float done = 0;
            float count = 0;

            foreach (var visualElement in _taskListScrollView.Children())
            {
                var task = (Toggle)visualElement;
                if (task.value) done++;
                count++;
            }

            var value = count <= 0 ? 1f : done / count;

            var progressTitle = _formatToggle.value ? $"{Mathf.Round(value * 1000) / 10f}%"
                : $"{done}/{count}";

            return new Progress(value, progressTitle);
        }

        private void DisplayProgress(ChangeEvent<bool> e) { DisplayProgress(); }

        private void DisplayProgress()
        {
            if (_taskListScrollView == null) return;

            var progress = CalculateProgress();

            _taskProgressBar.value = progress.Value;
            _taskProgressBar.title = progress.Title;
        }

        /* == MISC == */
        private static void SaveScriptableObject(Object so)
        {
            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssetIfDirty(so);
            AssetDatabase.Refresh();
        }

        private void Search(ChangeEvent<string> changeEvent)
        {
            var query = changeEvent.newValue.ToLower();

            foreach (var visualElement in _taskListScrollView.Children())
            {
                var task = (Toggle)visualElement;
                var taskText = task.text.ToLower();

                if (string.IsNullOrEmpty(query))
                {
                    task.RemoveFromClassList("lowlight");
                    continue;
                }

                if (taskText.Contains(query)) task.RemoveFromClassList("lowlight");
                else task.AddToClassList("lowlight");
            }
        }

        private void UpdateNotification(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            _notificationLabel.text = text;
        }

        private static string FindPathToFolder(string folderName)
        {
            var folderGUIDs = AssetDatabase.FindAssets(folderName);

            if (folderGUIDs.Length > 0)
                return AssetDatabase.GUIDToAssetPath(folderGUIDs[0]);

            Debug.LogWarning("FibDev Assets Not Found");
            return "";
        }
    }
}
