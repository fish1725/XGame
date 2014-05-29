namespace Assets._Scripts.XGameTrigger.XGameEvent.XGameEvents {
    public class EnterWorldEvent : XGameEvent {
        #region C'tors

        public EnterWorldEvent() {
            _type = XGameEventType.Enter_World;
            _message = "Enter World.";
        }

        #endregion
    }
}