using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public interface IDamageable 
{
	public DamageAffinity damageAffinity {get; set;}
	public enum DamageAffinity
	{
		None,
		Earth,
		Wood,
		Metal
	}
	void TakeDamage(int damage);
}
