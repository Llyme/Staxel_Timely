using Microsoft.Xna.Framework.Input;
using Plukit.Base;
using Staxel.Logic;
using StaxelAPI;

namespace Staxel_Timely
{
	public partial class Staxel_Timely
	{
		public override void UniverseUpdateBefore(Universe universe, Timestep step)
		{
			base.UniverseUpdateBefore(universe, step);

			if (!universe.Server)
				if (InputHelper.AnyDown((Keys)StopTimeKey.Value))
					StopTime.Value = !StopTime.Value;
		}
	}
}
