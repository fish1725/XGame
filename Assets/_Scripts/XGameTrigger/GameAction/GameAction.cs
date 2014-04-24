
public class GameAction  {

	public string Name { get; set; }

	[System.Xml.Serialization.XmlIgnore]
	public System.Type Command { get; set; }

	public void Execute (XGameEvent e) {
		
	}

}
