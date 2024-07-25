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

	public static Resource LoadStats(string type)
	{
		switch(type)
		{
			case "Earth":
			{
				var stats = GD.Load<Stats>("res://resources/stats/EarthStats.tres");
				return stats.Duplicate();
			}
			case "Wood":
			{
				var stats = GD.Load<Stats>("res://resources/stats/WoodStats.tres");
				return stats.Duplicate();
			}
			case "Metal":
			{
				var stats = GD.Load<Stats>("res://resources/stats/MetalStats.tres");
				return stats.Duplicate();
			}
		}

		return null;
	}

    public static Resource LoadOpposingStats(string type)
    {
	    switch(type)
	    {
			case "Earth":
			{
				var stats = GD.Load<Stats>("res://resources/stats/WoodStats.tres");
				return stats.Duplicate();
			}
			case "Wood":
			{
				var stats = GD.Load<Stats>("res://resources/stats/MetalStats.tres");
				return stats.Duplicate();
			}
			case "Metal":
			{
				var stats = GD.Load<Stats>("res://resources/stats/EarthStats.tres");
				return stats.Duplicate();
			}
		}

		return null;
    }

}
