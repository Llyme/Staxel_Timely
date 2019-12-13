using Harmony;
using Microsoft.Xna.Framework.Input;
using Staxel.Core;
using StaxelAPI;
using System.Reflection;

namespace Staxel_Timely
{
	public partial class Staxel_Timely : ModPlugin
	{
		public const string GUID = "com.staxel.mod.nyan.timely";

		const string SECTION_GENERAL = "General";

		public static readonly double defaultSecondsPerIngameDay = Constants.SecondsPerIngameDay;

		public static INI.Entry.Float SecondsPerIngameDay;
		public static INI.Entry.Bool StopTime;
		public static INI.Entry.Int StopTimeKey;

		public override void GameContextInitializeInit()
		{
			SecondsPerIngameDay = ini.Float(SECTION_GENERAL, "SecondsPerIngameGame", (float)defaultSecondsPerIngameDay);
			StopTime = ini.Bool(SECTION_GENERAL, "StopTime", false);
			StopTimeKey = ini.Int(SECTION_GENERAL, "StopTimeKey", (int)Keys.OemTilde);

			ini.OnUpdate += OnUpdate;
			OnUpdate();

			HarmonyInstance.Create(GUID).PatchAll(Assembly.GetExecutingAssembly());
		}

		void OnUpdate()
		{
			float _SecondsPerIngameDay = SecondsPerIngameDay.Value;

			if (_SecondsPerIngameDay == Constants.SecondsPerIngameDay)
				return;

			new Traverse(typeof(Constants))
				.Field("SecondsPerIngameDay")
				.SetValue(
					_SecondsPerIngameDay > 0 ?
						_SecondsPerIngameDay :
						defaultSecondsPerIngameDay
				);
		}
	}
}
