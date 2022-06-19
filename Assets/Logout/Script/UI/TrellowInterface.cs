using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//trellow make possible the complition of tasks in the game
//tasks can be, make money, rescue a pet or adopt a pet
//after it´s conclusion, it will be removed from the list and added after a time to a list of events
//the time will be decided by the cooldown of the event
public class TrellowInterface : MonoBehaviour
{
    public class TrellowTask
    {
        public Event Event;
        public TaskButton TaskButtonInstance;
        public float RefTime;

        public TrellowTask(Event task, TaskButton taskButtonInstance)
        {
            Event = task;
            TaskButtonInstance = taskButtonInstance;
            RefTime = 0f;
        }
    }

    private List<Event> taskEvents = new List<Event>();
    private List<TrellowTask> tasksToExecute = new List<TrellowTask>();
    Dictionary<Event, TaskButton> taskButtonInstances = new Dictionary<Event, TaskButton>();
    [SerializeField] private TaskButton buttonRef;
    [SerializeField] private Transform content;
    [SerializeField] private Transform todo;
    [SerializeField] private Transform doing;
    [SerializeField] private Transform done;
    private Action OnUpdate;

    private void Start()
    {
        Donation donation = new Donation(10);
        AddNewTask(new Event[] { donation });
        StartCoroutine(UpdateTasks());
    }

    public void Hide()
    {
        content.gameObject.SetActive(false);
    }

    public void Show()
    {
        content.gameObject.SetActive(true);
    }

    private void AddNewTask(Event task)
    {
        taskEvents.Add(task);
        InitTasks();
    }

    private void AddNewTask(Event[] tasks)
    {
        foreach (Event task in tasks)
        {
            taskEvents.Add(task);
        }
        InitTasks();
    }

    private void RemoveTask(TrellowTask task)
    {
        RemoveTask(task.Event);
        tasksToExecute.Remove(task);
    }

    private void RemoveTask(Event task)
    {
        //destroy button
        if (taskButtonInstances.ContainsKey(task))
        {
            task.DeclineEvent();
            TaskButton instance = taskButtonInstances[task];
            GameObject.Destroy(instance.gameObject);

            //repeat
            TimerEvent.Create(() =>
            {
                AddNewTask(task);
            }, Random.Range(task.TimeRange.x, task.TimeRange.y));

            taskButtonInstances.Remove(task);
        }

        //remove task from list
        taskEvents.Remove(task);
    }

    private void Update()
    {

    }

    private IEnumerator UpdateTasks()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            //for each task
            //register action to update method
            for (int i = 0; i < tasksToExecute.Count; i++)
            {
                TrellowTask task = tasksToExecute[i];
                if (task.TaskButtonInstance.transform.parent == doing)
                {
                    CompletingTask(ref task.RefTime, task.Event.TimeRange.y, task, task.TaskButtonInstance.taskSlider);
                }
                if (task.TaskButtonInstance.transform.parent == done)
                {
                    bool itLoops = false;

                    if (task.Event is Donation)
                    {
                        itLoops = true;
                    }

                    CompletedTask(ref task.RefTime, task.Event.TimeRange.y, task, task.TaskButtonInstance.taskSlider, true);
                }
            }
        }
    }

    private void InitTasks()
    {
        //each item in task
        //instantiate new button 
        foreach (Event task in taskEvents)
        {
            if (!taskButtonInstances.ContainsKey(task))
            {
                TaskButton taskButton = Instantiate<TaskButton>(buttonRef);
                taskButton.transform.parent = todo;
                taskButtonInstances.Add(task, taskButton);


                taskButton.InitButton(task.EventSprite, task.EventName, () =>
                {
                    if (taskButton.transform.parent == todo)
                    {
                        //change parent
                        //init progress bar, based on time to be filled 
                        taskButton.transform.parent = doing;

                        //add to the execute task list
                        tasksToExecute.Add(new TrellowTask(task, taskButton));
                    }
                });
            }
        }
    }

    //it calculate the time to a task to be completed and return how much time passed
    private void CompletingTask(ref float currentTime, float end, TrellowTask task, Slider slider)
    {
        //calculate bar
        currentTime += 0.25f;
        float value = currentTime / (end);
        value = Mathf.Clamp(value, 0, 1);

        //update slider
        slider.value = value;

        //execute concluded action
        if (value >= 1)
        {
            task.TaskButtonInstance.transform.parent = done;
        }
    }

    private void CompletedTask(ref float currentTime, float end, TrellowTask task, Slider slider, bool loop)
    {
        if (loop)
        {
            //calculate bar
            currentTime -= 0.25f;
            float value = currentTime / end;
            value = Mathf.Clamp(value, 0, 1);
            //update slider
            slider.value = value;
            task.Event.ConfirmEvent();

            if (slider.value <= 0)
            {
                RemoveTask(task);
            }
        }
        else
        {
            task.Event.ConfirmEvent();
            RemoveTask(task);
        }

    }
}
