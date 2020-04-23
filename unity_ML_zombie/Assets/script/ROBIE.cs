
using UnityEngine;
using MLAgents;
using MLAgents.Sensors;

public class ROBIE :Agent
{
    private Rigidbody rigRobie;
    private Rigidbody rigBALL;
    private Rigidbody rigDEADBALL;
    
    [Header("速度"),Range(1,1000)]
    public float speed = 5f;
   /* public float thrust = 100;*/
    private Animator animator;
  
    
    private void Start()
    {
        //抓到剛體
        rigRobie = GetComponent<Rigidbody>();
        rigBALL = GameObject.Find("BALL").GetComponent<Rigidbody>();
        rigDEADBALL = GameObject.Find("DEADBALL").GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
    }
   
    
    public override void OnEpisodeBegin()
    {

        //先將加速度角速度歸零
        rigRobie.velocity = Vector3.zero;
        rigRobie.angularVelocity = Vector3.zero;
        rigBALL.velocity = Vector3.zero;
        rigBALL.angularVelocity = Vector3.zero;
        rigDEADBALL.velocity = Vector3.zero;
        rigDEADBALL.angularVelocity = Vector3.zero;




        //指定隨機座標給各位

        Vector3 PosRobie = new Vector3(Random.Range(-3f, 2f), 0.1f, Random.Range(-4f, -2f));
        transform.position = PosRobie;
        Vector3 PosBALL  = new Vector3(Random.Range(-0.5f, 0f), 0.1f, Random.Range(-0.5f, 0));
        rigBALL.position = PosBALL;
        Vector3 PosDEADBALL = new Vector3(Random.Range(-1f, 2f), 0.1f, Random.Range(-1f, 1f));
        rigDEADBALL.position = PosDEADBALL;

        BALL.complete = false;
        DEADBALL.complete = false;
    }
  

    public override void CollectObservations(VectorSensor sensor)
    {
        //將座標加入觀測資料
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigBALL.position);
        sensor.AddObservation(rigDEADBALL.position);

        sensor.AddObservation(rigRobie.velocity.x);
        sensor.AddObservation(rigRobie.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
    { 
        animator.SetBool("走路開關", speed != 0);
        //使用參數控制機器人
        Vector3 control = Vector3.zero;
        control.x = vectorAction[0];
        control.z = vectorAction[1];
        rigRobie.AddForce(control * speed);
       
       

       
     

        //成功
        if (BALL.complete) 
        {
            SetReward(1);
            EndEpisode(); 
        
        }
      
      

        if( rigRobie.position.y<0 || rigBALL.position.y<0)
        {
            SetReward(-1);
            
            EndEpisode();
          
        } 
       if(DEADBALL.complete||rigDEADBALL.position.y < 0)
        {
            SetReward(-1);
            EndEpisode();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
     
        return action;
    }
 

    /*
    private void Update()
    {
        walkrm();
        Walk();
    }

    private void Walk()
    {
       

         float v = Input.GetAxis("Vertical");
        
        rigRobie.AddForce(tran.forward * speed * v * Time.deltaTime);//區域座標z軸
        animator.SetBool("走路開關", v != 0);
       

    }
    private void walkrm()
    {
        float h = Input.GetAxis("Horizontal");
        tran.Rotate(0, turn * h * Time.deltaTime, 0);
        
        animator.SetBool("走", h != 0);
        
        
    }
    */
}
