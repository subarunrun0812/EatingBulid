using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // playerオブジェクトをアタッチする
    public float distance = 15.0f; //注視対象プレイヤーからカメラを話す距離
    private Quaternion vRotation; //カメラの垂直回転(見下ろし回転)
    private Quaternion hRotation; //カメラの水平回転

    void Start()
    {
        //回転の初期化
        vRotation = Quaternion.Euler(45, 0, 0);//垂直回転（X軸を軸とする回転)は、３０度見下ろす回転
        hRotation = Quaternion.identity;//水平回転(Yじくを軸とする回転)は、無回転
        transform.rotation = hRotation * vRotation; //最終的なカメラの回転は、垂直回転してから水平回転する合成回転

        //位置の初期化
        //playerの位置から距離distanceだけ手前に引いた位置を設定します
        transform.position = player.position - transform.rotation * Vector3.forward * distance;
    }

    void LateUpdate()
    {
        //Update関数の後に処理を呼ばれるため、追尾するのに向いている関数
        //カメラの位置（transform.position）の更新
        //player位置から距離distaceだけてまえに引いた位置を設定します
        transform.position = player.position - transform.rotation * Vector3.forward * distance;

        //カメラの位置(transform.position)の更新
        //playerの位置から距離distaceだけ手前に引いた位置を設定します（位置補正版)
        transform.position = player.position + new Vector3(0, 3, 0) - transform.rotation * Vector3.forward * distance;
    }

}
