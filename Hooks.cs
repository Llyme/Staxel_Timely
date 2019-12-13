using Harmony;
using Plukit.Base;
using Staxel.Sky;

namespace Staxel_Timely
{
	[HarmonyPatch(typeof(DayNightCycle), "Update")]
	public class Patch_DayNightCycle_Update
	{
		[HarmonyPrefix]
		public static bool Prefix(DayNightCycle __instance,
								  Timestep step,
								  ref Timestep ____prevStep,
								  ref Timestep ____epoch,
								  ref double ____epochPhase,
								  ref double ____calendarEpochPhase,
								  ref double ____calendarDay,
								  ref double ____calendarPhase)
		{
			if (__instance.TimeStopped() ||
				__instance.GamePaused() ||
				__instance.StopTimeRequests > 0)
				return true;

			if (Staxel_Timely.StopTime.Value ||
				Staxel_Timely.SecondsPerIngameDay.Value <= 0)
			{
				____epochPhase = __instance.Day + __instance.Phase;
				____calendarEpochPhase = ____calendarDay + ____calendarPhase;
				____epoch = ____prevStep = step;
				return false;
			}

			return true;
		}
	}
}
