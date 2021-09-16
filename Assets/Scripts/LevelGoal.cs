using Player;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
	[SerializeField] private BuildScene _levelToLoad;

	private void OnTriggerEnter(Collider other)
	{
		PlayerController player = other.GetComponentInParent<PlayerController>();
		if (player == null)
			return;

		SceneHandler.Instance.LoadScene(_levelToLoad);
	}
}
