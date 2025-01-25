using Sandbox;

public sealed class Tab : Component
{
	[Property] public GameObject Tabclose;
	protected override void OnUpdate()
	{
		//close tab
		bool OnCloseTab = Input.Released("Slot0");
		if (OnCloseTab)
		{
			Sandbox.Services.Achievements.Unlock("destroy_this_thi");
			Tabclose.Destroy();
		}
	}
}
