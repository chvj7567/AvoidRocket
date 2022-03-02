using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
	protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
	public abstract void Init();

	private void Start()
	{
		Init();
	}

	// type 열거형에 해당하는 이름을 가진 T 타입 컴포넌트를 연결한다.
	protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
		string[] names = Enum.GetNames(type);
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
		_objects.Add(typeof(T), objects);

		for (int i = 0; i < names.Length; i++)
		{
			if (typeof(T) == typeof(GameObject))
				objects[i] = Util.FindChild(gameObject, names[i], true);
			else
				objects[i] = Util.FindChild<T>(gameObject, names[i], true);

			if (objects[i] == null)
				Debug.Log($"Failed To Bind({names[i]})");
		}
	}

	// idx에 해당하는 T 타입 컴포넌트를 가져온다.
	protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		if (_objects.TryGetValue(typeof(T), out objects) == false)
			return null;

		return objects[idx] as T;
	}

	// idx에 해당하는 게임오브젝트를 가져온다.
	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }

	// idx에 해당하는 텍스트 컴포넌트를 가져온다.
	protected Text GetText(int idx) { return Get<Text>(idx); }

	// idx에 해당하는 이미지 컴포넌트를 가져온다.
	protected Image GetImage(int idx) { return Get<Image>(idx); }
	// idx에 해당하는 버튼 컴포넌트를 가져온다.
	protected Button GetButton(int idx) { return Get<Button>(idx); }

	// go에 type에 해당하는 이벤트를 연결 시킨다.
	public static void BindEvent(GameObject go, Action action, Define.UIEvent type = Define.UIEvent.Update)
	{
		UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.Update:
				evt.OnUpdateHandler -= action;
				evt.OnUpdateHandler += action;
				break;
		}
	}

	// go에 type에 해당하는 이벤트를 연결 시킨다.
	public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type)
	{
		UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.BeginDrag:
				evt.OnBeginDragHandler -= action;
				evt.OnBeginDragHandler += action;
				break;
			case Define.UIEvent.Drag:
				evt.OnDragHandler -= action;
				evt.OnDragHandler += action;
				break;
			case Define.UIEvent.EndDrag:
				evt.OnEndDragHandler -= action;
				evt.OnEndDragHandler += action;
				break;
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action;
				evt.OnClickHandler += action;
				break;
		}
	}

	// go에 type에 해당하는 이벤트를 연결을 해제한다.
	public static void NoBindEvent(GameObject go, Action action, Define.UIEvent type = Define.UIEvent.Update)
	{
		UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.Update:
				evt.OnUpdateHandler -= action;
				break;
		}
	}

	// go에 type에 해당하는 이벤트를 연결을 해제한다.
	public static void NoBindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type)
	{
		UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.BeginDrag:
				evt.OnBeginDragHandler -= action;
				break;
			case Define.UIEvent.Drag:
				evt.OnDragHandler -= action;
				break;
			case Define.UIEvent.EndDrag:
				evt.OnEndDragHandler -= action;
				break;
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action;
				break;
		}
	}
}