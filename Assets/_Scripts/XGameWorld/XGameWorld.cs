#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Assets._Scripts.XGameMVC;
using Assets._Scripts.XGameTrigger;
using Assets._Scripts.XGameTrigger.XGameAction;
using Assets._Scripts.XGameTrigger.XGameCondition;
using Assets._Scripts.XGameTrigger.XGameEvent;
using Assets._Scripts.XGameTrigger.XGameExpression;
using UnityEngine;

#endregion

namespace Assets._Scripts.XGameWorld {
    public class XGameWorld : XGame {
        #region Fields

        private XGameTriggerController _triggerController;
        private XGameWorldController _worldController;

        #endregion

        #region Instance Methods

        public override void Setup() {
            base.Setup();
            _worldController = CreateController<XGameWorldController>();
            _triggerController = CreateController<XGameTriggerController>();

            // triggers
            XGameAction action = XGameAction.CreateAction(XGameExpression.Constant(_worldController.characterController),
                "SetTest", new XGameExpression[] {XGameExpression.Constant(100)});
            XGameCondition condition = XGameCondition.BooleanComparison(XGameExpression.Constant(2),
                XGameExpression.Constant(1), XGameConditionOperator.GreaterThan);
            XGameCondition condition2 = XGameCondition.BooleanComparison(XGameExpression.Constant(3),
                XGameExpression.Constant(3), XGameConditionOperator.GreaterThanOrEqual);
            XGameTriggerModel trigger = _triggerController.CreateTrigger("trigger",
                new List<XGameEvent> {new XGameEvent(XGameEventType.Character_Created)},
                new List<XGameCondition> {condition, condition2}, new List<XGameAction> {action});

            Type[] types = {typeof (XGameController)};
            XmlSerializer serializer = new XmlSerializer(typeof(XGameTriggerModel), types);
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, trigger);
            StringReader sr = new StringReader(sw.ToString());
            Debug.Log(sw.ToString());
            sw = new StringWriter();
            XGameTriggerModel t2 = serializer.Deserialize(sr) as XGameTriggerModel;
            if (t2 != null) {
                serializer.Serialize(sw, t2);
                Debug.Log(sw.ToString());

                _triggerController.RegisterTrigger(t2);
            }

            // world
            XGameWorldModel world = _worldController.CreateWorld();

            CreateView<XGameWorldView, XGameWorldModel>(world, gameObject);

            _worldController.InitWorldMap(world);
            _worldController.InitCharacters(world);

            RegisterInstance(world);
        }

        #endregion
    }
}