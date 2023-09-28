using UnityEngine;
using MADD;

[Docs("Example of a super class that can be inherited and documented. DocGen supports up to 10 sub classes, after that, you are on your own...")]
public class ExampleBaseClass : MonoBehaviour
{
    public int baseInt;

    [Docs("")]
    public void DoSomething()
    {
        print("I am useful!");
    }
}
