using UnityEngine;
using System.Collections;

public class XGameUIUtil {

    public static GameObject CreateImageButton(GameObject gameObject, string spriteName) {
        UISprite sprite = NGUITools.AddSprite(gameObject, XGame.Resolve<UIAtlas>(), spriteName);
        NGUITools.AddWidgetCollider(sprite.gameObject);
        sprite.gameObject.AddComponent<UIButton>();
        UIButtonScale bs = sprite.gameObject.AddComponent<UIButtonScale>();
        bs.hover = Vector3.one;
        bs.pressed = new Vector3(0.9f, 0.9f, 0.9f);
        return sprite.gameObject;
    }

    public static GameObject CreateLabel(GameObject gameObject, string content, int fontSize = 32) {
        UILabel label = NGUITools.AddChild<UILabel>(gameObject);
        label.trueTypeFont = XGame.Resolve<Font>();
        label.text = content;
        label.fontSize = fontSize;
        label.color = Color.black;
        label.overflowMethod = UILabel.Overflow.ResizeFreely;
        label.effectStyle = UILabel.Effect.Shadow;
        label.effectColor = Color.white;
        return label.gameObject;
    }

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
