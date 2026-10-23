using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;


public sealed class Scheduler : MonoBehaviour
{
    private static readonly LinkedList<Action<float>> updateTasks = new LinkedList<Action<float>>();
    private static readonly LinkedList<Action> fixedUpdateTasks = new LinkedList<Action>();
    private static readonly LinkedList<Action<float>> lateUpdateTasks = new LinkedList<Action<float>>();
    private static readonly List<Action> executing = new List<Action>();
    private static readonly List<Action> postTasks = new List<Action>();

    // next frame task
    private static List<Action> nextFrameTasks1 = new List<Action>();
    private static List<Action> nextFrameTasks2 = new List<Action>();
    private static int lastUpdateFrame;

    private static readonly LinkedList<TimerTask> delayTasks = new LinkedList<TimerTask>();
    private static readonly LinkedList<TimerTask> cycleTasks = new LinkedList<TimerTask>();

    private static Scheduler instance;

    public static LinkedListNode<Action<float>> ListenUpdate(Action<float> action)
    {
        return updateTasks.AddLast(action);
    }

    public static void UnlistenUpdate(LinkedListNode<Action<float>> handle)
    {
        if (handle != null && updateTasks.Find(handle.Value) == handle)
            updateTasks.Remove(handle);
        else
            Debug.LogError($"handle not belong to update. handle is null=[{handle == null}]");
    }

    public static LinkedListNode<Action> ListenFixedUpdate(Action action)
    {
        return fixedUpdateTasks.AddLast(action);
    }

    public static void UnlistenFixedUpdate(LinkedListNode<Action> handle)
    {
        if (handle != null && fixedUpdateTasks.Find(handle.Value) == handle)
            fixedUpdateTasks.Remove(handle);
        else
            Debug.LogError("handle not belong to fixupdate");
    }


    public static LinkedListNode<Action<float>> ListenLateUpdate(Action<float> action)
    {
        return lateUpdateTasks.AddLast(action);
    }

    public static void UnlistenLateUpdate(LinkedListNode<Action<float>> handle)
    {
        if (handle != null && lateUpdateTasks.Find(handle.Value) == handle)
            lateUpdateTasks.Remove(handle);
        else
            Debug.LogError("handle not belong to lateupdate");
    }

    public static void PostTask(Action task)
    {
        lock (postTasks)
        {
            postTasks.Add(task);
        }
    }

    public static Coroutine RunCoroutine(IEnumerator coroutine)
    {
        return GetInstance().StartCoroutine(coroutine);
    }

    public static void Delay(Action task)
    {
        if (lastUpdateFrame != Time.frameCount)
        {
            nextFrameTasks2.Add(task);
        }
        else
        {
            nextFrameTasks1.Add(task);
        }
    }

    public static LinkedListNode<TimerTask> Delay(float time, Action task)
    {

        var timerTask = new TimerTask(
            Time.realtimeSinceStartup + time, task);
        return delayTasks.AddLast(timerTask);
    }

    public static void CancelDelay(LinkedListNode<TimerTask> handler)
    {
        if (handler != null && delayTasks.Find(handler.Value) == handler)
        {
            handler.Value.Destroy();
            delayTasks.Remove(handler);
        }
        else
        {
            Debug.LogError("handle not belong to delayTask");
        }
    }

    public static LinkedListNode<TimerTask> Cycle(float cycle, Action task)
    {
        return cycleTasks.AddLast(new TimerTask(
            Time.realtimeSinceStartup + cycle, cycle, task));
    }

    public static void CancelCycle(LinkedListNode<TimerTask> node)
    {
        if (node != null && cycleTasks.Find(node.Value) == node)
        {
            node.Value.Destroy();
            cycleTasks.Remove(node);
        }
        else
        {
            Debug.LogError("handle not belong to cycleTask");
        }
    }

