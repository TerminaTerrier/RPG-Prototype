using Godot;
using System;

public partial class TypeLoader : Node
{
	
	public static Moveset LoadMoveset(string type)
	{
		switch(type)
		{
			case "Earth":
			{
				return GD.Load<Moveset>("res://resources/moves/earth/EarthMoveset.tres");
			}
			case "Wood":
			{
				return GD.Load<Moveset>("res://resources/moves/wood/WoodMoveset.tres");
			}
			case "Metal":
			{
				return GD.Load<Moveset>("res://resources/moves/metal/MetalMoveset.tres");
			}
		}

		return null;
	}

	public static Moveset LoadOpposingMoveset(string type)
	{
		switch(type)
		{
			case "Earth":
			{
				return GD.Load<Moveset>("res://resources/moves/wood/WoodMoveset.tres");
			}
			case "Wood":
			{
				return GD.Load<Moveset>("res://resources/moves/metal/MetalMoveset.tres");
			}
			case "Metal":
			{
				return GD.Load<Moveset>("res://resources/moves/earth/EarthMoveset.tres");
			}
		}

		return null;
	}

}
