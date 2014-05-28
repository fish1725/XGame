#region

using Assets._Scripts.XGameMVC;

#endregion

namespace Assets._Scripts.XGameSkill {
    public class XGameSkillModel : XGameModel {
        #region Instance Properties

        public float cooldownTime {
            get { return (float)Get("cooldownTime"); }
            set { Set("cooldownTime", value); }
        }

        public float cooldownTimeR {
            get { return (float)Get("cooldownTimeR"); }
            set { Set("cooldownTimeR", value); }
        }

        #endregion
    }
}