
using UnityEngine;

public class romove : MonoBehaviour
{
    [Header("旋轉速度"), Tooltip("Robie轉"), Range(1.5f, 200f)]
    public float turn = 20f;
    [Header("走路速度"), Tooltip("Robie走"), Range(1,2000)]
    public int speed = 10;

    public Transform tran;
    public Rigidbody rig;
    public Animator ani;

    private void Update()
    {
        Turn();
        Walk();
    }

    private void Walk()
    {
      if (ani.GetCurrentAnimatorStateInfo(0).IsName("Z_walk")) return; 
          
            float v = Input.GetAxis("Vertical");

    rig.AddForce(tran.forward* speed * v *Time.deltaTime);//區域座標z軸
        ani.SetBool("走路開關", v != 0);
           
    }
    private void Turn()
{
    float h = Input.GetAxis("Horizontal");
    tran.Rotate(0, turn * h * Time.deltaTime, 0);
}
}
