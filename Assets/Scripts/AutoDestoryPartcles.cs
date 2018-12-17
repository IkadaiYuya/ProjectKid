using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestoryPartcles : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

		List<ParticleSystem> partcleSystems = new List<ParticleSystem>();

		//ゲームオブジェクトにアタッチされているParticleSystemを追加.
		partcleSystems.Add(GetComponent<ParticleSystem>());
		partcleSystems.AddRange(GetComponentsInChildren<ParticleSystem>());

		float LongestDuration = 0;

		//Listに入っている分、Durationを比較して、その中の一番大きい数を代入
		foreach (ParticleSystem partcle in partcleSystems) {
			if (partcle.main.duration > LongestDuration) {
				LongestDuration = partcle.main.duration;
			}
		}
		//duration（＝存続期間）が経過したらオブジェクトを削除
		Destroy(gameObject, LongestDuration);

	}
}