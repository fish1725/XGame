using UnityEngine;
using System.Collections;

public class XGameUIUtil {

    public static GameObject CreateScrollViewContent(GameObject gameObject) {
        UISprite sprite = NGUITools.AddSprite(gameObject, XGame.Resolve<UIAtlas>(), "TitleBG");
        sprite.SetAnchor(gameObject, 30, 30, -30, -120);

        UIPanel panel = NGUITools.AddChild<UIPanel>(sprite.gameObject);
        panel.SetAnchor(sprite.gameObject, 10, 10, -10, -10);
        panel.clipping = UIDrawCall.Clipping.SoftClip;
        UIScrollView sv = panel.gameObject.AddComponent<UIScrollView>();
        sv.movement = UIScrollView.Movement.Vertical;

        UISprite bar = NGUITools.AddSprite(sprite.gameObject, XGame.Resolve<UIAtlas>(), "Check_Box");
        bar.SetAnchor(sprite.gameObject, -40, 15, -15, -15);
        bar.leftAnchor.relative = 1;
        UISprite foreground = NGUITools.AddSprite(bar.gameObject, XGame.Resolve<UIAtlas>(), "ButtonBG");
        foreground.SetAnchor(bar.gameObject, 5, 5, -5, -5);
        NGUITools.AddWidgetCollider(foreground.gameObject);
        foreground.gameObject.AddComponent<UIButton>();
        UIScrollBar sb = bar.gameObject.AddComponent<UIScrollBar>();
        sb.fillDirection = UIProgressBar.FillDirection.TopToBottom;
        sb.foregroundWidget = foreground;
        sb.backgroundWidget = bar;
        sv.verticalScrollBar = sb;
        sv.showScrollBars = UIScrollView.ShowCondition.Always;

        UITable table = NGUITools.AddChild<UITable>(panel.gameObject);
        table.columns = 1;
        UIWidget wid = table.gameObject.AddComponent<UIWidget>();
        wid.pivot = UIWidget.Pivot.TopLeft;
        wid.leftAnchor.target = panel.transform;
        wid.leftAnchor.absolute = 10;

        return sprite.gameObject;
    }
}
