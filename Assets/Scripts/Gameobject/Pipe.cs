using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxHeight;

    [SerializeField]
    private float minHeight;

    [SerializeField]
    private float destroyTime;

    public bool IsOn = false;

    public void SelfDestroy()
    {
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }


    void OnEnable()
    {
        gameObject.transform.position = new Vector3(0f, Random.Range(minHeight, maxHeight));

        Invoke("DelayDestroy", destroyTime);
    }

    void DelayDestroy()
    {
        if (IsOn == false)
        {
            return;
        }

        PoolManager.Instance.PushObj("Pipe", this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOn)
        {
            this.gameObject.transform.position += (new Vector3(-1, 0) * speed * Time.deltaTime);
        }
    }
}
