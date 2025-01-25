using Sandbox;
using Sandbox.UI;

public sealed class Fixedplayer : Component
{
	[Property] public GameObject cam;
	[Property] public GameObject tab;
	[Property] public GameObject devmenu;
	[Property] public GameObject Debil;
	[Property] public SceneFile MenuMap;
	[Property] public bool PointMode;

	public string isFixedUpdated = "Fixed Update";
	public float speedmove = 1f;
	public float FOV = 60f;

	private int sexy, sexx, sexz;
	private Vector3 Point;

	private int pereishere, expandyorvisi, nofpsquestion;

	protected override void OnStart()
    {
		if (!IsProxy)
		{
			cam.Enabled = true;
		}

		if (!IsProxy)
		{
			tab.Enabled = true;
		}

		if (!IsProxy)
		{
			devmenu.Enabled = true;
		}

		PointMode = false;

		pereishere = 0;
	}

	protected override void OnUpdate()
    {
		if (IsProxy)
			return;

		//ZOOM

		var Camera = cam.Components.Get<CameraComponent>();
		if (Input.Down("FOV up"))
		{
			FOV = FOV - 1;
		}

		if (Input.Down("FOV down"))
		{
			FOV = FOV + 1;
		}

		Camera.FieldOfView = FOV;

		//swith metode
		if(Input.Released("Switch Function"))
        {
			if (nofpsquestion == 0)
            {
				Sandbox.Services.Achievements.Unlock("no_fps_question");
				nofpsquestion = 1;
			}

			switch (isFixedUpdated)
            {
				case "Fixed Update":
					isFixedUpdated = "Update";
					break;
				case "Update":
					isFixedUpdated = "Fixed Update";
					break;
                default:
                    break;
            }
        }

		if (isFixedUpdated == "Update")
		{
			Tick();
		}
	}

	protected override void OnFixedUpdate()
	{
		if (IsProxy)
			return;
		if(isFixedUpdated == "Fixed Update")
        {
			Tick();
        }
		
	}
	public void Tick()
    {
		// tick rotate

		if (PointMode != true)
		{
			Transform.Rotation = new Angles(Transform.Rotation.x + sexy, Transform.Rotation.x + sexx, Transform.Rotation.x + sexz);
		}

		else
        {
			Transform.Rotation = Rotation.LookAt(Point - Transform.Position);
		}

		//Transform.Rotation = new Angles(Transform.Rotation.x + sexy, Transform.Rotation.x + sexx, Transform.Rotation.x + sexz);

		//DEV MENU
		if (Input.Released("Slot9"))
		{
			devmenu.Enabled = !devmenu.Enabled;
		}

		//ROTATE
		if (Input.Down("Forward"))
		{
			sexy = sexy - 1;
		}

		if (Input.Down("Backward"))
		{
			sexy = sexy + 1;
		}

		if (Input.Down("Left"))
		{
			sexx = sexx + 1;
		}

		if (Input.Down("Right"))
		{
			sexx = sexx - 1;
		}

		if (Input.Down("Rotate Left"))
		{
			sexz = sexz + 1;
		}

		if (Input.Down("Rotate Right"))
		{
			sexz = sexz - 1;
		}

		//MOVE LINE
		if (Input.Down("Line forward"))
		{
			Transform.Position += new Vector3(5 * speedmove, 0, 0);
		}

		if (Input.Down("Line backward"))
		{
			Transform.Position += new Vector3(-5 * speedmove, 0, 0);
		}

		if (Input.Down("Line right"))
		{
			Transform.Position += new Vector3(0, -5 * speedmove, 0);
		}

		if (Input.Down("Line left"))
		{
			Transform.Position += new Vector3(0, 5 * speedmove, 0);
		}

		if (Input.Down("up"))
		{
			Transform.Position += new Vector3(0, 0, 5 * speedmove);
		}

		if (Input.Down("down"))
		{
			Transform.Position += new Vector3(0, 0, -5 * speedmove);
		}

		//Normal move

		if (Input.Down("NORMAL forward"))
		{
			Transform.Position += new Vector3(Transform.Rotation.Forward * 5 * speedmove);
		}

		if (Input.Down("NORMAL backward"))
		{
			Transform.Position += new Vector3(-Transform.Rotation.Forward * 5 * speedmove);
		}

		//move speed

		if (Input.Down("Speed up"))
		{
			speedmove = speedmove + 0.1f;
		}

		if (Input.Down("Speed down"))
		{
			speedmove = speedmove - 0.1f;
		}

		switch (speedmove)
		{
			case < 0:
				speedmove = 0;
				break;
			default:
				break;
		}

		//exit

		if (Input.Released("Escape to menu"))
		{
			Scene.Load(MenuMap);
		}

		//RESET
		if (Input.Down("Reload"))
		{
			speedmove = 1f;
			FOV = 60;
			sexx = 0;
			sexy = 0;
			sexz = 0;
			PointMode = false;
		}

		//POINT MODE

		var startPos = cam.Transform.Position;
		var dir = cam.Transform.Rotation.Forward;
		var maxDist = 2000;

		if(Input.Down("Set Point"))
        {
			var tr = Scene.Trace.Ray(startPos, startPos + (dir * maxDist)).Run();
			if (tr.Hit)
            {
				Point = new Vector2(tr.EndPosition + tr.Normal * 10);
			}
		}

		if(Input.Released("Switch Point Mode"))
        {
			PointMode = !PointMode;
		}

		/*
		if(Input.Down("Set Point"))
        {
			var result = Scene.Trace.Ray(startPos, startPos + (dir * maxDist))
				.Run();
			Log.Warning("Not yay");
			if (result.Hit)
			{
				var Point = new Vector2 (result.EndPosition + result.Normal);
				Log.Warning("yay");
			}
		}
		*/


		//ACHIEVEMENTS

		if(sexx > 360 && pereishere == 0  || sexx < -360 && pereishere == 0  || sexy > 360 && pereishere == 0 || sexy < -360 && pereishere == 0)
        {
			Sandbox.Services.Achievements.Unlock("pereve_is_here");
			pereishere = 1;
		}

		if (FOV < 0 && expandyorvisi == 0 || FOV > 180 && expandyorvisi == 0)
		{
			Sandbox.Services.Achievements.Unlock("expand_your_visi");
			expandyorvisi = 1;
		}
	}
}
