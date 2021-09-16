using UnityEngine;

namespace MyUtilities
{
	public enum WeaponModifierType { Damage, AttackRate };
	public static class AudioConverter
	{
		/// <summary>
		/// Provides functions for converting floats to decibels, and decibels to floats.
		/// </summary>
		/*
		 * fixed problem of volume slider linearly changing log values (dB) (which made sound barely noticeable at halfway point)
		 * volume locked to 0.0001-1
		 * kudos: https://gamedevbeginner.com/the-right-way-to-make-a-volume-slider-in-unity-using-logarithmic-conversion/
		*/
		public static float ConvertFloatToDB(float value) => 20f * Mathf.Log10(value);
		public static float ConvertDBToFloat(float value) => Mathf.Pow(10, value / 20f) ;
	}
	
}

