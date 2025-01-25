using Sandbox;

public sealed class MouseOn : Component
{
	[Property] public bool isMouseOn;
	protected override void OnUpdate()
	{
        switch (isMouseOn)
        {
			case true:
				Mouse.Visible = true;
				break;
			case false:
				Mouse.Visible = false;
				break;
            default:
                break;
        }
	}
}
