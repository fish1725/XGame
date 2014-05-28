#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameUnit;

#endregion

namespace Assets._Scripts.XGameTrigger {
    public class XGameTriggerModel : XGameModel {
        #region Fields

        private Func<bool> _gameConditionCompiled;

        #endregion

        #region Instance Properties

        public List<XGameAction.XGameAction> gameActions {
            get { return Get("gameActions") as List<XGameAction.XGameAction>; }
            set { Set("gameActions", value); }
        }

        public List<XGameCondition.XGameCondition> gameConditions {
            get { return Get("gameConditions") as List<XGameCondition.XGameCondition>; }
            set {
                Set("gameConditions", value);
                CompileGameCondition();
            }
        }

        public List<XGameEvent> gameEvents {
            get { return Get("gameEvents") as List<XGameEvent>; }
            set { Set("gameEvents", value); }
        }

        public XGameTriggerModel testint {
            get { return Get("testint") as XGameTriggerModel; }
            set { Set("testint", value); }
        }

        #endregion

        #region Instance Methods

        public void Execute() {
            if (_gameConditionCompiled == null) {
                CompileGameCondition();
            }
            if (_gameConditionCompiled != null && _gameConditionCompiled()) {
                foreach (XGameAction.XGameAction action in gameActions) {
                    action.Execute();
                }
            }
        }

        private void CompileGameCondition() {
            Expression re = Expression.Constant(true);
            for (int i = 0; i < gameConditions.Count; i++) {
                re = Expression.And(re, gameConditions[i].result);
            }
            _gameConditionCompiled = Expression.Lambda<Func<bool>>(re).Compile();
        }

        #endregion
    }
}