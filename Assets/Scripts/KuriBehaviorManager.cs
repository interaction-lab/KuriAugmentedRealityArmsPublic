using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

static class Extensions {
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}
public class KuriBehaviorManager : MonoBehaviour {

    public enum KURI_BEHAVIORS {
        FacePlant,
        Shrug,
        ArmCross,
        Clap,
        Celebrate,
        Party,
        Waving,
        Follow,
        HighFiveStart
    }



    List<KURI_BEHAVIORS> positiveBehaviors = new List<KURI_BEHAVIORS> { KURI_BEHAVIORS.Clap, KURI_BEHAVIORS.Party, KURI_BEHAVIORS.Waving };
    List<KURI_BEHAVIORS> negativeBehaviors = new List<KURI_BEHAVIORS> { KURI_BEHAVIORS.FacePlant, KURI_BEHAVIORS.Shrug, KURI_BEHAVIORS.ArmCross };
    List<KURI_BEHAVIORS> positiveBehaviorsCopy = new List<KURI_BEHAVIORS> { KURI_BEHAVIORS.Clap, KURI_BEHAVIORS.Party, KURI_BEHAVIORS.Waving };
    List<KURI_BEHAVIORS> negativeBehaviorsCopy = new List<KURI_BEHAVIORS> { KURI_BEHAVIORS.FacePlant, KURI_BEHAVIORS.Shrug, KURI_BEHAVIORS.ArmCross };



    public static KuriBehaviorManager instance;

    KURI_BEHAVIORS currentBehavior;

    Animator m_Animator;

    static string behaviorColumnName = "KuriBehavior";

    void Start() {
        if (instance == null) {
            instance = this;
        }

        m_Animator = gameObject.GetComponent<Animator>();
        SetTrackingObjOfInterest(false);
        LoggingManager.instance.AddLogColumn(behaviorColumnName, "");
    }

    public void RunKuriBehavior(KURI_BEHAVIORS kb) {
        m_Animator.ResetTrigger(currentBehavior.ToString());
        m_Animator.SetTrigger(kb.ToString());
        currentBehavior = kb;
        LoggingManager.instance.UpdateLogColumn(behaviorColumnName, kb.ToString());
    }

    public void RunRandomPositiveBehavior() {
        if (positiveBehaviorsCopy.Count > 0) {
            int index = Random.Range(0, positiveBehaviorsCopy.Count);
            RunKuriBehavior(positiveBehaviorsCopy[index]);
            positiveBehaviorsCopy.RemoveAt(index);
            return;
        }
        RunKuriBehavior(positiveBehaviors[Random.Range(0, positiveBehaviors.Count)]);
    }

    public void RunRandomNegativeBehavior() {
        if (negativeBehaviorsCopy.Count > 0) {
            int index = Random.Range(0, negativeBehaviorsCopy.Count);
            RunKuriBehavior(negativeBehaviorsCopy[index]);
            negativeBehaviorsCopy.RemoveAt(index);
            return;
        }
        RunKuriBehavior(negativeBehaviors[Random.Range(0, negativeBehaviors.Count)]);
    }

    public void SetTrackingObjOfInterest(bool oIn, float time = 1f) {
        if (m_Animator.enabled == !oIn)
            return;
        m_Animator.enabled = !oIn;
        if (oIn)
            foreach (FollowCurrentObjectOfInterest fcooi in transform.GetComponentsInChildren<FollowCurrentObjectOfInterest>()) {
                fcooi.MoveArmToObjOfInterest(time);
            }
    }

}
