using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Magicko.Core
{
    public class ActionsManager : MonoBehaviour
    {
        IAction currentAction;
        public void BeginAction(IAction action)
        {
            if(currentAction != null && currentAction != action)
            {
                currentAction.CancelAction();
            }
            currentAction = action;
        }
    }
}
