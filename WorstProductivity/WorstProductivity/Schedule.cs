using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorstProductivity
{
    class Schedule
    {
        private List<Task> taskList;
        private string tasksToString;
        public Schedule()
        {
            taskList = new List<Task>();
            tasksToString = "";
        }
        public void AddTask(string title, DateTime start, DateTime end)
        {
            if (taskList.Count > 0)
                title = CheckTitle(title);
            Task task = new Task(title, start, end);
            if (taskList.Count > 0)
                insertIntoList(task);
            else
                taskList.Add(task);
            Refresh();
            //MessageBox.Show("" + taskList.Count);
        }
        public void RemoveTask(string title)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                if (title == taskList[i].title)
                {
                    taskList.RemoveAt(i);
                    Refresh();
                }
            }
        }
        public void EditTaskStart(string title, DateTime newStart)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                if (title == taskList[i].title && newStart != taskList[i].start)
                {
                    taskList[i].start = newStart;
                    Refresh();
                }
            }
        }
        public void EditTaskEnd(string title, DateTime newEnd)
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                if (title == taskList[i].title && newEnd != taskList[i].end)
                {
                    taskList[i].end = newEnd;
                    Refresh();
                }
            }
        }
        private void Refresh()
        {
            tasksToString = "";
            foreach (Task t in taskList)
            {
                tasksToString += t.ToString() + "\n";
            }
        }
        public string GetString()
        {
            return tasksToString;
        }

        private void insertIntoList(Task task)
        {
            List<Task> newTasks = new List<Task>();
            bool added = false;
            foreach (Task t in taskList)
            {
                //if (!added)
                //{
                    if (t.start < task.start)
                    {
                        newTasks.Add(t);
                    }
                    if (t.start >= task.start && !added)
                    {
                        newTasks.Add(task);
                        added = true;
                        
                    }
                    if (added)
                        newTasks.Add(t);
                //}
                //newTasks.Add(t);
            }
            if (!newTasks.Contains(task))
                newTasks.Add(task);
            taskList.Clear();
            foreach (Task t in newTasks)
            {
                taskList.Add(t);
            }
        }

        private string CheckTitle(string title)
        {
            string newTitle = title;
            bool changed = false;
            foreach (Task t in taskList)
            {
                if (t.title == title)
                {
                    newTitle += " 1";
                    changed = true;
                }
            }
            if (changed)
            {
                return CheckTitleRecursive(newTitle);
            }
            return newTitle;
        }

        private string CheckTitleRecursive(string title)
        {
            foreach (Task t in taskList)
            {
                if (t.title == title)
                {
                    int num = Convert.ToInt32(title.Substring(title.Length - 1, 1));
                    num++;
                    string[] titleWord = title.Split();
                    titleWord[0] += " " + num;
                    title = CheckTitleRecursive(titleWord[0]);
                }
            }
            return title;
        }

        private class Task
        {
            public string title { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }

            public Task(string Title, DateTime Start, DateTime End)
            {
                title = Title;
                start = Start;
                end = End;
            }

            public string ToString()
            {
                return start.ToString("hh:mm tt") + " - " + end.ToString("hh:mm tt") + " " + title;
            }
        }
    }
}
