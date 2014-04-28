using UnityEngine;
using System.Collections;

public class EnterWorldEvent : XGameEvent {

	public EnterWorldEvent() {
		_type = XGameEventType.Enter_World;
		_message = "Enter World.";
	}

}
