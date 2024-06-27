using Godot;
using System;



[GlobalClass]
public partial class VelocityComponent : Node2D
{
	[Export]
	private float maxXSpeed;	
	[Export]
	private float maxYSpeed;
	[Export]
	private int speed;
	[Export]
	private float mass;
	[Export]
	private float friction;
	public float speedMultiplier { get; set; } = 1f;
	private float speedModifier = 1f;
	public float targetSpeed => maxXSpeed * speedModifier * speedMultiplier;
	public Vector2 Velocity {get; set;}
	private Vector2 calculatedVelocity;
	private KinematicCollision2D kinematicCollision2D;
	

	public void AccelerateInDirection(Vector2 direction, float accScalar)
	{
		AddForce(direction * accScalar);
	}

	public void SetVelocity(Vector2 newVelocity)
	{
		calculatedVelocity = newVelocity * speed;
	}

	public void AddForce(Vector2 acceleration)
	{
		calculatedVelocity = calculatedVelocity.Clamp(new Vector2(-maxXSpeed, -maxYSpeed), new Vector2(maxXSpeed, maxYSpeed));
		calculatedVelocity += (acceleration * mass) * (float)GetProcessDeltaTime();
	}

	public void Decelerate()
	{	
		var speed = calculatedVelocity.X;
		
		if(Mathf.Max(speed, 0) > 0 | Mathf.Min(speed, 0) < 0)
		{
			calculatedVelocity.X *= friction * (float)GetProcessDeltaTime();
		}
	}

	public void SetMaxSpeed(float newXSpeed, float newYSpeed)
	{
		maxXSpeed = newXSpeed;
		maxYSpeed = newYSpeed;
	}

	public void SetSpeedModifier(float newModifier)
	{
		speedModifier = newModifier;
	}

	
	public void Move(CharacterBody2D characterBody2D)
	{
		Velocity = calculatedVelocity.Clamp(new Vector2(-maxXSpeed, -maxYSpeed), new Vector2(maxXSpeed, maxYSpeed));
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
	}

	public void CollisionCheck(KinematicCollision2D collisionData)
	{
		var collisionVelocity = collisionData.GetColliderVelocity();
		calculatedVelocity += new Vector2(collisionVelocity.X, 0);	
	}

	public Vector2 GetVelocity()
	{
		return Velocity;
	}
	
}
