using System;

public class Constants {
	public static readonly float PLAYER_SPEED = 10;  //units per second
	public static readonly float MAX_PLAYER_TARGET_SPEED = 15;  //units per second
	public static float PLAYER_TARGET_SPEED = MAX_PLAYER_TARGET_SPEED;  //units per second; slows as it moves further from player
	public static readonly float PLAYER_ROTATE_SPEED = 90;  //degrees per second
	public static readonly float FIRE_RATE = 20;  //shots per second
	public static readonly string PLAYER_TAG = "Player";
	public static readonly string ENEMY_TAG = "Enemy";
	public static readonly string ORB_TAG = "Orb";
	public static readonly string ORB_CONTROLLER = "OrbController";
	public static readonly string HOME_TAG = "HomeBase";
	public static readonly string FIRE_MATERIAL = "Orange";
	public static readonly string WATER_MATERIAL = "Blue";
	public static readonly string LEAF_MATERIAL = "Green";
	public static readonly string WIN_TEXT = "Way to go... loser ('R')";
	public static readonly string LOSE_TEXT = "You suck... loser ('R')";

	public static readonly Random random = new Random();
	public static T RandomEnumValue<T>() where T : IConvertible {
		Array values = Enum.GetValues(typeof(T));
		return (T)values.GetValue(random.Next(values.Length));
	}
}
