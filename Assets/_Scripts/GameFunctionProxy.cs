
using UnityEngine;
public class GameFunctionProxy {
	
	public GameFunctionProxy () {
	}

	[XGameFunctionDescriptionAttribute("whether {0} is greater than {1}")]
	public static bool GetBool (int i, int j) {
		return i > j;
	}

	[XGameFunctionDescriptionAttribute("whether {0} is less than 0")]
	public static bool GetAnotherBool (int i) {
		return i < 0;
	}
	[XGameFunctionDescriptionAttribute("Get {0} plus one.")]
	public static int PlusOne (int i) {
		return i+1;
	}

    [XGameFunctionDescriptionAttribute("Action test...")]
    public static void ActionTest(int a) {
        Debug.Log("Action test: " + a);
    }
}