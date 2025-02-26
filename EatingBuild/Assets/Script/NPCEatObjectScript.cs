using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class NPCEatObjectScript : MonoBehaviour
{
    private bool npcspeedflag = true;
    private float addSpeed = 0.625f;
    private int decreasePoint;
    [SerializeField] private EatObjectScript eatObj;//PlayerのeatObjectscriptをアタッチする
    void NPCAddPoint(int number)//ポイントの追加
    {
        point = point + number;
    }
    public int point;//大きさを変える時などに使うポイント
    public NavMeshAgent _agent;
    public int npc_level;//pointを一定ごとにlvに分類させていく

    [SerializeField] private GameManager gameManager;
    [SerializeField] private CountDownTimer countDownTimer;
    [SerializeField] private GameObject revenge;
    [SerializeField] private SphereCollider spherecol;
    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
        float agentspeed = _agent.speed;
        spherecol.radius = 10;//sphrecolliderの大きさを指定
    }

    private void AccelerationItem()//スピードアップのアイテムを食べた時
    {
        addSpeed = 0.625f;
        _agent.speed += addSpeed;
    }
    private void IncreasePointItem()////Pointが増えるアイテムを食べた時
    {
        if (point < eatObj.obj30p)
        {
            NPCAddPoint(point / 2);//pointを追加
        }
        else
        {
            NPCAddPoint(point / 2);//pointを追加
        }

    }
    private void DecreasePointItem()//Pointが減るアイテムを食べた時。
    {
        decreasePoint = point / 6;//小数点以下は切り捨て。
        decreasePoint = -decreasePoint;//-にする
        Debug.Log("NPCのdecreasePointは" + decreasePoint);
        NPCAddPoint(decreasePoint);//pointを減少
        if (point >= eatObj.objover1)
        {
            _agent.speed -= addSpeed;
        }
        else
        {
            _agent.speed -= addSpeed;
        }
    }
    private void QuestionItem()//questionが食べられた時。
    {
        int ranItem = Random.Range(0, 3);//アイテムは３種類あるから.0~3の間で乱数。intは「max - 1」
        Debug.Log("ranItemは" + ranItem);
        switch (ranItem)
        {
            case 0:
                AccelerationItem();
                break;
            case 1:
                IncreasePointItem();
                break;
            case 2:
                IncreasePointItem();
                break;
        }
    }
    private void OnDisable()
    {
        // gameManager.AddKill(1);
    }
    void OnTriggerEnter(Collider col)
    {
        //eatObjectscriptと変数の値を統一する
        float smallTime = eatObj.smallTime;//objectを小さくするのにかかる時間
        float smallTimeApartment = eatObj.smallTimeApartment;//マンションを小さくするのにかかる時間
        float smallTimeBigApartment = eatObj.smallTimeBigApartment;//大きいビルを小さくするのにかかる時間
        float playerScaleTime = eatObj.playerScaleTime;//プレイヤーを大きくするのにかかる時間
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
        int p = point;

        int n = 2;//pointを標準より倍にするために使う
        switch (col.gameObject.tag)
        {
            case "Untagged"://ポイントがついている以外は食べれない
                break;

            case "Player":
                if (npc_level > eatObj.level)
                {

                    col.transform.DOShakeRotation(
                         duration: 0.5f,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(gameManager.point);
                        col.gameObject.SetActive(false);
                        revenge.SetActive(true);
                        Time.timeScale = 0;
                    });
                }
                break;

            case "NPC":
                if (p > col.gameObject.GetComponent<NPCEatObjectScript>().point)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: 0.5f,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(col.gameObject.GetComponent<NPCEatObjectScript>().point);
                        col.gameObject.SetActive(false);

                    });
                }

                break;

            case "AT"://speedが上がるアイテムを食べた時
                if (p >= 0)
                {
                    AccelerationItem();
                    col.gameObject.SetActive(false);//gameObjectを消すより非表示の方が処理が軽いらしい
                }
                break;

            case "INCR"://Pointが増えるアイテムを食べた時
                if (p >= 0)
                {
                    IncreasePointItem();
                    col.gameObject.SetActive(false);//gameObjectを消すより非表示の方が処理が軽いらしい
                }
                break;

            case "DEC"://Pointが減るアイテムを食べた時
                if (p >= 0)
                {
                    DecreasePointItem();
                    col.gameObject.SetActive(false);//gameObjectを消すより非表示の方が処理が軽いらしい
                }
                break;

            case "QUESTION"://Pointが減るアイテムを食べた時
                if (p >= 0)
                {
                    QuestionItem();
                    col.gameObject.SetActive(false);//gameObjectを消すより非表示の方が処理が軽いらしい
                }
                break;

            case "1p":

                if (p >= 0)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTime,   // 演出時間
                         strength: 90f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTime)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(1 * n);//ポイントを1追加する
                        col.gameObject.SetActive(false);//gameObjectを消すより非表示の方が処理が軽いらしい
                    });
                }

                break;

            case "2p":

                if (p >= obj2p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTime,   // 演出時間
                         strength: 90f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTime)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(2 * n);
                        col.gameObject.SetActive(false);

                    });
                }
                break;


            case "3p":

                if (p >= obj3p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTime,   // 演出時間
                         strength: 90f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTime)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(3 * n);
                        col.gameObject.SetActive(false);

                    });
                }
                break;

            case "4p":

                if (p >= obj4p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTime,   // 演出時間
                         strength: 90f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTime)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(4 * n);
                        col.gameObject.SetActive(false);

                    });
                }
                break;

            case "5p":

                if (p >= obj5p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTime,   // 演出時間
                         strength: 90f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTime)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(5 * n);
                        col.gameObject.SetActive(false);

                    });
                }
                break;


            case "8p":
                if (p >= obj8p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTime,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTime)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(8);

                        col.gameObject.SetActive(false);

                    });
                }
                break;

            case "10p":
                if (p >= obj10p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTime,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTime)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(20);

                        col.gameObject.SetActive(false);

                    });
                }
                break;

            case "12p":
                if (p >= obj12p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTimeApartment,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTimeApartment)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(36);
                        col.gameObject.SetActive(false);

                    });
                }
                break;

            case "15p":
                if (p >= obj15p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTimeApartment,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTimeApartment)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(150);

                        col.gameObject.SetActive(false);

                    });
                }
                break;

            case "20p":
                if (p >= obj20p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTimeBigApartment,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTimeBigApartment)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(800);

                        col.gameObject.SetActive(false);

                    });
                }
                break;

            case "30p":
                if (p >= obj30p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTimeBigApartment,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTimeBigApartment)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(1200);

                        col.gameObject.SetActive(false);

                    });
                }
                break;

            case "50p":
                if (p >= obj50p)
                {
                    col.enabled = false;
                    col.transform.DOShakeRotation(
                         duration: smallTimeBigApartment,   // 演出時間
                         strength: 60f   // シェイクの強さ
                    );
                    col.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), smallTimeBigApartment)
                    .OnComplete(() =>//dotween終了後、cubeを消す
                    {
                        NPCAddPoint(1500);

                        col.gameObject.SetActive(false);

                    });
                }
                break;




        }
    }

    void FixedUpdate()
    {
        //eatObjectscriptと変数の値を統一する
        float smallTime = eatObj.smallTime;//objectを小さくするのにかかる時間
        float smallTimeApartment = eatObj.smallTimeApartment;//マンションを小さくするのにかかる時間
        float smallTimeBigApartment = eatObj.smallTimeBigApartment;//大きいビルを小さくするのにかかる時間
        float playerScaleTime = eatObj.playerScaleTime;//プレイヤーを大きくするのにかかる時間
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

        {

            //Playerの大きさをポイントに応じて変更する
            int p = point;

            if (p < obj2p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(1f, 1f, 1f), playerScaleTime
                );
                npc_level = 0;
            }

            else if (obj2p <= p && p < obj3p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(1.5f, 1.5f, 1.5f), playerScaleTime
                );
                npc_level = 1;
            }
            else if (obj3p <= p && p < obj4p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(2f, 2f, 2f), playerScaleTime
                ); spherecol.radius = 9f;
                npc_level = 2;
            }

            else if (obj4p <= p && p < obj5p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(2.5f, 2.5f, 2.5f), playerScaleTime
                );
                spherecol.radius = 8f;
                npc_level = 3;
                if (npcspeedflag == true)
                {
                    npcspeedflag = false;
                    _agent.speed += addSpeed;
                }
            }

            else if (obj5p <= p && p < obj8p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(3f, 3f, 3f), playerScaleTime
                );
                spherecol.radius = 7.5f;
                npc_level = 4; if (npcspeedflag == false)
                {
                    npcspeedflag = true;
                    _agent.speed += addSpeed;
                }
            }

            else if (obj8p <= p && p < obj10p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(4f, 4f, 4f), playerScaleTime
                );
                spherecol.radius = 6f;
                npc_level = 5;
                if (npcspeedflag == true)
                {
                    npcspeedflag = false;
                    _agent.speed += addSpeed;
                }
            }


            else if (obj10p <= p && p < obj12p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(6f, 6f, 6f), playerScaleTime
                );
                spherecol.radius = 4.5f;
                npc_level = 6; if (npcspeedflag == false)
                {
                    npcspeedflag = true;
                    _agent.speed += addSpeed;
                }
            }

            else if (obj12p <= p && p < obj15p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(8f, 8f, 8f), playerScaleTime
                );
                spherecol.radius = 4.5f;
                npc_level = 7; if (npcspeedflag == true)
                {
                    npcspeedflag = false;
                    _agent.speed += addSpeed;
                }
            }

            else if (obj15p <= p && p < obj20p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(10f, 10f, 10f), playerScaleTime
                );
                spherecol.radius = 3.5f;
                npc_level = 8; if (npcspeedflag == false)
                {
                    npcspeedflag = true;
                    _agent.speed += addSpeed;
                }
            }

            else if (obj20p <= p && p < obj30p)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(12f, 12f, 12f), playerScaleTime
                );
                spherecol.radius = 3f;
                npc_level = 9; if (npcspeedflag == true)
                {
                    npcspeedflag = false;
                    _agent.speed += addSpeed;
                }
            }

            else if (obj30p <= p && p < objover1)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(14f, 14f, 14f), playerScaleTime
                ); spherecol.radius = 2.5f;
                npc_level = 10; if (npcspeedflag == false)
                {
                    npcspeedflag = true;
                    _agent.speed += addSpeed;
                }
            }

            else if (objover1 <= p && p < objover2)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(15f, 15f, 15f), playerScaleTime
                );
                spherecol.radius = 2f;
                npc_level = 11; if (npcspeedflag == true)
                {
                    npcspeedflag = false;
                    _agent.speed += addSpeed;
                }
            }

            else if (objover2 <= p && p < objover3)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(16f, 16f, 16f), playerScaleTime
                ); spherecol.radius = 2f;
                npc_level = 12; if (npcspeedflag == false)
                {
                    npcspeedflag = true;
                    _agent.speed += addSpeed;
                }
            }

            else if (objover3 <= p && p < objover4)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(17f, 17f, 17f), playerScaleTime
                );
                spherecol.radius = 2f;
                npc_level = 13; if (npcspeedflag == true)
                {
                    npcspeedflag = false;
                    _agent.speed += addSpeed;
                }
            }

            else if (objover4 <= p && p < objover5)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(18f, 18f, 18f), playerScaleTime
                ); spherecol.radius = 2f;
                npc_level = 14; if (npcspeedflag == false)
                {
                    npcspeedflag = true;
                    _agent.speed += addSpeed;
                }
            }
            else if (objover5 <= p && p < objover6)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(19f, 19f, 19f), playerScaleTime
                ); spherecol.radius = 2f;
                npc_level = 15; if (npcspeedflag == true)
                {
                    npcspeedflag = false;
                    _agent.speed += addSpeed;
                }
            }
            else if (objover6 <= p && p < objoverMax)
            {
                this.gameObject.transform.DOScale(
                    new Vector3(20f, 20f, 20f), playerScaleTime
                ); spherecol.radius = 2f;
                npc_level = 16; if (npcspeedflag == false)
                {
                    npcspeedflag = true;
                    _agent.speed += addSpeed;
                }
            }
        }


    }
}