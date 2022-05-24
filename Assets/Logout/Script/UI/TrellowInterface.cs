using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrellowInterface : MonoBehaviour
{

    List<Event> tasks = new List<Event>();
    Dictionary<Event, TaskButton> taskButtonInstances = new Dictionary<Event, TaskButton>();
    [SerializeField] private TaskButton buttonRef;
    [SerializeField] private Transform content;
    [SerializeField] private Transform todo;
    [SerializeField] private Transform doing;
    [SerializeField] private Transform done;
    [SerializeField] private MG_Adoption mG_AdoptionRef;
    private Action OnUpdate;

    private void Start()
    {
        Donation donation = new Donation(100);
        Adoption adoption = new Adoption();
        AddNewTask(donation);
        AddNewTask(adoption);
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
        tasks.Add(task);
        UpdateTasks();
    }

    private void RemoveTask(Event task)
    {
        //remove task from list
        tasks.Remove(task);
        //destroy button
        if (taskButtonInstances.ContainsKey(task))
        {
            task.DeclineEvent();
            TaskButton instance = taskButtonInstances[task];
            GameObject.Destroy(instance.gameObject);
            taskButtonInstances.Remove(task);
        }
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void UpdateTasks()
    {
        //each item in task
        //instantiate new button 
        foreach (Event task in tasks)
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
                        float reftimer = 0;
                        //register action to update method
                        if (task is Adoption)
                        {
                            OnUpdate = () => { CompletingTask(ref reftimer, task.TimeRange.y, task, taskButton.taskSlider, () => { Instantiate(mG_AdoptionRef); ConfirmEvent(task, taskButton); }); };
                        }
                        else
                            OnUpdate = () => { CompletingTask(ref reftimer, task.TimeRange.y, task, taskButton.taskSlider, () => { ConfirmEvent(task, taskButton); }); };
                    }
                });
            }
        }
    }

    private void CompletingTask(ref float begin, float end, Event task, Slider slider, Action OnConcluded)
    {
        //calculate bar
        begin += Time.deltaTime;
        float value = begin / (end);
        value = Mathf.Clamp(value, 0, 1);

        //update slider
        slider.value = value;

        //execute concluded action
        if (value >= 1)
        {
            OnConcluded?.Invoke();
            OnUpdate = () => { ReceiveingReward(end, slider, () => { RemoveTask(task); OnUpdate = null; }); };
        }
    }


    private void ReceiveingReward(float end, Slider slider, Action OnConcluded)
    {
        //calculate bar
        slider.value -= Time.deltaTime / end;

        if (slider.value <= 0)
        {
            OnConcluded?.Invoke();
            OnUpdate = null;
        }
    }

    private void ConfirmEvent(Event evenRef, TaskButton button)
    {
        button.transform.parent = done;
        evenRef.ConfirmEvent();
    }
}
