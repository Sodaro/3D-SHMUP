using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolableObject
{
	public event EventHandler onGameObjectDisabled;
}
