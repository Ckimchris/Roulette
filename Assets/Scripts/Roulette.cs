using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    public GameObject sparks;
    public float numberResult;
    public bool spinning;    
    public GameObject wheel;
    public Transform wheel_sections;
    public Transform wheelParent;
    public List<GameObject> prizes;
    public GameObject prize;
    public List<Transform> spriteList;
    public GameObject Button;
    public List<AnimationCurve> animationCurves;

    // Start is called before the first frame update
    void Start()
    {
        spinning = false;
    }

    public void Spin()
    {
        numberResult = Random.Range(1, 100);
        if(numberResult <= 20)
        {
            StartCoroutine(RedeemPrize(1080f + 22.5f, 0));
        }
        else if(numberResult > 20 && numberResult <= 30)
        {
            StartCoroutine(RedeemPrize(1080f + 67.5f, 1));
        }
        else if(numberResult > 30 && numberResult <= 40)
        {
            StartCoroutine(RedeemPrize(1080f + 112.5f, 2));
        }
        else if(numberResult > 40 && numberResult <= 50)
        {
            StartCoroutine(RedeemPrize(1080f + 157.5f, 3));
        }
        else if(numberResult > 50 && numberResult <= 55)
        {
            StartCoroutine(RedeemPrize(1080f + 202.5f, 4));
        }
        else if(numberResult > 55 && numberResult <= 75)
        {
            StartCoroutine(RedeemPrize(1080f + 247.5f, 5));
        }
        else if(numberResult > 75 && numberResult <= 80)
        {
            StartCoroutine(RedeemPrize(1080f + 292.5f, 6));
        }
        else if(numberResult > 80 && numberResult <= 100)
        {
            StartCoroutine(RedeemPrize(1080f + 337.5f, 7));
        }
    }

    public void Spin(bool testing)
    {
        numberResult = Random.Range(1, 100);
        if(numberResult <= 20)
        {
            if(testing)
            {
                Debug.Log(prizes[0].name + " was won!");
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 22.5f, 0));
            }
        }
        else if(numberResult > 20 && numberResult <= 30)
        {
            if(testing)
            {
                Debug.Log(prizes[1].name + " was won!");
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 67.5f, 1));
            }
        }
        else if(numberResult > 30 && numberResult <= 40)
        {
            if(testing)
            {
                Debug.Log(prizes[2].name + " was won!");
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 112.5f, 2));
            }
        }
        else if(numberResult > 40 && numberResult <= 50)
        {
            if(testing)
            {
                Debug.Log(prizes[3].name + " was won!");
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 157.5f, 3));
            }
        }
        else if(numberResult > 50 && numberResult <= 55)
        {
            if(testing)
            {
                Debug.Log(prizes[4].name + " was won!");
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 202.5f, 4));
            }
        }
        else if(numberResult > 55 && numberResult <= 75)
        {
            if(testing)
            {
                Debug.Log(prizes[5].name + " was won!");
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 247.5f, 5));
            }
        }
        else if(numberResult > 75 && numberResult <= 80)
        {
            if(testing)
            {
                Debug.Log(prizes[6].name + " was won!");
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 292.5f, 6));
            }
        }
        else if(numberResult > 80 && numberResult <= 100)
        {
            if(testing)
            {
                Debug.Log(prizes[7].name + " was won!");
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 337.5f, 7));
            }
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene("Roulette");
    }

    void SetToRestart()
    {
        Button.GetComponent<Button>().onClick.RemoveListener(Spin);
        Button.GetComponent<Button>().onClick.AddListener(ResetScene);
        Button.transform.GetChild(0).GetComponent<Text>().text = "Restart";
    }

    void GetSpriteList(Transform obj)
    {
        foreach(Transform child in wheelParent)
        {
            spriteList.Add(child);
        }

        foreach(Transform child in obj)
        {
            if(prize != child.gameObject)
            {
                if(null == child)
                {
                    continue;
                }
                spriteList.Add(child);
                GetSpriteList(child);
            }
        }
        spriteList.Add(obj);
    }

    IEnumerator SpinTheWheel (float time, float endAngle)
    {
        spinning = true;
        
        float timer = 0.0f;        
        float startAngle = wheel.transform.eulerAngles.z;        
        endAngle = endAngle - startAngle;
        
        while (timer < time) {
        //to calculate rotation
            float angle = endAngle * animationCurves [0].Evaluate (timer / time) ;
            wheel.transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }
        
        wheel.transform.eulerAngles = new Vector3 (0.0f, 0.0f, endAngle + startAngle);
    }

    private IEnumerator RedeemPrize(float degrees, int prizeNumber)
    {
        StartCoroutine(SpinTheWheel (3f, degrees));
        prize = prizes[prizeNumber];
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeOut(30f));
        sparks.transform.position = new Vector3(prize.transform.position.x, prize.transform.position.y, 1f);
        sparks.GetComponent<ParticleSystem>().Play();
        SetToRestart();
    }

    IEnumerator FadeOut(float fadeTime)
    {
        GetSpriteList(wheel_sections);

        float elapsedTime = 0f;

        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            foreach(Transform child in spriteList)
            {
                Color initialColor = child.gameObject.GetComponent<SpriteRenderer>().color;
                Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
                Color currentColor = Color.Lerp(initialColor, targetColor, elapsedTime/ fadeTime);
                child.gameObject.GetComponent<SpriteRenderer>().color = currentColor;
            }
            yield return null;
        }
    }
}
