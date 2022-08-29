using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// template <typename T> = <T>
// 그런데 아무 타입이나 T에 올 수 있어서 제약을 두어야 한다. (우리의 의도는 컴포넌트 타입만 받기를 원하기 때문)
// <T>에 들어오는 T는 MonoBegaviour를 상속 받아야 해
public class SingletonBehabiour<T> : MonoBehaviour where T : MonoBehaviour
{
    // Unity의 제어를 받기 위해 MonoBehaviour를 상속한다.
    // 컴포넌트에 대해서만 동작하기 때문에 위와 같은 where 제약을 작성한다.

    private static T instance;
    public static T Instance 
    {  
        get 
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                DontDestroyOnLoad(instance.gameObject); 
            }

            return instance; 
        }
    }

    virtual protected void Awake()
    {
        if(instance != null)
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
            return;
        }
        instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject); // 씬이 전환될 때 파괴되지 않아야 한다.
    }
}

// [Awake에서 Initialize되어야 하는 이유]
// Awake는 스크립트가 활성화되어있지 않아도 호출이 된다.
// OnEnable과 Start는 스크립가 활성화 (오브젝트가 활성화)될 때 호출된다.
