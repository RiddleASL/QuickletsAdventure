using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IKTest : MonoBehaviour
{
    public int chainLength = 2;

    Transform prevTransform;
    Transform currTransform;
    Transform root;

    public Transform target;
    public int itterations = 10;
    
    public List<Transform> points;
    public List<float> bonesLength;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        root = transform.root.Find("Armature");
        count = 0;
        Transform getCount = root;
        while(getCount.name != transform.name){
            bonesLength.Add(Vector3.Distance(getCount.GetChild(0).position, getCount.position));
            count++;
            getCount = getCount.GetChild(0);
            points.Add(getCount);
        }
        Debug.Log(count);
    }

    // Update is called once per frame
    void Update()
    {
        SolveIk();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        for (int i = 0; i < chainLength; i++)
        {
            if(i!=chainLength-1){
                Gizmos.DrawLine(points[(points.Count - 1) - i].position, points[(points.Count - 1) - (i+1)].position);
            }
        }
    }

    void SolveIk(){
        
        // To implement inverse kinematics (According to Github Copilot) in your code, you will need to:
        // 1:   Define the kinematic chain of the arm or object you want to animate.
        //      This involves identifying the parent-child relationships between the joints or bones in the chain.

        // 2:   Define the target position that you want the end-effector of the arm or object to reach.

        // 3:   Implement an iterative algorithm that adjusts the joint angles of the arm or object in order to move
        //      the end-effector closer to the target position.

        // 4:   Repeat step 3 until the end-effector is within an acceptable distance of the target position,
        //      or until a maximum number of iterations has been reached.

        // 5:   Update the position and rotation of each joint in the kinematic chain based on the new joint angles.

        // 6;   Repeat steps 2-5 for each frame of the animation.

        for (int i = 0; i < itterations; i++)
        {
            for (int j = 0; j < chainLength; j++)
            {
                if(j != 0){
                    //get direction from each bone to its child bone
                    Vector3 direction = points[points.Count-1-j].position - points[points.Count-2-j].position;
                    //get the position of the child bone
                    points[points.Count-1-j].position = target.position + direction.normalized * bonesLength[bonesLength.Count-1-j];
                } else {
                    points[points.Count-1-j].position = target.position;
                }
            }
        }
    }
}