    [RuntimeInitializeOnLoadMethod]
    private static Scheduler GetInstance()
    {
        if (instance == null && Application.isPlaying)
        {
            var go = new GameObject("WorldScheduler", typeof(Scheduler));
            GameObject.DontDestroyOnLoad(go);
            instance = go.GetComponent<Scheduler>();
        }
        return instance;
    }

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }

    static void ClearLinkList<T>(LinkedList<T> list) where T : class
    {
        var first = list.First;
        while (first != null)
        {
            first.Value = null;
            first = first.Next;
        }
        list.Clear();
    }

    static void ClearTimerTaskLinkList(LinkedList<TimerTask> list)
    {
        while (list.First != null)
        {
            list.First.Value.Destroy();
            list.RemoveFirst();
        }
    }
    public static void OnDestroy()
    {
        Debug.Log("Destroying Scheduler");
        ClearLinkList(updateTasks);
        ClearLinkList(fixedUpdateTasks);
        ClearLinkList(lateUpdateTasks);
        executing.Clear();
        postTasks.Clear();
        nextFrameTasks1.Clear();
        nextFrameTasks2.Clear();
        ClearTimerTaskLinkList(delayTasks);
        ClearTimerTaskLinkList(cycleTasks);
        instance = null;
    }

    private void Update()
    {
        Execute(updateTasks);
        lock (postTasks)
        {
            if (postTasks.Count > 0)
            {
                for (int i = 0; i < postTasks.Count; ++i)
                {
                    executing.Add(postTasks[i]);
                }
                postTasks.Clear();
            }
        }

        lastUpdateFrame = Time.frameCount;
        if (nextFrameTasks1.Count > 0)
        {
            for (int i = 0; i < nextFrameTasks1.Count; ++i)
            {
                executing.Add(nextFrameTasks1[i]);
            }

            nextFrameTasks1.Clear();
        }

        var tmp = nextFrameTasks1;
        nextFrameTasks1 = nextFrameTasks2;
        nextFrameTasks2 = tmp;

        var now = Time.realtimeSinceStartup;
        if (delayTasks.Count > 0)
        {
            var cur = delayTasks.First;
            while (cur != null)
            {
                var task = cur.Value;
                if (now >= task.Time)
                {
                    executing.Add(task.Task);
                    task.Destroy();
                    var del = cur;
                    cur = del.Next;
                    delayTasks.Remove(del);
                    continue;
                }
                cur = cur.Next;
            }
        }

        if (cycleTasks.Count > 0)
        {
            var cur = cycleTasks.First;
            while (cur != null)
            {
                if (now >= cur.Value.Time)
                {
                    executing.Add(cur.Value.Task);
                    cur.Value.Time = now + cur.Value.cycle;
                }
                cur = cur.Next;
            }
        }

        for (int i = 0; i < executing.Count; ++i)
        {
            var task = executing[i];
            try
            {
                task();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        executing.Clear();
    }

    private void FixedUpdate()
    {
        Execute(fixedUpdateTasks);
    }

    private void LateUpdate()
    {
        Execute(lateUpdateTasks);
    }

    void Execute(LinkedList<Action<float>> list)
    {
        float deltaTime = Time.deltaTime;
        var iterator = list.First;
        while (iterator != null)
        {
            var next = iterator.Next;
            var value = iterator.Value;

            try
            {
                value(deltaTime);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                list.Remove(iterator);
            }
            iterator = next;
        }
    }

    void Execute(LinkedList<Action> list)
    {
        var iterator = list.First;
        while (iterator != null)
        {
            var next = iterator.Next;
            var value = iterator.Value;

            try
            {
                value();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                list.Remove(iterator);
            }
            iterator = next;
        }
    }

    public class TimerTask
    {
        public float cycle;
        public float Time;
        public Action Task;
        public TimerTask(float time, float cycle, Action task)
        {
            this.Time = time;
            this.Task = task;
            this.cycle = cycle;
        }

        public TimerTask(float time, Action task)
        {
            this.Time = time;
            this.Task = task;
            this.cycle = 0;
        }

        public void Destroy()
        {
            this.Task = null;
        }

        public override string ToString()
        {
            return $"cycle={cycle} Time={Time} ";
        }
    }
}
