using Sandbox;
using Sandbox.Citizen;
using Sandbox.UI;
using Sandbox.UI.Construct;

public sealed class WeaponHands : Component
{
	[Property]
	[Category( "Gun Components" )]
	private float punchCooldown { get; set; }
	[Property]
	[Category( "Gun Components" )]
	private float punchDamage { get; set; }
	[Property]
	[Category( "Gun Components" )]
	private GameObject punchOrigin;
	[Property]
	[Category( "Gun Components" )]
	private float PunchRange;
	private Player player { get; set; }
	[Property] 
	public void Attack(Player PL)
	{
		player = PL;
		if ( player.Animator != null )
		{
			player.Animator.HoldType = CitizenAnimationHelper.HoldTypes.Punch;
			player.Animator.Target.Set( "b_attack", true );
		}
		var punchTrace = Scene.Trace
			.FromTo( player.EyeWorldPosition, player.EyeWorldPosition + player.EyeAngles.Forward * PunchRange )
			.Size( 10f )
			.WithoutTags( "player" )
			.IgnoreGameObjectHierarchy( GameObject )
			.Run();
		if ( punchTrace.Hit )
			if ( punchTrace.GameObject.Components.TryGet<UnitInfo>( out var unitInfo ) )
				unitInfo.Damage( punchDamage );
		player._lastPunch = 0f;
	}


}
