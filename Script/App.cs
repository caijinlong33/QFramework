﻿using UnityEngine;
using System.Collections;
using QFramework;

/// <summary>
/// 全局唯一继承于MonoBehaviour的单例类，保证其他公共模块都以App的生命周期为准
/// </summary>
public class App : QMonoSingleton<App>
{
    public delegate void LifeCircleCallback();

    public LifeCircleCallback onUpdate = null;
	public LifeCircleCallback onFixedUpdate = null;
	public LifeCircleCallback onLatedUpdate = null;
    public LifeCircleCallback onGUI = null;
    public LifeCircleCallback onDestroy = null;
    public LifeCircleCallback onApplicationQuit = null;

    void Awake()
    {
		// 确保不被销毁
        DontDestroyOnLoad(gameObject);

        instance = this;

		// 进入欢迎界面
		Application.targetFrameRate = 60;
    }

    IEnumerator Start()
    {
		// 这个GameManager需要自己实现
		yield return StartCoroutine (GameManager.Instance ().OnStart ());
    }

    void Update()
    {
        if (this.onUpdate != null)
            this.onUpdate();
    }

	void FixedUpdate()
	{
		if (this.onFixedUpdate != null)
			this.onFixedUpdate ();

		if(Time.frameCount % 10 == 0) { System.GC.Collect(); }
	}

	void LatedUpdate()
	{
		if (this.onLatedUpdate != null)
			this.onLatedUpdate ();
	}

    void OnGUI()
    {
        if (this.onGUI != null)
            this.onGUI();
    }

     void OnDestroy()
    {
        if (this.onDestroy != null)
            this.onDestroy();
    }

    void OnApplicationQuit()
    {
        if (this.onApplicationQuit != null)
            this.onApplicationQuit();
    }
}
