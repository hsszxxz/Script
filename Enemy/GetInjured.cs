using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///<summary>
    public class GetInjured : MonoBehaviour
    {        
        private int GetRetraint(FiveElements enemyFiveElements, FiveElements attackFiveElements)
        {
            switch (enemyFiveElements)
            {
                case FiveElements.Jin:
                    switch (attackFiveElements)
                    {
                        case FiveElements.Mu:
                            return 1;
                        case FiveElements.Huo:
                            return 0;
                        default:
                            return 2;
                    }
                case FiveElements.Mu:
                    switch (attackFiveElements)
                    {
                        case FiveElements.Tu:
                            return 1;
                        case FiveElements.Jin:
                            return 0;
                        default:
                            return 2;
                    }
                case FiveElements.Shui:
                    switch (attackFiveElements)
                    {
                        case FiveElements.Huo:
                            return 1;
                        case FiveElements.Tu:
                            return 0;
                        default:
                            return 2;
                    }
                case FiveElements.Huo:
                    switch (attackFiveElements)
                    {
                        case FiveElements.Jin:
                            return 1;
                        case FiveElements.Shui:
                            return 0;
                        default:
                            return 2;
                    }
                case FiveElements.Tu:
                    switch (attackFiveElements)
                    {
                        case FiveElements.Shui:
                            return 1;
                        case FiveElements.Mu:
                            return 0;
                        default:
                            return 2;
                    }
                default:
                    return 3;
            }
        }
        private int AttackValue(FiveElements enemyFiveElements, FiveElements attackFiveElements,int attackValue)
        {
            switch ( GetRetraint(enemyFiveElements, attackFiveElements) )
            {
                case 0:
                    return attackValue/2;
                case 1:
                    return attackValue*2;
                case 2:
                    return attackValue;
                default:
                    return -1;
            }
        }
    }
}

