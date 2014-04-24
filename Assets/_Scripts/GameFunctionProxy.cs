
public class GameFunctionProxy {
	
	public GameFunctionProxy () {
	}

	[GameFunctionDescriptionAttribute("whether {0} is greater than {1}")]
	public static bool GetBool (int i, int j) {
		return i > j;
	}

	[GameFunctionDescriptionAttribute("whether {0} is less than 0")]
	public static bool GetAnotherBool (int i) {
		return i < 0;
	}
	[GameFunctionDescriptionAttribute("Get {0} plus one.")]
	public static int PlusOne (int i) {
		return i+1;
	}
}