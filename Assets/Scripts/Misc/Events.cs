using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FloatEvent : UnityEvent<float> { }

[Serializable]
public class IntEvent : UnityEvent<int> { }

[Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }