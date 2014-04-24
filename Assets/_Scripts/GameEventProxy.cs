using UnityEngine;
using System.Collections;

public class GameEventProxy {

	public GameEventProxy() {

	}

	[GameFunctionDescriptionAttribute("A unit {0}")]
	public static string GenericUnitEvent (string gameUnitEvent) {
		return gameUnitEvent.ToString();
	}

}
