using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//オブジェクトにNavMeshAgentコンポーネントを設置
[RequireComponent(typeof(NavMeshAgent))]

public class NPCMove : MonoBehaviour
{
    //設定した待機時間
    [SerializeField] private float waitTime;
    //待機時間を数える
    [SerializeField] float time = 0;
    [SerializeField] private NPCEatObjectScript npceat;
    private int p;
    [SerializeField] GameManager gameManager;

    //位置の基準になるオブジェクトのTransformを収める
    public Transform central;
    private NavMeshAgent agent;
    [SerializeField] private EatObjectScript eatObj;
    [SerializeField] private GameObject npc_crown;
    private GameObject[] arrayobjs1p;

    [SerializeField] private List<float> l_dis1p = new List<float>();

    private float minObj1p;//最も近いオブジェクトの距離感
    private int minIndex1p;//最も近いオブジェクトのインデックス
    private Vector3 _destination;
    [SerializeField] private GameObject player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //目標地点に近づいても速度を落とさなくなる
        agent.autoBraking = false;
        minObj1p = 1000;
    }

    void Update()
    {
        //目標地点に近づいたら
        if (agent.remainingDistance <= 1f)
        {
            StopHere();
        }
    }


    void StopHere()
    {

        //NavMeshAgentを止める
        // agent.isStopped = true;
        //待ち時間を数える
        time += Time.deltaTime;

        //待ち時間が設定された数値を超えると発動
        if (time > waitTime)
        {
            //目標地点を設定し直す
            //NPCが常に動いたてもらうため、周囲に食べるオブジェクトがない場合はPlayerに向かう
            agent.destination = player.transform.position;
            time = 0;
        }
    }

    void OnDisable()
    {
        npc_crown.SetActive(false);
    }

    //CollisionDetectorオブジェクトのonTriggerStayにセットし、衝突判定を受け取るメソッド
    public void OnDetectObject(Collider collider)
    {
        int obj2p = eatObj.obj2p;
        int obj3p = eatObj.obj3p;
        int obj4p = eatObj.obj4p;
        int obj5p = eatObj.obj5p;
        int obj8p = eatObj.obj8p;
        int obj10p = eatObj.obj10p;
        int obj12p = eatObj.obj12p;
        int obj15p = eatObj.obj15p;
        int obj20p = eatObj.obj20p;
        int obj30p = eatObj.obj30p;
        int obj50p = eatObj.obj50p;
        int objover1 = eatObj.objover1;
        int objover2 = eatObj.objover2;
        int objover3 = eatObj.objover3;
        int objover4 = eatObj.objover4;
        int objover5 = eatObj.objover5;
        int objover6 = eatObj.objover6;
        int objoverMax = eatObj.objoverMax;
        //NPCは基本的にポイントが高くなるオブジェクトを目標地点にする
        //Playerを追いかける
        if (collider.CompareTag("Player"))
        {
            if (npceat.npc_level > eatObj.level)//npcとplayerが同じレベルなら追いかける
            {
                agent.destination = collider.transform.position;
                Debug.LogError("追いかける:Playerを追いかける");
            }
            // else if (npceat.npc_level <= eatObj.level)
            // {
            //     agent.destination = -collider.transform.position;//playerと反対方向にいく
            //     Debug.LogError("逃げる:Playerから逃げる");
            // }
        }
        else if (collider.CompareTag("AT"))
        {
            agent.destination = collider.transform.position;
        }
        else if (collider.CompareTag("INCR"))
        {
            agent.destination = collider.transform.position;
        }
        else if (collider.CompareTag("QUESTION"))
        {
            agent.destination = collider.transform.position;
        }
        else
        {

            //Playerの大きさをポイントに応じて変更する
            int p = npceat.point;

            if (p < obj5p)
            {
                if (collider.CompareTag("1p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("2p"))
                {
                    agent.destination = collider.transform.position;
                }
            }
            else if (obj5p <= p && p < obj15p)
            {
                if (collider.CompareTag("3p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("4p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("5p"))
                {
                    agent.destination = collider.transform.position;
                }

            }
            else if (obj15p <= p && p < objover1)
            {
                if (collider.CompareTag("5p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("8p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("10p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("12p"))
                {
                    agent.destination = collider.transform.position;
                }

            }
            else if (objover1 <= p)
            {

                if (collider.CompareTag("15p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("20p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("30p"))
                {
                    agent.destination = collider.transform.position;
                }
                else if (collider.CompareTag("50p"))
                {
                    agent.destination = collider.transform.position;
                }

            }

        }
    }
}
