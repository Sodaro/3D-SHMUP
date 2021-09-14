using MyUtilities;
public interface IWeapon
{
	public void ApplyModifier(float value, WeaponModifierType modifierType);
	public void RemoveModifier(WeaponModifierType modifierType);

	public bool IsShooting { get; }
	//public void StartShooting(Action<int> onHitCallBack);
	public void StartShooting();
	public void StopShooting();
}
