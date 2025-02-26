using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerDetector : MonoBehaviour
{
    //IsTriggerがOnで他のColliderと重なっているときは、このメソッドが常にコールされる
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();
    private void OnTriggerStay(Collider other)
    {
        //onTriggerStayで指定された処理を実行する
        onTriggerStay.Invoke(other);
    }

    //UnityEventを継承したクラスに[Serializable]属性を付与することでInspectorウィンドウ上に表示できるようになる
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>//collider = trigger or collision
    {

    }
}
