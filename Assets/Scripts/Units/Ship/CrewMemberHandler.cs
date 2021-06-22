using UnityEngine;

/* Moral affected by happiness, 33% negative modifier above 0 and below 10, 66% negative modifier below 0 and above -5, 88% negative modifier -6 and below (-10 is minimum) 
 * Loyalty affected by moral that is less than  */
public class CrewMemberHandler : MonoBehaviour
{
    private int m_MaxMoral = 10, m_MaxLoyalty = 100;

    [HideInInspector]
    [Range(0, 100)]
    public int m_Loyalty; //Affected by happ

    [SerializeField]
    private int m_FoodCost = 3;

    [SerializeField]
    [Range(-10, 10)]
    private int m_OverallHappiness;

    [SerializeField]
    [Range(0, 10)]
    private double m_Moral; //Affected by negative happiness, Lost battles, Ship's loyalty and Damage taken to the ship

    public bool m_Starving;

    public void ModifyHappiness(int _HappinessModifier)
    {
        m_OverallHappiness += _HappinessModifier;
    }

    public void ModifyMoral(int _MoralModifier)
    {
        m_Moral += _MoralModifier;
    }

    public int Eat(int _TotalFood)
    {
        if (_TotalFood > 0)
        {
            return _TotalFood -= m_FoodCost;
        }
        else
        {
            m_Starving = true;
            return 0;
        }
    }

    private void Update()
    {
        if (m_Starving)
        {
            ModifyHappiness(-10);
        }

        CalculateMoral();
    }

    private void CalculateMoral()
    {
        if (m_OverallHappiness < 10 && m_OverallHappiness > 0)
        {
            double HappinessModifier = (m_MaxMoral - m_OverallHappiness) * 0.33;
            m_Moral -= HappinessModifier;

            if (m_Moral < 0)
                m_Moral = 0;
        }
        else if (m_OverallHappiness == 0)
        {
            m_Moral -= 0.66;
        }
        else if ((m_OverallHappiness < 0) && (m_OverallHappiness > -5))
        {
            int Invert = m_OverallHappiness * -1;
            double HappinessModifier = (m_MaxMoral - Invert) * 0.66;
            m_Moral -= HappinessModifier;

            if (m_Moral < 0)
                m_Moral = 0;
        }
        else if (m_OverallHappiness < -5)
        {
            int Invert = m_OverallHappiness * -1;
            double HappinessModifier = (m_MaxMoral - Invert) * 0.88;
            m_Moral -= HappinessModifier;

            if (m_Moral < 0)
                m_Moral = 0;
        }
    }

    private void CalculateLoyalty()
    {
        if (m_Moral < 5)
        {
            double MoralModifier = (m_MaxLoyalty - m_Moral) * 0.1;
            m_Loyalty -= Mathf.RoundToInt((float)MoralModifier);

            if (m_Loyalty < 0)
                m_Loyalty = 0;
        }

        if (m_OverallHappiness <= 10 && m_OverallHappiness > 0)
        {
            double HappinessModifier = m_OverallHappiness / 100;
            HappinessModifier = m_Loyalty * HappinessModifier;
            m_Loyalty += Mathf.RoundToInt((float)HappinessModifier);

            if (m_Loyalty < 0)
                m_Loyalty = 0;
        }

        if ((m_OverallHappiness < 0) && (m_OverallHappiness > -5))
        {
            int Invert = m_OverallHappiness * -1;
            double HappinessModifier = Invert * 1.5;
            m_Loyalty -= Mathf.RoundToInt((float)HappinessModifier);

            if (m_Loyalty < 0)
                m_Loyalty = 0;
        }
        else if (m_OverallHappiness < -5)
        {
            int Invert = m_OverallHappiness * -1;
            int HappinessModifier = Invert * 2;
            m_Loyalty -= HappinessModifier;

            if (m_Loyalty < 0)
                m_Loyalty = 0;
        }

        if (m_Loyalty > m_MaxLoyalty)
        {
            m_Loyalty = m_MaxLoyalty;
        }
    }
}
