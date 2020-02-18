using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{
    public float thrust = 1.0f;
    public float maxthrust = 10.0f;
    public float speed = 5.0f;
    public float jumpspeed = 0.1f;

    private Rigidbody rb;
    private LineRenderer dirline;

    private Vector3 direction;
    private Vector3 pos;
    private float force;

    private Scene currentScene;

    public float strength = 0;
    public Image powerBar;
    public float delta = 50;
    public float maxPower = 20000;

    public float totalMass;

    Vector3 dir;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dirline = GetComponent<LineRenderer>();
    }


    void Update()
    {
        KeyboardRPG();//for testing
        PointShoot02();
        //PointShoot();
        isOutofArea();
        //PressJumpForward();
        //PressJumpUp();
    }


    void KeyboardRPG()
    {
        if (Input.GetKey("w"))
        {
            dir = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            rb.AddForce(dir * speed * 10);
        }

        if (Input.GetKey("s"))
            rb.AddForce(-1 * dir * speed * 10);

        if (Input.GetKey("a"))
            rb.AddForce(-1 * Camera.main.transform.right * speed * 10);

        if (Input.GetKey("d"))
            rb.AddForce(Camera.main.transform.right * speed * 10);
        
        //if (Input.GetKeyDown("space"))
        //{
        //    rb.AddForce(Vector3.up * jumpspeed * 10);
        //}

    }

    //预测路线，显示预测线，有投掷上限
    void PointShoot()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.tag == "Ground" || hit.rigidbody.tag == "Block")
                {
                    dirline.positionCount = 2;
                    dirline.SetPosition(0, rb.transform.position);
                    dirline.SetPosition(1, hit.point);
                    pos = hit.point;
                    pos.y = rb.transform.position.y; //avoid jumpping
                    force = Mathf.Min(Vector3.Distance(pos, rb.transform.position), maxthrust) * thrust;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            direction = pos - rb.transform.position;
            direction.y = 2;
            dirline.positionCount = 0;
            rb.AddForce(direction * force * 10);
        }
    }

    void PointShoot02()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.tag == "Ground" || hit.rigidbody.tag == "Block")
                {

                    if (strength <= maxPower)
                    {
                        strength += delta;
                    }
                    else
                    {
                        strength = maxPower;
                    }
                    direction = pos - rb.transform.position;
                    direction.y = 0;
                    dirline.positionCount = 2;
                    dirline.SetPosition(0, rb.transform.position);
                    dirline.SetPosition(1, rb.transform.position + direction.normalized * 5);
                    pos = hit.point;
                    pos.y = rb.transform.position.y; //avoid jumpping
                    force = Mathf.Min(Vector3.Distance(pos, rb.transform.position), maxthrust) * thrust;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {         
            dirline.positionCount = 0;
            rb.AddForce(direction.normalized * strength * 0.3f);
        }
    }



    void PressJumpForward()
    {
        if (Input.GetMouseButton(0))
        {
            if (strength <= maxPower)
            {
                strength += delta;
            }
            else
            {
                strength = maxPower;
            }
            powerBar.GetComponent<RectTransform>().sizeDelta = new Vector2(20, strength/maxPower * 500);
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.AddForce(Camera.main.transform.forward * strength);
            strength = 0;
        }
    }

    void PressJumpUp()
    {
        if (Input.GetMouseButton(0))
        {
            if (strength <= maxPower)
            {
                strength += delta;
            }
            else
            {
                strength = maxPower;
            }
            powerBar.GetComponent<RectTransform>().sizeDelta = new Vector2(20, strength / maxPower * 500);
        }

        if (Input.GetMouseButtonUp(0))
        {
            rb.AddForce(new Vector3(0,1,0) * strength);
            strength = 0;
        }
    }


    float GetTotalMass(GameObject ob)
    {
        float ma = 0;
        ma += ob.GetComponent<Rigidbody>().mass;

        Rigidbody[] bodies;
        bodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody x in bodies)
        {
            ma += x.mass;
        }

        return ma;
    }


    void isOutofArea()
    {
        if(transform.position.y < -20)
        {
            Destroy(gameObject);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
